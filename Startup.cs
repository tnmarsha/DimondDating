using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DimondDating.Startup))]
namespace DimondDating
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }


    }

}
