using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WeatherSecureApi
{
    public class Program
    {
        /*
          https://www.youtube.com/watch?v=3PyUjOmuFic
            http://dotnetplaybook.com/secure-a-net-core-api-using-bearer-authentication/
            https://github.com/binarythistle/S03E01-Secure-.NET-Core-API
     */
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
