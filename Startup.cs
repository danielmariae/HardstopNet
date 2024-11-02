using HardstopNet.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HardstopNet.Startup))]
namespace HardstopNet
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CriarRolesPadrao();
        }

        private void CriarRolesPadrao()
        {
            using (var context = new ApplicationDbContext())
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

                // Verificar e criar Role de Cliente
                if (!roleManager.RoleExists("Cliente"))
                {
                    roleManager.Create(new IdentityRole("Cliente"));
                }

                // Verificar e criar Role de Funcionário
                if (!roleManager.RoleExists("Funcionario"))
                {
                    roleManager.Create(new IdentityRole("Funcionario"));
                }
            }
        }

    }
}
