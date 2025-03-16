using static PersonManagementAPI.Enums.Enums;

namespace PersonManagementAPI.DTOs
{
    public class PersonDTO
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public GenderType Gender { get; set; }
        public string PersonalNumber { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public int CityId { get; set; }
        public string? ProfilePicture { get; set; }
        public List<PhoneNumberDTO> PhoneNumbers { get; set; } = new();
        public List<ConnectedPersonDTO> ConnectedPersons { get; set; } = new();
    }
}