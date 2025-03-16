using static PersonManagementAPI.Enums.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonManagementAPI.Models
{
    public class PhoneNumber
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PersonId { get; set; }
        public Person Person { get; set; } = null!;

        [Required]
        [Column(TypeName = "varchar(10)")]
        public PhoneType PhoneType { get; set; }

        [Required]
        public string Number { get; set; } = string.Empty;
    }
}
