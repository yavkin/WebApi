using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryManager
    {
        ICompanyRepository Company { get; }
        IEmployeeRepository Employee { get; }
        IRealtyCompanyRepository RealtyCompany { get; }
        IClientRepository Client { get; }
        void Save();
        public Task SaveAsync() => _repositoryContext.SaveChangesAsync();
    }

    internal class _repositoryContext
    {
        internal static Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
