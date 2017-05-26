using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SistemaDeFacturacion.Startup))]
namespace SistemaDeFacturacion
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
