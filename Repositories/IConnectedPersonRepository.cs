using PersonManagementAPI.DTOs;
using PersonManagementAPI.Models;
using static PersonManagementAPI.Enums.Enums;
namespace PersonManagementAPI.Repositories
{
    public interface IConnectedPersonRepository
    {
        Task AddAsync(ConnectedPerson connectedPerson);
        Task RemoveAsync(int personId, int connectedPersonId, ConnectionType connectionType);
        Task<List<ConnectedPerson>> GetConnectedPersonsByPersonIdAsync(int personId);
        Task<List<ConnectionReportDTO>> GetConnectionReportAsync();
        IQueryable<ConnectedPerson> GetAll();
    }
}
