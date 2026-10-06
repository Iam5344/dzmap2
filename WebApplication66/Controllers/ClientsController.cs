using AutoMapper;
using ClientManagementAPI.DTOs;
using ClientManagementAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClientManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IMapper _mapper;

        private static readonly List<Client> Clients = new List<Client>
        {
            new Client { Id = 1, FullName = "Іван Петренко", Email = "ivan@example.com", Phone = "+380501112233", RegistrationDate = DateTime.Now.AddDays(-10) },
            new Client { Id = 2, FullName = "Олена Коваль", Email = "olena@example.com", Phone = "+380672223344", RegistrationDate = DateTime.Now.AddDays(-5) },
            new Client { Id = 3, FullName = "Максим Сидоренко", Email = "maksim@example.com", Phone = "+380933334455", RegistrationDate = DateTime.Now.AddDays(-2) }
        };

        public ClientsController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetClients()
        {
            var clientDtos = _mapper.Map<IEnumerable<ClientReadDto>>(Clients);
            return Ok(clientDtos);
        }

        [HttpGet("{id:int}", Name = "GetClientById")]
        public IActionResult GetClientById(int id)
        {
            var client = Clients.FirstOrDefault(c => c.Id == id);
            if (client == null)
            {
                return NotFound("Client not found");
            }

            var clientDto = _mapper.Map<ClientReadDto>(client);
            return Ok(clientDto);
        }

        [HttpPost]
        public IActionResult CreateClient([FromBody] ClientCreateDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.FullName) || string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Phone))
            {
                return BadRequest("Поля 'FullName', 'Email' та 'Phone' є обов'язковими.");
            }

            var clientModel = _mapper.Map<Client>(dto);
            
            int newId = Clients.Count > 0 ? Clients.Max(c => c.Id) + 1 : 1;
            clientModel.Id = newId;
            clientModel.RegistrationDate = DateTime.Now;

            Clients.Add(clientModel);

            var clientReadDto = _mapper.Map<ClientReadDto>(clientModel);

            return CreatedAtRoute("GetClientById", new { id = clientReadDto.Id }, clientReadDto);
        }
    }
}
