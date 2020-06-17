using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OSMS.Models
{
    public class Standard
    {
        [Key]
        public int StandardID { get; set; }
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string StandardName { get; set; }
        [Column(TypeName = "varchar(max)")]
        public string Description { get; set; }

        public List<Student> Students { get; set; }
        public List<Instructor> Instructors { get; set; }
    }
}
