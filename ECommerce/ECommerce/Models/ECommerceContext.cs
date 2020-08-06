using System.Data.Entity;

namespace ECommerce.Models
{
    public class ECommerceContext : DbContext
    {

        public ECommerceContext() : base("DefaultConnection")
        {
                
        }

        public System.Data.Entity.DbSet<ECommerce.Models.City> Cities { get; set; }

        public System.Data.Entity.DbSet<ECommerce.Models.Departaments> Departaments { get; set; }
    }
}