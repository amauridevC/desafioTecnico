using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VR.MVC.Startup))]
namespace VR.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
