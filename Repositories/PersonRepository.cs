using Microsoft.EntityFrameworkCore;
using PersonManagementAPI.Data;
using PersonManagementAPI.Models;

namespace PersonManagementAPI.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ApplicationDbContext _context;

        public PersonRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Person> GetAll() => _context.Persons.AsQueryable();

        public async Task<Person?> GetByIdAsync(int id)
        {
            return await _context.Persons
                                 .Include(p => p.PhoneNumbers)
                                 .Include(p => p.ConnectedPersons)
                                 .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddAsync(Person person) => await _context.Persons.AddAsync(person);

        public void Update(Person person) => _context.Persons.Update(person);

        public async Task DeleteAsync(int id)
        {
            var connectedPersons = _context.ConnectedPersons
                               .Where(cp => cp.ConnectedPersonId == id)
                               .ToList();

            _context.ConnectedPersons.RemoveRange(connectedPersons);
            await _context.SaveChangesAsync();

            var person = await _context.Persons.FindAsync(id);
            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();

        }

        public async Task UpdateProfilePictureAsync(int personId, string imagePath)
        {
            var person = await _context.Persons.FindAsync(personId);
            if (person != null)
            {
                person.ProfilePicture = imagePath;
                _context.Persons.Update(person);
            }
        }
    }
}
