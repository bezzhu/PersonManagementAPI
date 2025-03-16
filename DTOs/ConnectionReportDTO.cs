using static PersonManagementAPI.Enums.Enums;

namespace PersonManagementAPI.DTOs
{
    public class ConnectionReportDTO
    {
        public int PersonId { get; set; }
        public List<ConnectionTypeCount> ConnectionTypeCounts { get; set; } = new();
    }

    public class ConnectionTypeCount
    {
        public ConnectionType ConnectionType { get; set; }
        public int Count { get; set; }
    }
}
