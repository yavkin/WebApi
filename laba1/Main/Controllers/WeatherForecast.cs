using Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using static Contracts.Class1;
using Repository;

[Route("[controller]")]
[ApiController]
public class WeatherForecastController : ControllerBase
{
    private readonly IRepositoryManager _repository;
    public WeatherForecastController(IRepositoryManager repository)
    {
        _repository = repository;
    }
    [HttpGet]
    public ActionResult<IEnumerable<string>> Get()
    {
        _repository.Company.AnyMethodFromCompanyRepository();
        _repository.Employee.AnyMethodFromEmployeeRepository();
        return new string[] { "value1", "value2" };
    }
}