using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Bolao.Startup))]
namespace Bolao
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
