using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InsetByCsv.Startup))]
namespace InsetByCsv
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
