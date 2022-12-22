using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetClientsAsync(Guid realtycompanyId, ClientParameters clientParameters, bool trackChanges);
        Task<Client> GetClientAsync(Guid realtycompanyId, Guid id, bool trackChanges);
        void CreateClientForCompany(Guid realtycompanyId, Client client);
        void DeleteClient(Client client);
    }
}
