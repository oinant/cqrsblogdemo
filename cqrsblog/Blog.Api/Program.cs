using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Blog.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var program = new Program();
            program.Run();
        }

        public void Run()
        {
            using (var host = new WebHostBuilder()
                .UseKestrel()
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build())
            {
                host.Run();
            }
        }
    }
}
