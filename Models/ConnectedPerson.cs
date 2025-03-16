using static PersonManagementAPI.Enums.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonManagementAPI.Models
{
    public class ConnectedPerson
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PersonId { get; set; }
        public Person Person { get; set; } = null!;

        [Required]
        public int ConnectedPersonId { get; set; }
        public Person ConnectedTo { get; set; } = null!;

        [Required]
        [Column(TypeName = "varchar(20)")]
        public ConnectionType ConnectionType { get; set; }
    }
}
