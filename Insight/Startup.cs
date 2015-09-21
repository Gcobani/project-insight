using Microsoft.Owin;
using Owin;
using Hangfire;
using System.Configuration;

[assembly: OwinStartupAttribute(typeof(Insight.Startup))]
namespace Insight
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["dataSocket"].ConnectionString;
            ConfigureAuth(app);
            GlobalConfiguration.Configuration.UseSqlServerStorage(ConnectionString);

            app.UseHangfireDashboard();
            app.UseHangfireServer();
        }
    }
}
