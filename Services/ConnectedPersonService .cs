using PersonManagementAPI.Models;
using PersonManagementAPI.Repositories;
using static PersonManagementAPI.Enums.Enums;

namespace PersonManagementAPI.Services
{
    public class ConnectedPersonService : IConnectedPersonService
    {
        private readonly IConnectedPersonRepository _connectedPersonRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ConnectedPersonService(
            IConnectedPersonRepository connectedPersonRepository,
            IUnitOfWork unitOfWork)
        {
            _connectedPersonRepository = connectedPersonRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddConnectionAsync(int personId, int connectedPersonId, ConnectionType connectionType)
        {
            var connectedPersonEntity = new ConnectedPerson
            {
                PersonId = personId,
                ConnectedPersonId = connectedPersonId,
                ConnectionType = connectionType
            };

            await _connectedPersonRepository.AddAsync(connectedPersonEntity);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<bool> RemoveConnectionAsync(int personId, int connectedPersonId, ConnectionType connectionType)
        {
            await _connectedPersonRepository.RemoveAsync(personId, connectedPersonId, connectionType);
            await _unitOfWork.CommitAsync(); 
            return true;
        }

        

        
    }
}
