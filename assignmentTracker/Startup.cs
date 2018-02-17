using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(assignmentTracker.Startup))]
namespace assignmentTracker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
