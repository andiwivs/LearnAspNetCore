﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Routing;
using OdeToFood.Services;
using OdeToFood.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace OdeToFood
{
    public class Startup
    {

        #region properties

        public IConfiguration Configuration { get; }

        #endregion

        #region constructors

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                                .SetBasePath(env.ContentRootPath)
                                .AddJsonFile("appsettings.json")
                                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        #endregion

        #region methods

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            // per-app services
            services.AddSingleton(Configuration);
            services.AddSingleton<IGreeter, Greeter>();

            services.AddDbContext<OdeToFoodDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Core")));
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<OdeToFoodDbContext>();

            // per-request services
            services.AddScoped<IRestaurantData, SqlRestaurantData>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app, 
            IHostingEnvironment env, 
            ILoggerFactory loggerFactory,
            IGreeter greeter)
        {

            // logging middleware
            loggerFactory.AddConsole();

            // exception handler middleware
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(new ExceptionHandlerOptions
                {
                    ExceptionHandler = context => context.Response.WriteAsync("Oops")
                });
            }

            // static content delivery middleware
            app.UseFileServer(); // combines default with static file handler

            // custom middleware for node module content
            app.UseNodeModules(env.ContentRootPath);

            // identity framework should be set ahead of MVC
            app.UseIdentity();

            // MVC middleware
            app.UseMvc(ConfigureRoutes);

            // DEBUG: catch-all handler for unsupported routes
            //app.Run(ctx => ctx.Response.WriteAsync("Not found"));
        }

        #endregion

        #region private helper methods

        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            routeBuilder.MapRoute("Default",
                "{controller=Home}/{action=Index}/{id?}");
        }

        #endregion
    }
}
