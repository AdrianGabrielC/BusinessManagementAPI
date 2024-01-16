using GlanzCleanAPI.PresentationLayer.DataTransferObjects.EmployeesDTOs;
using GlanzCleanAPI.PresentationLayer.DataTransferObjects.InvoiceDTOs;
using GlanzCleanAPI.ServiceLayer.ServiceManager;
using GlanzCleanAPI.Utilities.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GlanzCleanAPI.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public InvoicesController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        // GET: api/Invoices
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> GetInvoices([FromQuery] InvoiceParameters invoiceParameters)
        {
            var pagedResult = await _serviceManager.InvoiceService.GetInvoicesAsync<InvoiceDto>(invoiceParameters, false);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

            return Ok(pagedResult.invoices);

        }

        // GET api/<InvoicesController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> GetInvoice(Guid id)
        {
            var invoice = await _serviceManager.InvoiceService.GetInvoiceByIdAsync<InvoiceDto>(id, false);

            return Ok(invoice);
        }

        // POST api/<InvoicesController>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> PostInvoice([FromBody] InvoicePostDto invoice)
        {
            if (invoice is null) return BadRequest("Invoice object is null");
            if (!ModelState.IsValid) return UnprocessableEntity(ModelState);

            var createdInvoice = await _serviceManager.InvoiceService.CreateInvoiceAsync(invoice);

            return CreatedAtAction("GetInvoice", new { id = createdInvoice.Id }, createdInvoice);

        }

        // PUT api/<InvoicesController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutInvoice(Guid id, [FromBody] InvoicePutDto invoice)
        {
            if (invoice is null) return BadRequest("Invoice parameter is null");

            await _serviceManager.InvoiceService.UpdateInvoiceAsync(id, invoice, trackChanges: true);

            return NoContent();
        }

        // DELETE api/<InvoicesController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteInvoice(Guid id)
        {
            await _serviceManager.InvoiceService.DeleteInvoiceAsync(id);

            return NoContent();
        }
    }
}
