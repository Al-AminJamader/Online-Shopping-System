using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SportStore.Startup))]
namespace SportStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
