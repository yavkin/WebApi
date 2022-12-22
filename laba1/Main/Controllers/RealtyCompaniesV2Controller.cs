using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Main.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/realtycompanies")]
    [ApiController]
    public class RealtyCompaniesV2Controller : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        public RealtyCompaniesV2Controller(IRepositoryManager repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetRealtyCompanies()
        {
            var realtycompanies = await
           _repository.RealtyCompany.GetAllRealtyCompaniesAsync(trackChanges:
            false);
            return Ok(realtycompanies);
        }
    }
}
