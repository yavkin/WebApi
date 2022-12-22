using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRealtyCompanyRepository
    {
        Task<IEnumerable<RealtyCompany>> GetAllRealtyCompaniesAsync(bool trackChanges);
        Task<RealtyCompany> GetRealtyCompanyAsync(Guid realtycompanyId, bool trackChanges);
        void CreateRealtyCompany(RealtyCompany realtycompany);
        Task<IEnumerable<RealtyCompany>> GetByIdsAsync(IEnumerable<Guid> ids, bool
       trackChanges);
        void DeleteRealtyCompany(RealtyCompany realtycompany);
    }
}
