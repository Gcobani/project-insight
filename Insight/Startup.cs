using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Insight.Startup))]
namespace Insight
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
