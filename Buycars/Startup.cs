using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Buycars.Startup))]
namespace Buycars
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
