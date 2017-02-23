using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Project300_BioStudent.Startup))]
namespace Project300_BioStudent
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
