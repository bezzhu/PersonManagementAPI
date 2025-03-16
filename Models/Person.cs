using static PersonManagementAPI.Enums.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PersonManagementAPI.Models
{
    public class Person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "varchar(20)")]
        public GenderType Gender { get; set; }

        [Required]
        public string PersonalNumber { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "date")]
        public DateTime BirthDate { get; set; }

        [Required]
        public int CityId { get; set; }

        public List<PhoneNumber> PhoneNumbers { get; set; } = new();

        public string? ProfilePicture { get; set; }

        public List<ConnectedPerson> ConnectedPersons { get; set; } = new();
    }
}
