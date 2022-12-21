﻿using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IClientRepository
    {
        IEnumerable<Client> GetClients(Guid realtycompanyId, bool trackChanges);
        Client GetClient(Guid realtycompanyId, Guid id, bool trackChanges);
    }
}
