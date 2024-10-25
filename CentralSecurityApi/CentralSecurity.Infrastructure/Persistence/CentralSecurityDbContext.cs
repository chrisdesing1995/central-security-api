using CentralSecurity.Domain.Entities;
using CentralSecurity.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace CentralSecurity.Infrastructure.Persistence
{
    public class CentralSecurityDbContext : DbContext
    {

        public CentralSecurityDbContext(DbContextOptions<CentralSecurityDbContext> dbContext)
        : base(dbContext)
        {

        }

        #region Module Security
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Menu> Menus { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //modelBuilder.Entity<ProductSPCodeDto>().HasNoKey();
            base.OnModelCreating(modelBuilder);

        }
    }
}
