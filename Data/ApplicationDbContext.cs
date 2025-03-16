using Microsoft.EntityFrameworkCore;
using PersonManagementAPI.Models;

namespace PersonManagementAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Person> Persons { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public DbSet<ConnectedPerson> ConnectedPersons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PhoneNumber>()
                .HasOne(pn => pn.Person)
                .WithMany(p => p.PhoneNumbers)
                .HasForeignKey(pn => pn.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ConnectedPerson>()
                .HasOne(cp => cp.Person)
                .WithMany(p => p.ConnectedPersons)
                .HasForeignKey(cp => cp.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ConnectedPerson>()
                .HasOne(cp => cp.ConnectedTo)
                .WithMany()
                .HasForeignKey(cp => cp.ConnectedPersonId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Person>()
                .HasIndex(p => p.PersonalNumber)
                .IsUnique();
        }
    }
}
