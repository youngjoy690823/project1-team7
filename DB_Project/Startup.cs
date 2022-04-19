using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DB_Project.Startup))]
namespace DB_Project
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
