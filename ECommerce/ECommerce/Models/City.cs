using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models
{
    public class City
    {
        [Key]
        [Display(Name = "Cidade Id")]
        public int CityID { get; set; }

        [Required(ErrorMessage = "O campo Nome é requirido!!")]
        [Display(Name = "Cidade")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo Departamento é requirido!!")]
        [Display(Name = "Departamento")]
        public int DepartamentsID { get; set; }

        public virtual Departaments Departament { get; set; }
    }
}