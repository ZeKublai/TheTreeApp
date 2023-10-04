using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TreeWebApp.Startup))]
namespace TreeWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
