using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShaTask.DTOs;
using ShaTask.Interfaces;
using ShaTask.Models;
using ShaTask.Services;

namespace ShaTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceDataController : ControllerBase
    {
        private IInvoiceService invoiceService;
        public InvoiceDataController(IInvoiceService invoiceService)
        {
            this.invoiceService = invoiceService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InvoiceDataDTO>>> GetAll()
        {
            var invoiceDataDTOs = await invoiceService.GetAllInvoiceDataAsync();

            return Ok(invoiceDataDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InvoiceDataDTO>> GetById(long id)
        {
            var invoiceDataDTO = await invoiceService.GetInvoiceDataByIdAsync(id);
            if(invoiceDataDTO == null) { return NotFound(); }
            return Ok(invoiceDataDTO);
        }

        [HttpPost]
        public async Task<ActionResult> AddInvoice(NewInvoiceDTO invoiceDataDTO)
        {
            if (invoiceDataDTO == null) { return BadRequest(); }

            await invoiceService.MappingDTOToInvoiceAsync(invoiceDataDTO);
            return Created();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteInvoice(long id)
        {

            await invoiceService.DeleteInvoiceAsync(id);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateInvoice(UpdateInvoiceDataDTO updateInvoiceDataDTO)
        {
            var invoiceDataDTO = await invoiceService.GetInvoiceDataByIdAsync(updateInvoiceDataDTO.InvoiceHeaderId);
            if (invoiceDataDTO == null) { return NotFound(); }

            await invoiceService.UpdateInvoiceAsync(updateInvoiceDataDTO);
            return CreatedAtAction(nameof(GetById), new { id = updateInvoiceDataDTO.InvoiceHeaderId }, updateInvoiceDataDTO);
        }

        

    }
}
