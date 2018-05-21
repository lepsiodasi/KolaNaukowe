using System;
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
using IdentityServer4.AccessTokenValidation;

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
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryClients(Config.GetClients())
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddAspNetIdentity<ApplicationUser>();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;
                // Lockout settings.
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(1);
            });

            if (Enviroment.IsProduction())
            {
                services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                    .AddIdentityServerAuthentication(options =>
                    {
                        //Production website link
                        options.Authority = "";
                        options.RequireHttpsMetadata = false;

                        options.ApiName = "StudentResearchGroupAPI";
                    });
            }
            else
            {
                services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                    .AddIdentityServerAuthentication(options =>
                    {
                        options.Authority = "http://localhost:5000/";
                        options.RequireHttpsMetadata = false;

                        options.ApiName = "StudentResearchGroupAPI";
                    });
            }


            services.AddAuthorization(options =>
                {
                    options.AddPolicy("AdminOnly", policy =>
                    policy.Requirements.Add(new RoleRequirement(new List<string> { Constants.AdministratorRole })));

                    options.AddPolicy("LeaderAndAdmin", policy =>
                    policy.Requirements.Add(new RoleRequirement(new List<string> { Constants.AdministratorRole, Constants.LeaderRole })));
                });

            services.AddSingleton<IAuthorizationHandler, RoleAuthorizationHandler>();
            services.AddScoped<IStudentResearchGroupService, StudentResearchGroupService>();
            services.AddScoped<ISubjectService, SubjectService>();
            services.AddScoped<IGenericRepository<StudentResearchGroup>, GenericRepository<StudentResearchGroup>>();
            services.AddScoped<IGenericRepository<Student>, GenericRepository<Student>>();
            services.AddScoped<IGenericRepository<Subject>, GenericRepository<Subject>>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddSingleton(AutoMapperConfig.Initialize());
            services.AddMvc(options =>
            {
                options.Filters.Add(new ProducesAttribute("application/json"));
            });
             //Add WebApi

            services.AddDbContext<KolaNaukoweDbContext>(o => o.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=KolaNaukowe;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"));

            services.AddTransient<IEmailSender, EmailSender>();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

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

            app.UseCors("CorsPolicy");

            app.UseMvc(routes =>
            {
            routes.MapRoute(
             name: "StudentResearchGroupController",
             template: "api/{controller}/{action}/{id?}",
            defaults: new { controller = "StudentResearchGroup", action = "GetAll" });

            routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                  // defaults: new { controller = "Home", action = "PageOne" });
            });

            
        }
    }
}
