using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ReviewMe.Web.Startup))]
namespace ReviewMe.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
