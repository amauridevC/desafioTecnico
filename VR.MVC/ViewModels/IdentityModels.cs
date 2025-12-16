using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace VR.MVC.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }

        public string NomeCompleto
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Nome) && string.IsNullOrWhiteSpace(Sobrenome))
                    return UserName; 
                return $"{Nome?.Trim()} {Sobrenome?.Trim()}".Trim();
            }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            userIdentity.AddClaim(new Claim("NomeCompleto", this.NomeCompleto));
            // Add custom user claims here

            var roles = await manager.GetRolesAsync(this.Id);
            if (roles.Any())
            {
                userIdentity.AddClaim(new Claim("RoleDisplay", string.Join(", ", roles)));
            }
            else
            {
                userIdentity.AddClaim(new Claim("RoleDisplay", "Usuário"));
            }

            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}