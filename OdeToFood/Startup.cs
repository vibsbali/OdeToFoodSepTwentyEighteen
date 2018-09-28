using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OdeToFood.Data;
using OdeToFood.Services;

namespace OdeToFood
{
   public class Startup
   {
      private readonly IConfiguration _config;

      public Startup(IConfiguration config)
      {
         _config = config;
      }
      // This method gets called by the runtime. Use this method to add services to the container.
      // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
      public void ConfigureServices(IServiceCollection services)
      {
         services.AddSingleton<IGreeter, Greeter>();
         services.AddEntityFrameworkSqlServer();
         services.AddDbContext<OdeToFoodDbContext>(options =>
         {
            options.UseSqlServer(_config.GetConnectionString("OdeToFood"));
         });
         services.AddScoped<IRestaurantData, SqlRestaurantData>();

         services.AddMvc();
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app,
         IHostingEnvironment env,
         IGreeter greeter,
         ILogger<Startup> logger)
      {
         if (env.IsDevelopment())
         {
            app.UseDeveloperExceptionPage();
         }

         app.UseStaticFiles();

         app.UseMvc(ConfigureRoutes);

         //app.Use(next =>
         //{
         //   return async context =>
         //   {
         //      logger.LogInformation("Request Incoming");
         //      if (context.Request.Path.StartsWithSegments("/mym"))
         //      {
         //         await context.Response.WriteAsync("Hit!!");
         //         logger.LogInformation("Request Handled");
         //      }
         //      else
         //      {
         //         await next(context);
         //         logger.LogInformation("Response outgoing");
         //      }
         //   };
         //});

         app.Run(async (context) =>
            {
               var greeting = greeter.GetMessageOfTheDay();
               await context.Response.WriteAsync(greeting);
            });
      }

      private void ConfigureRoutes(IRouteBuilder routeBuilder)
      {
         // /Home/Index

         routeBuilder.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
      }
   }
}
