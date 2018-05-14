﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using KolaNaukowe.web.Data;
using KolaNaukowe.web.Models;
using KolaNaukowe.web.Services;
using KolaNaukowe.web.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.IdentityModel.Tokens.Jwt;
using KolaNaukowe.web.Repositories;
using KolaNaukowe.web.Mappers;

namespace KolaNaukowe.web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IHostingEnvironment Enviroment { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            Enviroment = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<KolaNaukoweDbContext>()
                .AddDefaultTokenProviders();

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryPersistedGrants()
                .AddInMemoryIdentityResources(Config.identityResources)
                .AddInMemoryClients(Config.Clients)
                .AddInMemoryApiResources(Config.Apis)
                .AddAspNetIdentity<ApplicationUser>();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication()
                  .AddJwtBearer(jwt =>
                  {
                      jwt.Authority = "http://localhost:5000";
                      jwt.RequireHttpsMetadata = false;
                      jwt.Audience = "StudentResearchGroupAPI";
                  });


            services.AddAuthorization(options =>
                {
                    options.AddPolicy("AdminOnly", policy =>
                    policy.Requirements.Add(new RoleRequirement(new List<string> { Constants.AdministratorRole })));

                    options.AddPolicy("LeaderAndAdmin", policy =>
                    policy.Requirements.Add(new RoleRequirement(new List<string> { Constants.AdministratorRole, Constants.LeaderRole })));
                });

            services.AddSingleton<IAuthorizationHandler, RoleAuthorizationHandler>();
            services.AddScoped<IStudentResearchGroupService, StudentResearchGroupService>();
            services.AddScoped<IGenericRepository<StudentResearchGroup>, GenericRepository<StudentResearchGroup>>();
            services.AddScoped<IGenericRepository<Student>, GenericRepository<Student>>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddSingleton(AutoMapperConfig.Initialize());
            services.AddMvc(options =>
            {
                options.Filters.Add(new ProducesAttribute("application/json"));
            });
             //Add WebApi

            services.AddDbContext<KolaNaukoweDbContext>(o => o.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=KolaNaukowe;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"));

            services.AddTransient<IEmailSender, EmailSender>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseIdentityServer();

            app.UseMvc(routes =>
            {
            routes.MapRoute(
             name: "StudentResearchGroupController",
             template: "api/{controller}/{action}/{id?}",
            defaults: new { controller = "StudentResearchGroup", action = "GetAll" });
            // defaults: new { controller = "StudentResearchGroupController", action = "GetAll" });

            routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                  // defaults: new { controller = "Home", action = "PageOne" });
            });

            
        }
    }
}
