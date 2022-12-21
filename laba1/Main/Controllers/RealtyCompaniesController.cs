using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
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
        [HttpGet]
        public IActionResult GetRealtyCompanies()
        {
            var realtycompanies = _repository.RealtyCompany.GetAllRealtyCompanies(trackChanges: false);
            var realtycompaniesDto = _mapper.Map<IEnumerable<RealtyCompanyDto>>(realtycompanies);
            return Ok(realtycompaniesDto);
        }
    }
}
