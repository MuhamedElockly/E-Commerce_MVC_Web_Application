using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyBook.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName(" Name")]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [DisplayName("Display Order")]
        [Range(0, 100, ErrorMessage = "The range is not valid")]
        public int DisplayOrder { get; set; }
    }
}
