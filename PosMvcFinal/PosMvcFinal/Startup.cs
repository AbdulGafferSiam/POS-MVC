using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PosMvcFinal.Startup))]
namespace PosMvcFinal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
