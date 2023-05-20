using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Water_consumption_monitors.DTO;
using Water_consumption_monitors.Interface;
using Water_consumption_monitors.Models;

namespace Water_consumption_monitors.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypesOfRealEstateController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork ;
        private readonly IMapper _mapper ;  

        public TypesOfRealEstateController(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;   
        }

        [HttpGet("GetById")]   
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var TypesOfRealEstate = await _unitOfWork.TypesOfRealEstate.GetByIdAsync(id);
            var data = _mapper.Map<TypesOfRealEstateDto>(TypesOfRealEstate);
            return Ok(data);   
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var TypesOfRealEstate = await _unitOfWork.TypesOfRealEstate.GetAllAsync();
            var data = _mapper.Map<IEnumerable<TypesOfRealEstateDto>>(TypesOfRealEstate);
            return Ok(data);      
        }

        [HttpPost("Add")]
        public IActionResult Add(TypesOfRealEstateDto dto)
        {
            var data = _mapper.Map<TypesOfRealEstate>(dto);
            var TypesOfRealEstate = _unitOfWork.TypesOfRealEstate.Add(data);
            _unitOfWork.Compelete();
            return Ok(data);
        }

        [HttpPut("Update")]
        public IActionResult Update(TypesOfRealEstateDto dto)
        {
            var data = _mapper.Map<TypesOfRealEstate>(dto);
            _unitOfWork.TypesOfRealEstate.Update(data);
            _unitOfWork.Compelete();
            return Ok(data);   
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(int id)
        {
            var temp = _unitOfWork.TypesOfRealEstate.GetByIdAsync(id).Result;
            _unitOfWork.TypesOfRealEstate.Delete(temp);
            return Ok(temp);
        }

    }
}
