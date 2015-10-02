using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Week6LabTwitter.Startup))]
namespace Week6LabTwitter
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
