using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShaTask.DTOs;
using ShaTask.Interfaces;

namespace ShaTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CashierController : ControllerBase
    {
        private ICashierService cashierService;
        public CashierController(ICashierService cashierService)
        {
            this.cashierService = cashierService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CashierDTO>>> GetAll()
        {
            var cashierDTOs = await cashierService.GetAllCashierAsync();
            return Ok(cashierDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CashierDTO>> GetById(int id)
        {
            var cashierDTO = await cashierService.GetCashierByIdAsync(id);
            if (cashierDTO == null) { return NotFound(); }

            return Ok(cashierDTO);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteById(int id)
        {
            var cashierDTO = await cashierService.GetCashierByIdAsync(id);
            if (cashierDTO == null) { return  BadRequest(); }

            await cashierService.DeleteCashierAsync(id);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> Add(NewCashierDTO cashierDTO)
        {
            await cashierService.AddCashierAsync(cashierDTO);
            return Created();
        }

        [HttpPut]
        public async Task<ActionResult> Update(UpdateCashierDTO cashierDTO)
        {
            await cashierService.UpdateCashierAsync(cashierDTO);
            return CreatedAtAction(nameof(GetById), new { id = cashierDTO.Id }, cashierDTO);
        }
    }
}
