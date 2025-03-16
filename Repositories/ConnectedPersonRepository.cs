using Microsoft.EntityFrameworkCore;
using PersonManagementAPI.Data;
using PersonManagementAPI.DTOs;
using PersonManagementAPI.Models;
using static PersonManagementAPI.Enums.Enums;

namespace PersonManagementAPI.Repositories
{
    public class ConnectedPersonRepository : IConnectedPersonRepository
    {
        private readonly ApplicationDbContext _context;

        public ConnectedPersonRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public async Task<List<ConnectedPerson>> GetConnectedPersonsByPersonIdAsync(int personId)
        {
            return await _context.ConnectedPersons
                                 .Where(cp => cp.PersonId == personId)
                                 .ToListAsync(); 
        }

        public async Task AddAsync(ConnectedPerson connectedPerson)
        {
            await _context.ConnectedPersons.AddAsync(connectedPerson);
        }

        public async Task RemoveAsync(int personId, int connectedPersonId, ConnectionType connectionType)
        {
            var connection = await _context.ConnectedPersons
                .FirstOrDefaultAsync(cp => cp.PersonId == personId && cp.ConnectedPersonId == connectedPersonId);

            if (connection == null)
            {
                throw new KeyNotFoundException("Persons are not connected.");
            }

            if (connection.ConnectionType != connectionType)
            {
                throw new KeyNotFoundException($"These persons are not connected with the specified connection type: {connectionType}.");
            }

            _context.ConnectedPersons.Remove(connection);

            await _context.SaveChangesAsync();
        }

        public async Task<List<ConnectionReportDTO>> GetConnectionReportAsync()
        {
            var directConnections = await _context.ConnectedPersons
                .GroupBy(cp => cp.PersonId)
                .Select(g => new ConnectionReportDTO
                {
                    PersonId = g.Key,
                    ConnectionTypeCounts = g
                        .GroupBy(cp => cp.ConnectionType)
                        .Select(cg => new ConnectionTypeCount
                        {
                            ConnectionType = cg.Key,
                            Count = cg.Count()
                        })
                        .ToList()
                })
                .ToListAsync();

            var reverseConnections = await _context.ConnectedPersons
                .GroupBy(cp => cp.ConnectedPersonId)
                .Select(g => new ConnectionReportDTO
                {
                    PersonId = g.Key,
                    ConnectionTypeCounts = g
                        .GroupBy(cp => cp.ConnectionType)
                        .Select(cg => new ConnectionTypeCount
                        {
                            ConnectionType = cg.Key,
                            Count = cg.Count()
                        })
                        .ToList()
                })
                .ToListAsync();

            return directConnections.Concat(reverseConnections)
                .GroupBy(r => r.PersonId) 
                .Select(g => new ConnectionReportDTO
                {
                    PersonId = g.Key,
                    ConnectionTypeCounts = g
                        .SelectMany(r => r.ConnectionTypeCounts)
                        .GroupBy(c => c.ConnectionType)
                        .Select(cg => new ConnectionTypeCount
                        {
                            ConnectionType = cg.Key,
                            Count = cg.Sum(c => c.Count) 
                        })
                        .ToList()
                })
                .ToList();
        }

        public IQueryable<ConnectedPerson> GetAll()
        {
            return _context.ConnectedPersons.AsQueryable();
        }

    }
}
