using Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using static Contracts.Class1;

[Route("[controller]")]
[ApiController]
public class WeatherForecastController : ControllerBase {
    private ILoggerManager _logger;
    public WeatherForecastController(ILoggerManager logger) {
        _logger = logger;
    }
    [HttpGet]
    public IEnumerable<string> Get() {
        _logger.LogInfo("Вот информационное сообщение от нашего контроллера значений.");
        _logger.LogDebug("Вот отладочное сообщение от нашего контроллера значений.");
        _logger.LogWarn("Вот сообщение предупреждения от нашего контроллера значений.");
        _logger.LogError("Вот сообщение об ошибке от нашего контроллера значений.");
    return new string[] { "value1", "value2" };
    }
    private readonly IRepositoryManager _repository;
    public WeatherForecastController(IRepositoryManager repository)
    {
        repository = repository;
    }
    [HttpGet]
    public ActionResult<IEnumerable<string>> Get()
    {
        repository.Company.AnyMethodFromCompanyRepository();
        _repository.Employee.AnyMethodFromEmployeeRepository();
        return new string[] { "value1", "value2" };
    }
}