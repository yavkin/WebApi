using Contracts;
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
    public class ClientRepository : RepositoryBase<Client>, IClientRepository
    {
        public ClientRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }
        public async Task<PagedList<Client>> GetClientsAsync(Guid realtycompanyId, ClientParameters clientParameters, bool trackChanges)
        {
            var clients = await FindByCondition(e => e.RealtyCompanyId.Equals(realtycompanyId) &&
           (e.Age
            >= clientParameters.MinAge && e.Age <= clientParameters.MaxAge),
            trackChanges)
            .OrderBy(e => e.Name)
            .ToListAsync();
            return PagedList<Client>
            .ToPagedList(clients, clientParameters.PageNumber,
            clientParameters.PageSize);
        }
        public void DeleteClient(Client client)
        {
            Delete(client);
        }
        Task<IEnumerable<Client>> IClientRepository.GetClientsAsync(Guid realtycompanyId, ClientParameters clientParameters, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public Task<Client> GetClientAsync(Guid realtycompanyId, Guid id, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public void CreateClientForCompany(Guid realtycompanyId, Client client)
        {
            throw new NotImplementedException();
        }
    }
}
