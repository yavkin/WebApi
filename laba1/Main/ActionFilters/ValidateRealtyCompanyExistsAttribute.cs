﻿using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Contracts.Class1;

namespace Main.ActionFilters
{
    public class ValidateRealtyCompanyExistsAttribute : IAsyncActionFilter
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        public ValidateRealtyCompanyExistsAttribute(IRepositoryManager repository,
        ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context,
        ActionExecutionDelegate next)
        {
            var trackChanges = context.HttpContext.Request.Method.Equals("PUT");
            var id = (Guid)context.ActionArguments["id"];
            var realtycompany = await _repository.RealtyCompany.GetRealtyCompanyAsync(id,
           trackChanges);
            if (realtycompany == null)
            {
                _logger.LogInfo($"RealtyCompany with id: {id} doesn't exist in the database.");
                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("realtycompany", realtycompany);
                await next();
            }
        }
    }
}
