using AutoMapper;
using InvoiceManagement.API.DTOs;
using InvoiceManagement.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoicesController : ControllerBase
    {
        private readonly IMapper _mapper;

        private static readonly List<Invoice> Invoices = new List<Invoice>
        {
            new Invoice { Id = 1, Number = "INV-2026-001", ClientName = "ТОВ 'Вектор'", Total = 12500.00m, CreatedAt = DateTime.Now.AddDays(-10) },
            new Invoice { Id = 2, Number = "INV-2026-002", ClientName = "ФОП Іванов", Total = 4300.50m, CreatedAt = DateTime.Now.AddDays(-5) },
            new Invoice { Id = 3, Number = "INV-2026-003", ClientName = "ПП 'Гарант'", Total = 8900.00m, CreatedAt = DateTime.Now.AddDays(-1) }
        };

        public InvoicesController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetInvoices()
        {
            var invoiceDtos = _mapper.Map<IEnumerable<InvoiceReadDto>>(Invoices);
            return Ok(invoiceDtos);
        }

        [HttpGet("{id:int}", Name = "GetInvoiceById")]
        public IActionResult GetInvoiceById(int id)
        {
            var invoice = Invoices.FirstOrDefault(i => i.Id == id);
            if (invoice == null)
            {
                return NotFound("Invoice not found");
            }

            var invoiceDto = _mapper.Map<InvoiceReadDto>(invoice);
            return Ok(invoiceDto);
        }

        [HttpPost]
        public IActionResult CreateInvoice([FromBody] InvoiceCreateDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.ClientName) || dto.Total <= 0)
            {
                return BadRequest("Поле 'ClientName' є обов'язковим, а 'Total' має бути більше 0.");
            }

            var invoiceModel = _mapper.Map<Invoice>(dto);

            int newId = Invoices.Count > 0 ? Invoices.Max(i => i.Id) + 1 : 1;
            invoiceModel.Id = newId;
            invoiceModel.Number = $"INV-2026-{newId:D3}";
            invoiceModel.CreatedAt = DateTime.Now;

            Invoices.Add(invoiceModel);

            var invoiceReadDto = _mapper.Map<InvoiceReadDto>(invoiceModel);

            return CreatedAtRoute("GetInvoiceById", new { id = invoiceModel.Id }, invoiceReadDto);
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateInvoice(int id, [FromBody] InvoiceUpdateDto dto)
        {
            var existingInvoice = Invoices.FirstOrDefault(i => i.Id == id);
            if (existingInvoice == null)
            {
                return NotFound("Invoice not found");
            }

            if (dto == null || dto.Total <= 0)
            {
                return BadRequest("Сума 'Total' має бути більше 0.");
            }

            _mapper.Map(dto, existingInvoice);

            var invoiceReadDto = _mapper.Map<InvoiceReadDto>(existingInvoice);
            return Ok(invoiceReadDto);
        }
    }
}
