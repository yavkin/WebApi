using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Configuration
{
    public class RealtyCompanyConfiguration : IEntityTypeConfiguration<RealtyCompany>
    {
        public void Configure(EntityTypeBuilder<RealtyCompany> builder)
        {
            builder.HasData
            (
                new RealtyCompany
                {
                    Id = new Guid("g2jld532-49b6-410c-bc78-2d54a9996890"),
                    Name = "Doorstead",
                    Location = "San Francisco, California, United States",
                    Country = "USA"
                },
                new RealtyCompany
                {
                    Id = new Guid("1n322s60-94ce-4d15-9494-4328844c2ve7"),
                    Name = "Incitu",
                    Location = "New York, New York, United States",
                    Country = "USA"
                }
            );
        }
    }
}