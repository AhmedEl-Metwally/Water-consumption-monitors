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
    public class InvoiceController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;  
        
        public InvoiceController (IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;   
            _mapper = mapper;   
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var Invoice = await _unitOfWork.Invoice.GetByIdAsync(id);
            var date = _mapper.Map<InvoiceDto>(Invoice);
            return Ok(date);    
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var Invoice = await _unitOfWork.Invoice.GetAllAsync();
            var date = _mapper.Map<IEnumerable<InvoiceDto>>(Invoice);
            return Ok(date);    
        }

        [HttpPost("Add")]
        public IActionResult Add(InvoiceDto dto)
        {
            var date = _mapper.Map<Invoice>(dto);
            var Invoice = _unitOfWork.Invoice.Add(date);
            _unitOfWork.Compelete();
            return Ok(date);    
        }

        [HttpPut("Update")]
        public IActionResult Update(InvoiceDto dto)
        {
            var date = _mapper.Map<Invoice>(dto);
            var Invoice = _unitOfWork.Invoice.Add(date);
            _unitOfWork.Compelete();
            return Ok(date);    
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(int id) 
        {
            var tamp = _unitOfWork.Invoice.GetByIdAsync(id).Result;
            _unitOfWork.Invoice.Delete(tamp);
            return Ok(tamp);    
        }

    }
}
