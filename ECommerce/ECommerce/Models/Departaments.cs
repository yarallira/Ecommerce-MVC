using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models
{
    public class Departaments
    {
        [Key]
        public int DepartamentsID { get; set; }

        [Required(ErrorMessage ="O campo Nome é requirido!!")]
        public string Name { get; set; }

        public virtual ICollection<City> Cities { get; set; }
    }
}