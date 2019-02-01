using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Fingerprint_Voting.Startup))]
namespace Fingerprint_Voting
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
