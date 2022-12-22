using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.ModelBinding.Binders;
using static Contracts.Class1;

namespace Main.Controllers
{
    [Route("api/realtycompanies")]
    [ApiController]
    public class RealtyCompaniesController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public RealtyCompaniesController(IRepositoryManager repository, ILoggerManager
        logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetRealtyCompanies()
        {
            var realtycompanies = await _repository.RealtyCompany.GetAllRealtyCompaniesAsync(trackChanges:
           false);
            var realtycompaniesDto = _mapper.Map<IEnumerable<RealtyCompanyDto>>(realtycompanies);
            return Ok(realtycompaniesDto);
        }
        [HttpGet("{id}", Name = "RealtyCompanyById")]
        public async Task<IActionResult> GetRealtyCompany(Guid id)
        {
            var realtycompany = await _repository.RealtyCompany.GetRealtyCompanyAsync(id, trackChanges: false);
            if (realtycompany == null)
            {
                _logger.LogInfo($"Company with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                var realtycompanyDto = _mapper.Map<RealtyCompanyDto>(realtycompany);
                return Ok(realtycompanyDto);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateRealtyCompany([FromBody] RealtyCompanyForCreationDto realtycompany)
        {
            if (realtycompany == null)
            {
                _logger.LogError("RealtyCompanyForCreationDto object sent from client is null.");
                return BadRequest("RealtyCompanyForCreationDto object is null");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the RealtyCompanyForCreationDto object");
                return UnprocessableEntity(ModelState);
            }
            var realtycompanyEntity = _mapper.Map<RealtyCompany>(realtycompany);
            _repository.RealtyCompany.CreateRealtyCompany(realtycompanyEntity);
            await _repository.SaveAsync();
            var realtycompanyToReturn = _mapper.Map<RealtyCompanyDto>(realtycompanyEntity);
            return CreatedAtRoute("RealtyCompanyById", new { id = realtycompanyToReturn.Id },
           realtycompanyToReturn);
        }
        [HttpGet("collection/({ids})", Name = "RealtyCompanyCollection")]
        public async Task<IActionResult> GetRealtyCompanyCollection(IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                _logger.LogError("Parameter ids is null");
                return BadRequest("Parameter ids is null");
            }
            var realtycompanyEntities = await _repository.RealtyCompany.GetByIdsAsync(ids, trackChanges: false);
            if (ids.Count() != realtycompanyEntities.Count())
            {
                _logger.LogError("Some ids are not valid in a collection");
                return NotFound();
            }
            var realtycompaniesToReturn =
           _mapper.Map<IEnumerable<RealtyCompanyDto>>(realtycompanyEntities);
            return Ok(realtycompaniesToReturn);
        }
        [HttpPost("collection")]
        public async Task<IActionResult> CreateRealtyCompanyCollection([FromBody] IEnumerable<RealtyCompanyForCreationDto> realtycompanyCollection)
        {
            if (realtycompanyCollection == null)
            {
                _logger.LogError("RealtyCompany collection sent from client is null.");
                return BadRequest("RealtyCompany collection is null");
            }
            var realtycompanyEntities = _mapper.Map<IEnumerable<RealtyCompany>>(realtycompanyCollection);
            foreach (var realtycompany in realtycompanyEntities)
            {
                _repository.RealtyCompany.CreateRealtyCompany(realtycompany);
            }
            await _repository.SaveAsync();
            var realtycompanyCollectionToReturn =
            _mapper.Map<IEnumerable<RealtyCompanyDto>>(realtycompanyEntities);
            var ids = string.Join(",", realtycompanyCollectionToReturn.Select(c => c.Id));
            return CreatedAtRoute("RealtyCompanyCollection", new { ids },
            realtycompanyCollectionToReturn);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRealtyCompany(Guid id)
        {
            var realtycompany = await _repository.RealtyCompany.GetRealtyCompanyAsync(id, trackChanges: false);
            if (realtycompany == null)
            {
                _logger.LogInfo($"RealtyCompany with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _repository.RealtyCompany.DeleteRealtyCompany(realtycompany);
            await _repository.SaveAsync();
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompany(Guid id, [FromBody] CompanyForUpdateDto company)
        {
            if (company == null)
            {
                _logger.LogError("CompanyForUpdateDto object sent from client is null.");
                return BadRequest("CompanyForUpdateDto object is null");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the CompanyForUpdateDto object");
                return UnprocessableEntity(ModelState);
            }
            var companyEntity = await _repository.Company.GetCompanyAsync(id,
           trackChanges:
            true);
            if (companyEntity == null)
            {
                _logger.LogInfo($"Company with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _mapper.Map(company, companyEntity);
            await _repository.SaveAsync();
            return NoContent();
        }
    }
}
