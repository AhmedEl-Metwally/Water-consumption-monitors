using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Water_consumption_monitors.Dto;
using Water_consumption_monitors.Helpers;
using Water_consumption_monitors.Interface;
using Water_consumption_monitors.Models;
//using System.Security.Cryptography;
using System.Net.Http.Headers;
using Microsoft.EntityFrameworkCore;

namespace Water_consumption_monitors.Services
{
    public class AuthService : IAuth
    {
        private readonly  UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly Jwt _jwt;

        public AuthService(UserManager<ApplicationUser> userManager , IMapper mapper,
                     IOptions<Jwt> jwt , RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager; 
            _mapper = mapper;
            _jwt = jwt.Value;

        }

        public async Task<string> AddRoleAsync(AddRole model)
        {

            var user = await _userManager.FindByIdAsync(model.UserId);
            var result = await _userManager.AddToRoleAsync(user, model.Role);

            if (user is null || await _roleManager.RoleExistsAsync(model.Role))
            {
                return "Invalid user Id Or Role ";
            }

            return result.Succeeded ? string.Empty : " Something went wrong ";

        }

        public async Task<Auth> GetTokenAsync(TokenRequest model)
        {
            var auth = new Auth();

            var user = await _userManager.FindByEmailAsync (model.Email);
            if (user is null)
            {
                auth.Message = "Email Or Password Is Incorrect";
                return auth;
            }

            if (!await _userManager.CheckPasswordAsync(user, model.Password))
            {
                auth.Message = "Email Or Password Is Incorrect";
                return auth;
            }

            var JwtSecurityToken = await CreateJwtToken(user);
            var rolesList = await _userManager.GetRolesAsync(user);

            auth.IsAuthenticated = true;
            auth.Token = new JwtSecurityTokenHandler().WriteToken(JwtSecurityToken);
            auth.Email = model.Email;
            auth.Username = user.UserName;
            auth.ExpiresOn = JwtSecurityToken.ValidTo;
            auth.Roles = rolesList.ToList();
            
            if(user.RefreshTokens.Any(t=>t .IsActive))
            {
                var activeRefreshToken = user.RefreshTokens.FirstOrDefault(t => t .IsActive);
                auth.RefreshToken = activeRefreshToken.Token;
                auth.RefreshTokenExpiration = activeRefreshToken.Expireson;
            }
            else
            {
                var refreshToken = GenerateaRefreshToken();
                auth.RefreshToken = refreshToken.Token;
                auth.RefreshTokenExpiration = refreshToken.Expireson;
                user.RefreshTokens.Add(refreshToken);
                await _userManager.UpdateAsync(user);
            }
            return auth;
        }

        public async Task<Auth> RefreshTokenAsync(string token)
        {
            var auth = new Auth();
            var user = await _userManager.Users.SingleOrDefaultAsync(u=>u .RefreshTokens.Any(t=>t .Token == token));

            if(user == null)
            {
               // auth.IsAuthenticated = false;
                auth.Message = "Invalid token";
                return auth;    
            }

            var refreshToken = user.RefreshTokens.Single(t=>t .Token == token);

            if (!refreshToken.IsActive)
            {
               //auth.IsAuthenticated = false;
                auth.Message = "Invalid token";
                return auth;
            }

            refreshToken.RevokedOn = DateTime.UtcNow;   

            var newRefreshToken = GenerateaRefreshToken();
            user.RefreshTokens.Add(newRefreshToken);
            await _userManager.UpdateAsync(user);

            var jwtToken = await CreateJwtToken(user);
            auth.IsAuthenticated = true;
            auth.Token = new JwtSecurityTokenHandler().WriteToken(jwtToken);    
            auth.Email = user.Email;
            auth.Username = user.UserName;
            var role = await _userManager.GetRolesAsync(user);
            auth.Roles = role.ToList();
            auth.RefreshToken = newRefreshToken.Token;
            auth.RefreshTokenExpiration = newRefreshToken.Expireson;

            return auth;    
        }

        public async Task <Auth> RegisterAsync(Register model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) is not null)
                return new Auth { Message = "Email is already registered" };

            if (await _userManager.FindByNameAsync(model.Username) is not null)
                return new Auth { Message = "Username is already registered" };

            var user = _mapper.Map<ApplicationUser>(model);

            var register = await _userManager.CreateAsync(user, model.Password);
            if(!register .Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in register.Errors)
                {
                   errors += $"{error.Description},";
                }
                return new Auth { Message = errors };   
            }

            await _userManager.AddToRoleAsync(user, "user");
            var JwtSecurityToken = await CreateJwtToken(user);
            var dto = _mapper.Map<AuthDto>(user);

            return new Auth
            {
                Email = user.Email,
                ExpiresOn = JwtSecurityToken.ValidTo,
                IsAuthenticated = true,
                Roles = new List<string> { "User" },
                Token = new JwtSecurityTokenHandler().WriteToken(JwtSecurityToken),
                Username = user.UserName
            };
        }

        public async Task<bool> RevokeTokenAsync(string token)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));

            if (user == null)  
                return false;

            var refreshToken = user.RefreshTokens.Single(t => t.Token == token);

            if (!refreshToken.IsActive)
                return false;

            refreshToken.RevokedOn = DateTime.UtcNow;

            await _userManager.UpdateAsync(user);

            return true;
        }

        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

        private RefreshToken GenerateaRefreshToken()
        {
            var randomNumber = new byte[32];
            using var generator = new RNGCryptoServiceProvider();

            generator.GetBytes(randomNumber);

            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomNumber),
                Expireson = DateTime.UtcNow.AddDays(10),
                CreatedOn = DateTime.UtcNow,
            };
        }
    }
}
