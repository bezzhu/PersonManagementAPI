using Microsoft.EntityFrameworkCore;
using PersonManagementAPI.Models;
using static PersonManagementAPI.Enums.Enums;

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

            modelBuilder.Entity<Person>().HasData(
                new Person { Id = -1, FirstName = "John", LastName = "Doe", PersonalNumber = "12345678901", BirthDate = new DateTime(1990, 1, 1), CityId = 1 },
                new Person { Id = -2, FirstName = "Jane", LastName = "Smith", PersonalNumber = "98765432101", BirthDate = new DateTime(1995, 5, 10), CityId = 2 }
            );

            modelBuilder.Entity<PhoneNumber>().HasData(
                new PhoneNumber { Id = -1, PersonId = -1, Number = "+123456789", PhoneType = PhoneType.Mobile },
                new PhoneNumber { Id = -2, PersonId = -2, Number = "+987654321", PhoneType = PhoneType.Home }
            );

            modelBuilder.Entity<ConnectedPerson>().HasData(
                new ConnectedPerson { Id = -1, PersonId = -1, ConnectedPersonId = -2, ConnectionType = ConnectionType.Colleague}
            );
        }
    }
}

