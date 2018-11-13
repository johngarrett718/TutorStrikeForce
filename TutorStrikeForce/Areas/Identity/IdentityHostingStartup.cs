using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(TutorStrikeForce.Areas.Identity.IdentityHostingStartup))]
namespace TutorStrikeForce.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}