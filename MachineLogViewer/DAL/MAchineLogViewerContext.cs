using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using MachineLogViewer.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MachineLogViewer.DAL
{
    public class MachineLogViewerContext : DbContext
    {
        public MachineLogViewerContext()
            : base("DefaultConnection")
        {
        }
        
        public DbSet<Machine> Machines { get; set; }
        public DbSet<LogEntry> LogEntries { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}