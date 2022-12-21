using Entities.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Configuration
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasData
            (
                new Client
                {
                    Id = new Guid("34ghsda5-364s-8n11-v2nm-147379368g4m"),
                    Name = "Christian Bale",
                    Age = 49,
                    ClientId = new Guid("g2jld532-49b6-410c-bc78-2d54a9996890")
                },
                new Client
                {
                    Id = new Guid("32jba8c0-d178-41e7-938c-ef8678fb53s"),
                    Name = "Bradley Pitt",
                    Age = 59,
                    ClientId = new Guid("g2jld532-49b6-410c-bc78-2d54a9996890")
                },
                new Client
                {
                    Id = new Guid("321fa7c1-0gha-4jfd-ae34-2138a9963481"),
                    Name = "Cillian Murphy",
                    Age = 46,
                    ClientId = new Guid("1n322s60-94ce-4d15-9494-4328844c2ve7")
                },
                new Client
                {
                    Id = new Guid("951fj5n3-1dkb-4aad-al34-2449a8439811"),
                    Name = "Matthew McConaughey",
                    Age = 53,
                    ClientId = new Guid("1n322s60-94ce-4d15-9494-4328844c2ve7")
                }
            );
        }
    }
}