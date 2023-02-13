﻿using Contracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repositoryContext;
        private ICompanyRepository _companyRepository;
        private IEmployeeRepository _employeeRepository;
        private IRealtyCompanyRepository _realtycompanyRepository;
        private IClientRepository _clientRepository;
        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }
        public ICompanyRepository Company
        {
            get
            {
                if (_companyRepository == null)
                    _companyRepository = new CompanyRepository(_repositoryContext);
                return _companyRepository;
            }
        }
        public IEmployeeRepository Employee
        {
            get
            {
            if (_employeeRepository == null)
                    _employeeRepository = new EmployeeRepository(_repositoryContext);
                return _employeeRepository;
            }
        }
        public IRealtyCompanyRepository RealtyCompany
        {
            get
            {
                if (_realtycompanyRepository == null)
                    _realtycompanyRepository = new RealtyCompanyRepository(_repositoryContext);
                return _realtycompanyRepository;
            }
        }
        public IClientRepository Client
        {
            get
            {
                if (_clientRepository == null)
                    _clientRepository = new ClientRepository(_repositoryContext);
                return _clientRepository;
            }
        }
        public void Save() => _repositoryContext.SaveChanges();
    }
}
