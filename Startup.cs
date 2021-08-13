using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RentMgt.Startup))]
namespace RentMgt
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
