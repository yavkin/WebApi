using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IRealtyCompanyRepository
    {
        IEnumerable<RealtyCompany> GetAllRealtyCompanies(bool trackChanges);
        void AnyMethodFromRealtyCompanyRepository();
    }
}
