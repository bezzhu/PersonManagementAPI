using PersonManagementAPI.Data;

namespace PersonManagementAPI.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Persons = new PersonRepository(_context);
            ConnectedPersons = new ConnectedPersonRepository(_context);
        }

        public IPersonRepository Persons { get; }
        public IConnectedPersonRepository ConnectedPersons { get; }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
