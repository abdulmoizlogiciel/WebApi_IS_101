using IdentityServer4.Models;
using IdentityServer4.Test;
using Ids;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WebApi_IS_101.Services;

namespace WebApi_IS_101
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
            services.AddControllers();

            //var defaultConnection = "Data Source=IdentityServer.db";
            //var migrationAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services
                .AddIdentityServer()
                .AddInMemoryClients(Config.Clients)
                .AddInMemoryIdentityResources(Config.IdentityResources)
                .AddInMemoryApiResources(Config.ApiResources)
                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddTestUsers(Config.Users)

                //.AddConfigurationStore(options =>
                //{
                //    options.ConfigureDbContext = builder => builder.UseMySql();
                //options.ConfigureDbContext = builder => builder.UseSqlite(defaultConnection, opt => opt.MigrationsAssembly(migrationAssembly));
                //})
                //.AddOperationalStore(options =>
                //{
                //    options.ConfigureDbContext = builder => builder.UseMySql();
                //options.ConfigureDbContext = builder => builder.UseSqlite(defaultConnection, opt => opt.MigrationsAssembly(migrationAssembly));
                //})

                .AddSigningCredential(GetJwtCertificate())
                .AddProfileService<ProfileService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseIdentityServer();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


        private System.Security.Cryptography.X509Certificates.X509Certificate2 GetJwtCertificate()
        {
            return new(Configuration["JwtCertificate:FileName"], Configuration["JwtCertificate:Password"]);
        }
    }
}
