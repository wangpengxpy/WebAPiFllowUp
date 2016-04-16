using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebAPiFllowUp.Startup))]
namespace WebAPiFllowUp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
