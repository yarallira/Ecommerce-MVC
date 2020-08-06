using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models
{
    public class City
    {
        [Key]
        public int CityID { get; set; }

        [Required(ErrorMessage = "O campo Nome é requirido!!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo Nome é requirido!!")]

        public int DepartamentsID { get; set; }

        public virtual Departaments Departament { get; set; }
    }
}