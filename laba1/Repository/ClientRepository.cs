using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class ClientRepository : RepositoryBase<Client>, IClientRepository
    {
        public ClientRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }
        public IEnumerable<Client> GetClients(Guid realtycompanyId, bool trackChanges) =>
FindByCondition(e => e.RealtyCompanyId.Equals(realtycompanyId), trackChanges)
.OrderBy(e => e.Name);
        public Client GetClient(Guid realtycompanyId, Guid id, bool trackChanges) =>
FindByCondition(e => e.RealtyCompanyId.Equals(realtycompanyId) && e.Id.Equals(id),
trackChanges).SingleOrDefault();
        public void DeleteClient(Client client)
        {
            Delete(client);
        }

        public void CreateClientForCompany(Guid realtycompanyId, Client client)
        {
            throw new NotImplementedException();
        }
    }
}
