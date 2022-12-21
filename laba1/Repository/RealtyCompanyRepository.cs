using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class RealtyCompanyRepository : RepositoryBase<RealtyCompany>, IRealtyCompanyRepository
    {
        public RealtyCompanyRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }
    }
}
