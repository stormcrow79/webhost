using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace webhost
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var info = new ServerInfo
      {
        Hostname = "localhost"
      };


      CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
      Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
          webBuilder.UseStartup<Startup>();
        });
  }

  public class Startup
  {
    public void ConfigureServices(IServiceCollection services) { }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
      IConfiguration config)
    {
      if (env.IsDevelopment())
        app.UseDeveloperExceptionPage();

      app.UseStaticFiles(new StaticFileOptions()
      {
        FileProvider = new PhysicalFileProvider(config["HostOptions:PhysicalPath"]),
        RequestPath = config["HostOptions:RequestPath"]
      });
    }
  }

  class ServerInfo
  {
    public string Hostname {get;set;}
    public int Port {get;set;}
    public int Username {get;set;}
  }
}
