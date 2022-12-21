using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IRealtyCompanyRepository
    {
        IEnumerable<RealtyCompany> GetAllRealtyCompanies(bool trackChanges);
        RealtyCompany GetRealtyCompany(Guid realtycompanyId, bool trackChanges);
        public void CreateRealtyCompany(RealtyCompany realtycompany) => Create(realtycompany);
        void Create(RealtyCompany realtycompany);
        IEnumerable<RealtyCompany> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
    }
}
