using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActivityTracker.Data;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ActivityTracker
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {

      // We have DB Context, we have our database, we have our connection string - REGISTER ALL TOGETHER - all in the starter method, should be good to go. 
      services.AddDbContext<ActivityTrackerContext>(opt => opt.UseSqlServer
        (Configuration.GetConnectionString("ActivityTrackerConnection")));

      services.AddControllers();

      services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

      // If we ever need to change mockActivityTrackerRepo, just swap it out. 
      // services.AddScoped<IActivityTrackerRepo, MockActivityTrackerRepo>();
      services.AddScoped<IActivityTrackerRepo, SqlActivityTrackerRepo>();


    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
