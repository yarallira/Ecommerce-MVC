using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models
{
    public class City
    {
        [Key]
        [Display(Name = "Cidade Id")]
        public int CityID { get; set; }

        [Required(ErrorMessage = "O campo Nome é requirido!!")]
        [MaxLength(50,ErrorMessage ="Este campo somente aceita até 50 caracteres.")]
        [Display(Name = "Cidade")]
        [Index("City_Name_Index", IsUnique = true)]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo Departamento é requirido!!")]
        [Display(Name = "Departamento")]
        [Range( 1, double.MaxValue , ErrorMessage="Selecione um departamento")]
        public int DepartamentsID { get; set; }

        public virtual Departaments Departament { get; set; }

        public virtual ICollection<Company> Company { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
}