using GeoLocation.Services.Settings;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace GeoLocation.Services
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            builder.AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ElasticSearchSettings>(Configuration.GetSection("ElasticSearchSettings"));

            // if you really need access to the underlying configuration you can also inject that in ConfigureServices to make it available in your classes.
            //services.AddSingleton(Configuration);
        }
    }
}
