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
        void CreateRealtyCompany(RealtyCompany realtycompany);
        void DeleteRealtyCompany(RealtyCompany realtycompany);
        IEnumerable<RealtyCompany> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
    }
}
