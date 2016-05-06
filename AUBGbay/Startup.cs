using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AUBGbay.Startup))]
namespace AUBGbay
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
