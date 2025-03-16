using static PersonManagementAPI.Enums.Enums;

namespace PersonManagementAPI.DTOs
{
    public class ConnectedPersonDTO
    {
        public int ConnectedPersonId { get; set; }
        public ConnectionType ConnectionType { get; set; }
    }
}
