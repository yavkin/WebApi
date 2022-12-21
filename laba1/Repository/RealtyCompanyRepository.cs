using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public void CreateRealtyCompany(RealtyCompany realtycompany)
        {
            throw new NotImplementedException();
        }
    }
}
