using Microsoft.Extensions.Options;
using ClientRegistryAPI.Models.Domain;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ClientRegistryAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; private set; } = null!;
        public DbSet<Audit> Audits { get; private set; } = null!;


        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
