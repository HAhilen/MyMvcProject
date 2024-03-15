using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace firstproj.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [StringLength(100, ErrorMessage = "Email length cannot exceed 100 characters")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email format")]
        [Index(IsUnique = true)] // Ensure uniqueness
        public string Email { get; set; }

        public string Faculty { get; set; }

        [Required(ErrorMessage = "Roll number is required")]
        [StringLength(20, ErrorMessage = "Roll number length cannot exceed 20 characters")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Roll number can only contain letters and digits")]
        [Index(IsUnique = true)] // Ensure uniqueness
        public string RollNO { get; set; }
        
    }
}
