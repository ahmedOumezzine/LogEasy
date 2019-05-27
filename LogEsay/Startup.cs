using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LogEsay.Startup))]
namespace LogEsay
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
