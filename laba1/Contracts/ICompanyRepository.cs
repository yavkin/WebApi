using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface ICompanyRepository
    {
        IEnumerable<Company> GetAllCompanies(bool trackChanges);
        Company GetCompany(Guid companyId, bool trackChanges);
        public void CreateCompany(Company company) => Create(company);
        IEnumerable<Company> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
    }
}
