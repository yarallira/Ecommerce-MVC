using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models
{
    public class Departaments
    {
        [Key]
        public int DepartamentsID { get; set; }

        [Required(ErrorMessage ="O campo Nome é requirido!!")]
        [MaxLength(50, ErrorMessage = "Este campo somente aceita até 50 caracteres.")]
        [Display(Name = "Nome")]
        [Index("Departaments_Name_Index", IsUnique = true)]
        public string Name { get; set; }

        public virtual ICollection<City> Cities { get; set; }
    }
}