using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Contracts.Class1;

namespace Main.Controllers
{
    [Route("api/realtycompanies/{realtycompanyId}/clients")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public ClientsController(IRepositoryManager repository, ILoggerManager
       logger,
        IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet("{id}", Name = "GetClientForCompany")]
        public IActionResult GetClientForCompany(Guid realtycompanyId, Guid id)
        {
            var realtycompany = _repository.RealtyCompany.GetRealtyCompany(realtycompanyId, trackChanges: false);
            if (realtycompany == null)
            {
                _logger.LogInfo($"Company with id: {realtycompanyId} doesn't exist in the database.");
                return NotFound();
            }
            var clientDb = _repository.Client.GetClient(realtycompanyId, id,
           trackChanges:
            false);
            if (clientDb == null)
            {
                _logger.LogInfo($"Employee with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var client = _mapper.Map<ClientDto>(clientDb);
            return Ok(client);
        }
        [HttpPost]
        public IActionResult CreateClientForCompany(Guid realtycompanyId, [FromBody] ClientForCreationDto client)
        {
            if (client == null)
            {
                _logger.LogError("ClientForCreationDto object sent from client is null.");
                return BadRequest("ClientForCreationDto object is null");
            }
            var realtycompany = _repository.RealtyCompany.GetRealtyCompany(realtycompanyId, trackChanges: false);
            if (realtycompany == null)
            {
                _logger.LogInfo($"Company with id: {realtycompanyId} doesn't exist in the database.");
                return NotFound();
            }
            var clientEntity = _mapper.Map<Client>(client);
            _repository.Client.CreateClientForCompany(realtycompanyId, clientEntity);
            _repository.Save();
            var clientToReturn = _mapper.Map<ClientDto>(clientEntity);
            return CreatedAtRoute("GetClientForCompany", new
            {
                realtycompanyId,
                id = clientToReturn.Id
            }, clientToReturn);
        }
    }
}
