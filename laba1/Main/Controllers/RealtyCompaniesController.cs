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
        [HttpGet("{id}", Name = "RealtyCompanyById")]
        public IActionResult GetRealtyCompany(Guid id)
        {
            var realtycompany = _repository.RealtyCompany.GetRealtyCompany(id, trackChanges: false);
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
        public IActionResult CreateRealtyCompany([FromBody] RealtyCompanyForCreationDto realtycompany)
        {
            if (realtycompany == null)
            {
                _logger.LogError("RealtyCompanyForCreationDto object sent from client is null.");
                return BadRequest("RealtyCompanyForCreationDto object is null");
            }
            var realtycompanyEntity = _mapper.Map<RealtyCompany>(realtycompany);
            _repository.RealtyCompany.CreateRealtyCompany(realtycompanyEntity);
            _repository.Save();
            var realtycompanyToReturn = _mapper.Map<RealtyCompanyDto>(realtycompanyEntity);
            return CreatedAtRoute("RealtyCompanyById", new { id = realtycompanyToReturn.Id },
            realtycompanyToReturn);
        }
        [HttpGet("collection/({ids})", Name = "RealtyCompanyCollection")]
        public IActionResult GetRealtyCompanyCollection(IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                _logger.LogError("Parameter ids is null");
                return BadRequest("Parameter ids is null");
            }
            var realtycompanyEntities = _repository.RealtyCompany.GetByIds(ids, trackChanges: false);
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
        public IActionResult CreateRealtyCompanyCollection([FromBody] IEnumerable<RealtyCompanyForCreationDto> realtycompanyCollection)
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
            _repository.Save();
            var realtycompanyCollectionToReturn =
            _mapper.Map<IEnumerable<RealtyCompanyDto>>(realtycompanyEntities);
            var ids = string.Join(",", realtycompanyCollectionToReturn.Select(c => c.Id));
            return CreatedAtRoute("RealtyCompanyCollection", new { ids },
            realtycompanyCollectionToReturn);
        }
    }
}
