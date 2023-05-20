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
    public class SubscriberController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SubscriberController (IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;   
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var Subscriber = await _unitOfWork.Subscriber.GetByIdAsync(id);
            var data = _mapper.Map<SubscriberDto>(Subscriber);
            return Ok(data);    
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var Subscriber = await _unitOfWork.Subscriber.GetAllAsync();
            var data = _mapper.Map<IEnumerable<SubscriberDto>>(Subscriber);
            return Ok(data);    
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(SubscriberDto dto)
        {
            var data = _mapper.Map<Subscriber>(dto);
            var Subscriber = _unitOfWork.Subscriber.Add(data);
            _unitOfWork.Compelete();
            return Ok(data);    
        }

        [HttpPut("Update")]
        public IActionResult Update(SubscriberDto dto)
        {
            var data = _mapper.Map<Subscriber>(dto);
            var Subscriber = _unitOfWork.Subscriber.Add(data);
            _unitOfWork.Compelete();
            return Ok(data); 
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(int id)
        {
            var tamp = _unitOfWork.Subscriber.GetByIdAsync(id).Result;
            _unitOfWork.Subscriber.Delete(tamp);
            return Ok(tamp);    
        }

    }
}
