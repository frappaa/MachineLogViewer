using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MachineLogViewer.Startup))]
namespace MachineLogViewer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
