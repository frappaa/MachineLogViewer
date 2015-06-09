using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MachineLogViewer.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Machine> Machines { get; set; }
        public DbSet<LogEntry> LogEntries { get; set; }
        public DbSet<Takings> Takings { get; set; }
        public DbSet<LogDescription> LogDescriptions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
            .Entity<Machine>()
            .Property(t => t.Code)
            .IsRequired()
            .HasMaxLength(12)
            .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute {IsUnique = true}));

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}