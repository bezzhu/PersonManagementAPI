using static PersonManagementAPI.Enums.Enums;

namespace PersonManagementAPI.Services
{
    public interface IConnectedPersonService
    {
        Task<bool> AddConnectionAsync(int personId, int connectedPersonId, ConnectionType connectionType);
        Task<bool> RemoveConnectionAsync(int personId, int connectedPersonId, ConnectionType connectionType);

        
    }
}

