using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using MachineLogViewer.Models;

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
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}