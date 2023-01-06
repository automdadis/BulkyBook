using System.ComponentModel.DataAnnotations;

namespace BulkyBookWeb.Models
{
    //Είναι ο πίνακας Category μέσα στην db
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public int DisplayOrder { get; set; }

        public DateTime CreatedDateTime { get; set; }  = DateTime.Now; 

    }
}
