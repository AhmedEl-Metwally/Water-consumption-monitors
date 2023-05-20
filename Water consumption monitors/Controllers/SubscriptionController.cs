using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Water_consumption_monitors.DTO;
using Water_consumption_monitors.Interface;
using Water_consumption_monitors.Models;

namespace Water_consumption_monitors.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SubscriptionController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var Subscription = await _unitOfWork.Subscription.GetByIdAsync(id);
            var date = _mapper.Map<SubscriptionDto>(Subscription);
            return Ok(date);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var Subscription = await _unitOfWork.Subscription.GetAllAsync();
            var data = _mapper.Map<IEnumerable<SubscriptionDto>>(Subscription);
            return Ok(data);    
        }

        [HttpPost("Add")]
        public IActionResult Add(SubscriptionDto dto)
        {
            var date = _mapper.Map<Subscription>(dto);
            var Subscription = _unitOfWork.Subscription.Add(date);
            _unitOfWork.Compelete();
            return Ok(date);    
        }

        [HttpPut("Update")]
        public IActionResult Update(SubscriptionDto dto)
        {
            var date = _mapper.Map<Subscription>(dto);
            _unitOfWork.Subscription.Update(date);
            _unitOfWork.Compelete();    
            return Ok(date);    
        }

        [HttpDelete("Delete")]  
        public IActionResult Delete(int id)
        {
            var tamp = _unitOfWork.Subscription.GetByIdAsync(id).Result;
            _unitOfWork.Subscription.Delete(tamp);
            return Ok(tamp);    
        }

    }
}
