using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
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
            var realtycompany = _repository.RealtyCompany.GetRealtyCompanyAsync(realtycompanyId, trackChanges: false);
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
            var realtycompany = _repository.RealtyCompany.GetRealtyCompanyAsync(realtycompanyId, trackChanges: false);
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
        [HttpDelete("{id}")]
        public IActionResult DeleteClientForRealtyCompany(Guid realtycompanyId, Guid id)
        {
            var realtycompany = _repository.RealtyCompany.GetRealtyCompanyAsync(realtycompanyId, trackChanges: false);
            if (realtycompany == null)
            {
                _logger.LogInfo($"Company with id: {realtycompanyId} doesn't exist in the database.");
                return NotFound();
            }
            var clientForCompany = _repository.Client.GetClient(realtycompanyId, id,
            trackChanges: false);
            if (clientForCompany == null)
            {
                _logger.LogInfo($"Client with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _repository.Client.DeleteClient(clientForCompany);
            _repository.Save();
            return NoContent();
        }
        [HttpPut("{id}")]
        public IActionResult UpdateClientForCompany(Guid realtycompanyId, Guid id, [FromBody] ClientForUpdateDto client)
        {
            if (client == null)
            {
                _logger.LogError("ClientForUpdateDto object sent from client is null.");
                return BadRequest("ClientForUpdateDto object is null");
            }
            var realtycompany = _repository.RealtyCompany.GetRealtyCompanyAsync(realtycompanyId, trackChanges: false);
            if (realtycompany == null)
            {
                _logger.LogInfo($"RealtyCompany with id: {realtycompanyId} doesn't exist in the database.");
                return NotFound();
            }
            var clientEntity = _repository.Client.GetClient(realtycompanyId, id,
           trackChanges:
            true);
            if (clientEntity == null)
            {
                _logger.LogInfo($"Client with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _mapper.Map(client, clientEntity);
            _repository.Save();
            return NoContent();
        }
        [HttpPut("{id}")]
        public IActionResult UpdateRealtyCompany(Guid id, [FromBody] RealtyCompanyForUpdateDto realtycompany)
        {
            if (realtycompany == null)
            {
                _logger.LogError("RealtyCompanyForUpdateDto object sent from client is null.");
                return BadRequest("RealtyCompanyForUpdateDto object is null");
            }
            var realtycompanyEntity = _repository.RealtyCompany.GetRealtyCompanyAsync(id, trackChanges: true);
            if (realtycompanyEntity == null)
            {
                _logger.LogInfo($"RealtyCompany with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _mapper.Map(realtycompany, realtycompanyEntity);
            _repository.Save();
            return NoContent();
        }
        [HttpPatch("{id}")]
        public IActionResult PartiallyUpdateClientForCompany(Guid realtycompanyId, Guid id, [FromBody] JsonPatchDocument<ClientForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                _logger.LogError("patchDoc object sent from client is null.");
                return BadRequest("patchDoc object is null");
            }
            var realtycompany = _repository.RealtyCompany.GetRealtyCompanyAsync(realtycompanyId, trackChanges: false);
            if (realtycompany == null)
            {
                _logger.LogInfo($"RealtyCompany with id: {realtycompanyId} doesn't exist in the database.");
                return NotFound();
            }
            var clientEntity = _repository.Client.GetClient(realtycompanyId, id,
           trackChanges:
            true);
            if (clientEntity == null)
            {
                _logger.LogInfo($"Client with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var clientToPatch = _mapper.Map<ClientForUpdateDto>(clientEntity);
            patchDoc.ApplyTo(clientToPatch);
            _mapper.Map(clientToPatch, clientEntity);
            _repository.Save();
            return NoContent();
        }
    }
}
