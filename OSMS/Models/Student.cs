using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OSMS.Models
{
    public class Student
    {
        [Key]
        [Column(TypeName = "nvarchar(50)")]
        public string StudentID { get; set; }
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string StudentName {get; set;}
        [ForeignKey("Standard")]
        [Required]
        public int StandardID { get; set; }
        [Column(TypeName = "timestamp")]
        public byte[] RowVersion { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string Password { get; set; }
        [ForeignKey("Role")]
        [Required]
        public int RoleID { get; set; }

        public Standard Standard { get; set; }
        public Role Role { get; set; }
    }
}
