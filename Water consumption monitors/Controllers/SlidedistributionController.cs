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
    public class SlidedistributionController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SlidedistributionController (IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("GetAllById")]
        public async Task<IActionResult> GetAllByIdAsync(int id)
        {
            var Slidedistribution = await _unitOfWork.Slidedistribution.GetByIdAsync(id);
            var date = _mapper.Map<SlidedistributionDto>(Slidedistribution);
            return Ok(date);    
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var Slidedistribution = await _unitOfWork.Slidedistribution.GetAllAsync();
            var date = _mapper.Map<IEnumerable<SlidedistributionDto>>(Slidedistribution);
            return Ok(date);    
        }

        [HttpPost("Add")]
        public IActionResult Add(SlidedistributionDto dto) 
        {
            var date = _mapper.Map<Slidedistribution>(dto);
            var Slidedistribution = _unitOfWork.Slidedistribution.Add(date);
            _unitOfWork.Compelete();
            return Ok(date);    
        }

        [HttpPut("Update")]
        public IActionResult Update(SlidedistributionDto dto)
        {
            var date = _mapper.Map<Slidedistribution>(dto);
            var Slidedistribution = _unitOfWork.Slidedistribution.Add(date);
            _unitOfWork.Compelete();
            return Ok(date);    
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(int id)
        {
            var tamp = _unitOfWork.Slidedistribution.GetByIdAsync(id).Result;
            _unitOfWork.Slidedistribution.Delete(tamp);
            return Ok(tamp);
        }

    }
}
