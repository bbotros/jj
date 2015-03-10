using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JJTrailer.Startup))]
namespace JJTrailer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
