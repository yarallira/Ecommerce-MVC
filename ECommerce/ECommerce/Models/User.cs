using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace ECommerce.Models
{
    public class User
    {
        [Key]
        [Display(Name = "Usuário Id")]
        public int UserID { get; set; }

        [Required(ErrorMessage = "O campo E-mail é requirido!!")]
        [MaxLength(250, ErrorMessage = "Este campo somente aceita até 50 caracteres.")]
        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        [Index("User_UserName_Index", IsUnique = true)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "O campo Nome é requirido!!")]
        [MaxLength(50, ErrorMessage = "Este campo somente aceita até 50 caracteres.")]
        [Display(Name = "Nome")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "O campo Sobrenome é requirido!!")]
        [MaxLength(50, ErrorMessage = "Este campo somente aceita até 50 caracteres.")]
        [Display(Name = "Sobrenome")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "O campo Telefone é requirido!!")]
        [MaxLength(50, ErrorMessage = "Este campo somente aceita até 50 caracteres.")]
        [Display(Name = "Telefone")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "O campo Endereço é requirido!!")]
        [MaxLength(100, ErrorMessage = "Este campo somente aceita até 100 caracteres.")]
        [Display(Name = "Endereço")]
        public string Adress { get; set; }

        [Display(Name = "Imagem")]
        [DataType(DataType.ImageUrl)]
        public string Photo { get; set; }

        [NotMapped]
        public HttpPostedFileBase PhotoFile { get; set; }

        [Required(ErrorMessage = "O campo Departamento é requirido!!")]
        [Display(Name = "Departamento")]
        [Range(1, double.MaxValue, ErrorMessage = "Selecione um departamento")]
        public int DepartamentsID { get; set; }

        [Required(ErrorMessage = "O campo Cidade é requirido!!")]
        [Display(Name = "Cidade")]
        [Range(1, double.MaxValue, ErrorMessage = "Selecione uma cidade")]
        public int CityID { get; set; }

        [Required(ErrorMessage = "O campo companhia é requirido!!")]
        [Display(Name = "Companhia")]
        [Range(1, double.MaxValue, ErrorMessage = "Selecione uma companhia")]
        public int CompanyID { get; set; }

        public string FullName { get { return string.Format("{0} {1}"); } }

        public virtual Departaments Departaments { get; set; }
        public virtual City Cities { get; set; }
        public virtual Company Company { get; set; }
    }
}