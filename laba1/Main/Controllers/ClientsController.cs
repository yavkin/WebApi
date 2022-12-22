using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using Main.ActionFilters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        [HttpGet]
        public async Task<IActionResult> GetClientsForCompany(Guid realtycompanyId, [FromQuery] ClientParameters clientParameters)
        {
            if (!clientParameters.ValidAgeRange)
                return BadRequest("Max age can't be less than min age.");
            var realtycompany = await _repository.RealtyCompany.GetRealtyCompanyAsync(realtycompanyId, trackChanges: false);
            if (realtycompany == null)
            {
                _logger.LogInfo($"RealtyCompany with id: {realtycompanyId} doesn't exist in the database.");
                return NotFound();
            }
            var clientsFromDb = await _repository.Client.GetClientsAsync(realtycompanyId,
            clientParameters, trackChanges: false);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(clientsFromDb));
            var clientsDto = _mapper.Map<IEnumerable<EmployeeDto>>(clientsFromDb);
            return Ok(clientsDto);
        }
        [HttpPost]
        public IActionResult CreateClientForCompany(Guid realtycompanyId, [FromBody] ClientForCreationDto client)
        {
            if (client == null)
            {
                _logger.LogError("ClientForCreationDto object sent from client is null.");
                return BadRequest("ClientForCreationDto object is null");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the patch document");
                return UnprocessableEntity(ModelState);
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
        [ServiceFilter(typeof(ValidateClientForCompanyExistsAttribute))]
        public async Task<IActionResult> DeleteClientForCompany(Guid realtycompanyId, Guid id)
        {
            var clientForCompany = HttpContext.Items["client"] as Client;
            _repository.Client.DeleteClient(clientForCompany);
            await _repository.SaveAsync();
            return NoContent();
        }
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateClientForCompanyExistsAttribute))]
        public async Task<IActionResult> UpdateClientForCompany(Guid realtycompanyId, Guid id, [FromBody] ClientForUpdateDto client)
        {
            var clientEntity = HttpContext.Items["client"] as Client;
            _mapper.Map(client, clientEntity);
            await _repository.SaveAsync();
            return NoContent();
        }
        [HttpPatch("{id}")]
        [ServiceFilter(typeof(ValidateClientForCompanyExistsAttribute))]
        public async Task<IActionResult> PartiallyUpdateClientForCompany(Guid realtycompanyId, Guid id, [FromBody] JsonPatchDocument<ClientForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                _logger.LogError("patchDoc object sent from client is null.");
                return BadRequest("patchDoc object is null");
            }
            var clientEntity = HttpContext.Items["client"] as Client;
            var clientToPatch = _mapper.Map<ClientForUpdateDto>(clientEntity);
            patchDoc.ApplyTo(clientToPatch, ModelState);
            TryValidateModel(clientToPatch);
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the patch document");
                return UnprocessableEntity(ModelState);
            }
            _mapper.Map(clientToPatch, clientEntity);
            await _repository.SaveAsync();
            return NoContent();
        }
    }
}
