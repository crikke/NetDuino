using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NetDuino.Startup))]
namespace NetDuino
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
