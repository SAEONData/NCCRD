using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NCCRD.Services.DataV2.Database.Contexts;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;

namespace NCCRD.Services.DataV2
{
    public class Startup
    {
        public static IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddCors();
            services.AddCors(options =>
            {
                options.AddPolicy("CORSPolicy",
                      builder =>
                      {
                          builder
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                      });
            });

            var connectionString = Configuration.GetConnectionString("NCCRD");
            services.AddDbContext<SQLDBContext>(options =>
            {
                options.UseSqlServer(connectionString, o =>
                {
                    o.UseRowNumberForPaging(); //Backwards compatibility for for SQL 2008 R2
                });
            });

            services.AddTransient<ODataModelBuilder>();

            services.AddMvc();
            services.AddOData();

            // Add OpenAPI/Swagger document
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Title = "NCCRD API",
                    Version = "v1.0-beta",
                    Description = "This API gives you access to all <b>NCCRD <i>(National Climate Change Response Database)</i></b> data. <br/>" +
                    "Read access is unauthenticated, but some write actions require authentication <i>(look out for the lock icon)</i>",
                    Contact = new Contact() { Name = "Contact Us", Url = "http://app01.saeon.ac.za/dev/UI_footside/page_contact.html" },
                    License = new License() { Name = "Data Licences", Url = "https://docs.google.com/document/d/e/2PACX-1vT8ajcogJEEo0ZC9BGIej_jOH2EV8lMFrwOu8LB4K9pDq7Tki94mUoVxU8hGM-J5EL8V3w5o83_TuEl/pub" },
                    TermsOfService = "http://noframe.media.dirisa.org/wiki-1/conditions-of-use?searchterm=conditions"
                });
                options.DocumentFilter<CustomDocumentFilter>();
            });

            services
                .AddAuthentication(GetAuthenticationOptions)
                .AddJwtBearer(GetJwtBearerOptions);
        }

        private static void GetAuthenticationOptions(AuthenticationOptions options)
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }

        private static void GetJwtBearerOptions(JwtBearerOptions options)
        {
            var authBaseAddress = Configuration.GetSection("IdentityServiceURL").Value;
            options.Authority = authBaseAddress; //"http://identity.saeon.ac.za";
            options.Audience = authBaseAddress + "/resources";
            options.RequireHttpsMetadata = false;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ODataModelBuilder modelBuilder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CORSPolicy");

            // Add OpenAPI/Swagger middlewares
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "v1.0-beta");
            });

            app
                .UseAuthentication()
                .UseMvc(routeBuilder =>
                {
                    routeBuilder.MapODataServiceRoute(
                        "ODataRoutes",
                        "odata",
                        modelBuilder.GetEdmModel(app.ApplicationServices));
                });
        }
    }
}
