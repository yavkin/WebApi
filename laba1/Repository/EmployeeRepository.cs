﻿using Contracts;
using Entities;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }
        public async Task<PagedList<Employee>> GetEmployeesAsync(Guid companyId, EmployeeParameters employeeParameters, bool trackChanges)
        {
            var employees = await FindByCondition(e => e.CompanyId.Equals(companyId) &&
           (e.Age
            >= employeeParameters.MinAge && e.Age <= employeeParameters.MaxAge),
            trackChanges)
            .OrderBy(e => e.Name)
            .ToListAsync();
            return PagedList<Employee>
            .ToPagedList(employees, employeeParameters.PageNumber,
            employeeParameters.PageSize);
        }
        public void DeleteEmployee(Employee employee)
        {
            Delete(employee);
        }

        public void CreateEmployeeForCompany(Guid companyId, Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
