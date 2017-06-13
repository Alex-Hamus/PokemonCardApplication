using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PokemonCards.Startup))]
namespace PokemonCards
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
