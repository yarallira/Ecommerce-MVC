using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ECommerce.Models
{
    public class ECommerceContext : DbContext
    {

        public ECommerceContext() : base("DefaultConnection")
        {
                
        }

        public System.Data.Entity.DbSet<ECommerce.Models.City> Cities { get; set; }

        public System.Data.Entity.DbSet<ECommerce.Models.Departaments> Departaments { get; set; }

        public System.Data.Entity.DbSet<ECommerce.Models.Company> Companies { get; set; }


        //DESABILITAR CASCATAS

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public System.Data.Entity.DbSet<ECommerce.Models.User> Users { get; set; }
    }
}