using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CRUD_MVC.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Category Name")]
        public string Name { get; set; }


        [DisplayName("Display Order")]
        public int DisplayOrder { get; set; }
        

        public DateTime CreatedDateTime { get; set; } = DateTime.Now; 
    }
}
