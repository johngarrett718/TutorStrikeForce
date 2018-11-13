using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using TutorStrikeForce.Extensions;

namespace TutorStrikeForce
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).SeedRoles().SeedUsers().SeedApplicationData().Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
