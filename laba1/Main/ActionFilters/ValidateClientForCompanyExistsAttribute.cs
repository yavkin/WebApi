using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Contracts.Class1;

namespace Main.ActionFilters
{
    public class ValidateClientForCompanyExistsAttribute : IAsyncActionFilter
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        public ValidateClientForCompanyExistsAttribute(IRepositoryManager repository,
        ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var method = context.HttpContext.Request.Method;
            var trackChanges = (method.Equals("PUT") || method.Equals("PATCH")) ?
           true : false;
            var realtycompanyId = (Guid)context.ActionArguments["realtycompanyId"];
            var realtycompany = await _repository.RealtyCompany.GetRealtyCompanyAsync(realtycompanyId,
           false);
            if (realtycompany == null)
            {
                _logger.LogInfo($"RealtyCompany with id: {realtycompanyId} doesn't exist in the database.");
                return;
                context.Result = new NotFoundResult();
            }
            var id = (Guid)context.ActionArguments["id"];
            var client = await _repository.Client.GetClientAsync(realtycompanyId, id, trackChanges);
            if (client == null)
            {
                _logger.LogInfo($"Client with id: {id} doesn't exist in the database.");

                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("Client", client);
                await next();
            }
        }
    }
}
