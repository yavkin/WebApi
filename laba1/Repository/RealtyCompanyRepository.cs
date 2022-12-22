using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RealtyCompanyRepository : RepositoryBase<RealtyCompany>, IRealtyCompanyRepository
    {
        public RealtyCompanyRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }
        public void AnyMethodFromCompanyRepository()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<RealtyCompany> GetAllRealtyCompanies(bool trackChanges) =>
        FindAll(trackChanges)
            .OrderBy(c => c.Name)
            .ToList();
        public RealtyCompany GetRealtyCompany(Guid realtycompanyId, bool trackChanges) => FindByCondition(c
=> c.Id.Equals(realtycompanyId), trackChanges).SingleOrDefault();
        public IEnumerable<RealtyCompany> GetByIds(IEnumerable<Guid> ids, bool trackChanges) =>
FindByCondition(x => ids.Contains(x.Id), trackChanges).ToList();
        public void DeleteRealtyCompany(RealtyCompany realtycompany)
        {
            Delete(realtycompany);
        }
        public async Task<IEnumerable<RealtyCompany>> GetAllRealtyCompaniesAsync(bool trackChanges)
=> await FindAll(trackChanges)
 .OrderBy(c => c.Name)
 .ToListAsync();
        public async Task<RealtyCompany> GetRealtyCompanyAsync(Guid realtycompanyId, bool trackChanges) =>
         await FindByCondition(c => c.Id.Equals(realtycompanyId), trackChanges)
         .SingleOrDefaultAsync();
        public async Task<IEnumerable<RealtyCompany>> GetByIdsAsync(IEnumerable<Guid> ids, bool
        trackChanges) =>
         await FindByCondition(x => ids.Contains(x.Id), trackChanges)
         .ToListAsync();

        public void CreateRealtyCompany(RealtyCompany realtycompany)
        {
            throw new NotImplementedException();
        }
    }
}
