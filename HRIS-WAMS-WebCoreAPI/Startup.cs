using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Microsoft.OpenApi.Models;

using System.Reflection;
using System.IO;
using Microsoft.CodeAnalysis.Options;

namespace HRIS_WAMS_WebCoreAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                              builder =>
                              {
                                  builder.WithOrigins("http://*.cht.com.tw",
                                                      "http://*.miraclemobile.com.tw")
                                    .AllowAnyHeader()
                                    .AllowAnyOrigin()
                                    .AllowAnyMethod();
                                  ;
                              });
            });

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                //c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v0.18 (05/18)",
                    Title = "HRIS-WAMS 工時系統 API",
                    Description = "",
                    //TermsOfService = new Uri("https://www.cht.com.tw/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "總公司",
                        Email = string.Empty
                        //Url = new Uri("https://twitter.com/spboyer"),
                    },
                    //License = new OpenApiLicense
                    //{
                    //    Name = "北區分公司",
                    //    Url = new Uri("https://www.csi.com.tw/license"),
                    //}
                });

               
                // Configure Swagger to use the xml documentation file
                var xmlFile = Path.ChangeExtension(typeof(Startup).Assembly.Location, ".xml");
                c.IncludeXmlComments(xmlFile);

            });

            services.AddMvc();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            // Enable middleware to serve generated Swagger as a JSON endpoint.
            //app.UseSwagger();
            app.UseSwagger( c=>
            {
                c.PreSerializeFilters.Add((swagger, httpReq) =>
                {
                    swagger.Servers = new List<OpenApiServer> { 
                        new OpenApiServer { 
                            Url = @"http://10.172.18.146:8081/api/v1/whs/" } };
                });
            });

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "HRIS-WAMS Swagger 文件說明製作 V1");
            });

            app.UseRouting();

            // Calls the UseCors extension method and specifies the _myAllowSpecificOrigins CORS policy. UseCors adds the CORS middleware. The call to UseCors must be placed after UseRouting, but before UseAuthorization. For more information, see Middleware order
            // With endpoint routing, the CORS middleware must be configured to execute between the calls to UseRouting and UseEndpoints
            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                //endpoints.MapControllerRoute(name: "duty",
                //   pattern: "duty/{*article}",
                //   defaults: new { controller = "Whs", action = "Article" });
            });

            
        }
    }
}
