using static PersonManagementAPI.Enums.Enums;

namespace PersonManagementAPI.DTOs
{
    public class PhoneNumberDTO
    {
        public PhoneType PhoneType { get; set; }
        public string Number { get; set; } = string.Empty;
    }
}

