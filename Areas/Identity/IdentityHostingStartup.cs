using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(liquidador_web.Areas.Identity.IdentityHostingStartup))]
namespace liquidador_web.Areas.Identity
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