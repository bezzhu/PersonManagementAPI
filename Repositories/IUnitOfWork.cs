

namespace PersonManagementAPI.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IPersonRepository Persons { get; }
        IConnectedPersonRepository ConnectedPersons { get; }
        Task CommitAsync(); 
    }
}

