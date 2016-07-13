using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IBC.Startup))]
namespace IBC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
