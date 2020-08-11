using ECommerce.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace ECommerce.Classes
{
    public class UserHelper
    {
        public class UsersHelper : IDisposable
        {
            private static ApplicationDbContext userContext = new ApplicationDbContext();
            private static ECommerceContext db = new ECommerceContext();

            public static void CheckRole(string roleName)
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(userContext));

                // Check to see if Role Exists, if not create it
                if (!roleManager.RoleExists(roleName))
                {
                    roleManager.Create(new IdentityRole(roleName));
                }
            }

            public static void CheckSuperUser()
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));
                var email = WebConfigurationManager.AppSettings["AdminUser"];
                var password = WebConfigurationManager.AppSettings["AdminPassWord"];
                var userASP = userManager.FindByName(email);
                if (userASP == null)
                {
                    CreateUserASP(email, "Admin", password);
                    return;
                }

                userManager.AddToRole(userASP.Id, "Admin");
            }
            public static void CreateUserASP(string email, string roleName)
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));

                var userASP = new ApplicationUser
                {
                    Email = email,
                    UserName = email,
                };

                userManager.Create(userASP, email);
                userManager.AddToRole(userASP.Id, roleName);
            }

            public static void CreateUserASP(string email, string roleName, string password)
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));

                var userASP = new ApplicationUser
                {
                    Email = email,
                    UserName = email,
                };

                userManager.Create(userASP, password);
                userManager.AddToRole(userASP.Id, roleName);
            }

            public static async Task PasswordRecovery(string email)
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));
                var userASP = userManager.FindByEmail(email);
                if (userASP == null)
                {
                    return;
                }

                var user = db.Users.Where(tp => tp.UserName == email).FirstOrDefault();
                if (user == null)
                {
                    return;
                }

                var random = new Random();
                var newPassword = string.Format("{0}{1}{2:04}*",
                    user.FirstName.Trim().ToUpper().Substring(0, 1),
                    user.LastName.Trim().ToLower(),
                    random.Next(10000));

                userManager.RemovePassword(userASP.Id);
                userManager.AddPassword(userASP.Id, newPassword);

                var subject = "A senha foi modificada";
                var body = string.Format(@"
                <h1>A senha foi modificada</h1>
                <p>Sua nova senha é: <strong>{0}</strong></p>
                <p>Senha alterada com sucesso.",
                    newPassword);

                await MailHelper.SendMail(email, subject, body);
            }

            public void Dispose()
            {
                userContext.Dispose();
                db.Dispose();
            }
        }

    }
}