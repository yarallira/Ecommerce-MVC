using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace ECommerce.Models
{
    public class Company
    {
        [Key]
        public int CompanyID { get; set; }

        [Required(ErrorMessage = "O campo Nome é requirido!!")]
        [MaxLength(50, ErrorMessage = "Este campo somente aceita até 50 caracteres.")]
        [Display(Name = "Nome")]
        [Index("Company_Name_Index", IsUnique = true)]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo Telefone é requirido!!")]
        [MaxLength(50, ErrorMessage = "Este campo somente aceita até 50 caracteres.")]
        [Display(Name = "Telefone")]
        [Index("Company_Phone_Index", IsUnique = true)]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "O campo Endereço é requirido!!")]
        [MaxLength(100, ErrorMessage = "Este campo somente aceita até 100 caracteres.")]
        [Display(Name = "Endereço")]
        [Index("Company_Address_Index", IsUnique = true)]
        public string Adress { get; set; }

        [Display(Name = "Imagem")]
        [DataType(DataType.ImageUrl)]
        public string Logo { get; set; }

        [NotMapped]
        public HttpPostedFileBase LogoFile { get; set; }

        [Required(ErrorMessage = "O campo Departamento é requirido!!")]
        [Display(Name = "Departamento")]
        [Range(1, double.MaxValue, ErrorMessage = "Selecione um departamento")]
        public int DepartamentsID { get; set; }

        [Required(ErrorMessage = "O campo Cidade é requirido!!")]
        [Display(Name = "Cidade")]
        [Range(1, double.MaxValue, ErrorMessage = "Selecione uma cidade")]
        public int CityID { get; set; }

        public virtual Departaments Departaments { get; set; }
        public virtual City Cities { get; set; }
    }
}