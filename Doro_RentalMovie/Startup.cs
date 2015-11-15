using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Doro_RentalMovie.Startup))]
namespace Doro_RentalMovie
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
