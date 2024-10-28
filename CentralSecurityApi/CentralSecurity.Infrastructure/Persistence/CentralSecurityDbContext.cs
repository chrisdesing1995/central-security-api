using CentralSecurity.Domain.Common;
using CentralSecurity.Domain.Dto;
using CentralSecurity.Domain.Entities;
using CentralSecurity.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace CentralSecurity.Infrastructure.Persistence
{
    public class CentralSecurityDbContext : DbContext
    {

        public CentralSecurityDbContext(DbContextOptions<CentralSecurityDbContext> dbContext)
        : base(dbContext)
        {

        }

        #region Module Security
        public DbSet<ResultSp> ResultSp { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Menu> Menus { get; set; }
        #endregion

        public DbSet<UserLoginDto> Login { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ResultSp>().HasNoKey();
        }
    }
}
