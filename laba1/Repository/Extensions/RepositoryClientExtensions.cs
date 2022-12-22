using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic.Core;
using System.Reflection;

namespace Repository.Extensions
{
    public static class RepositoryClientExtensions
    {
        public static IQueryable<Client> Sort(this IQueryable<Client> clients, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return client.OrderBy(e => e.Name);
            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(Client).GetProperties(BindingFlags.Public |
            BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();
            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;
                var propertyFromQueryName = param.Split(" ")[0];
                var objectProperty = propertyInfos.FirstOrDefault(pi =>
                pi.Name.Equals(propertyFromQueryName,
               StringComparison.InvariantCultureIgnoreCase));
                if (objectProperty == null)
                    continue;
                var direction = param.EndsWith(" desc") ? "descending" : "ascending";
                orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {direction}, ");
            }
            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' '); if
           (string.IsNullOrWhiteSpace(orderQuery))
                return clients.OrderBy(e => e.Name);
            return clients.OrderBy(orderQuery);
        }
    }
}
