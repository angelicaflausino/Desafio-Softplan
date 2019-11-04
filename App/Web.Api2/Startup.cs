using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using App.Application.Interfaces;
using App.Application.Services;
using App.Infra.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;

namespace Web.Api2
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddCors();
            services.AddMvc();
            services.AddMvcCore()
                .AddApiExplorer();
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            ConfigureSwaggerGen(services);

            services.AddScoped<ICalculaJurosService, CalculaJurosService>();

            services.AddHttpClient<ICalculaJurosService, CalculaJurosService>(options => {
                options.BaseAddress = new Uri(Configuration["AppSettings:DevelopUrl"]);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // to serve up index.html
            app.UseDefaultFiles();
            app.UseStaticFiles();

            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }

            app.UseMvc();

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web.Api2 API V1");
                c.RoutePrefix = string.Empty;
            });

            
        }

        private static void ConfigureSwaggerGen(IServiceCollection services)
        {
            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "Web.API 2",
                    Version = "v1",
                    Description = "Desafio técnico Softplan",
                    Contact = new Swashbuckle.AspNetCore.Swagger.Contact
                    {
                        Name = "Angélica Flausino",
                        Url = "https://github.com/angelicaflausino"
                    }
                });

                // show token field
                //c.OperationFilter<AuthorizationHeaderParameterOperationFilter>();

                //exibe comentários na documentação swagger
                string caminhoApp = PlatformServices.Default.Application.ApplicationBasePath;
                string nomeApp = PlatformServices.Default.Application.ApplicationName;
                string caminhoXmlDoc = Path.Combine(caminhoApp, $"{nomeApp}.xml");
                c.IncludeXmlComments(caminhoXmlDoc);
            });
        }
    }
}
