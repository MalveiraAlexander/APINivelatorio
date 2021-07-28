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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestNivelatorio.Data;
using TestNivelatorio.Helper;
using TestNivelatorio.Interfaces;
using TestNivelatorio.Repositories;
using TestNivelatorio.Services;
using Microsoft.OpenApi.Models;

namespace TestNivelatorio
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
            services.AddDbContext<DBNivelatorioContext>(options => options.UseNpgsql(Configuration.GetConnectionString("DBNivelatorioContext")));
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient(typeof(IGenericService<>), typeof(GenericService<>));
            services.AddControllers();
            services.AddCors();
            var mapperConfig = new MapperConfiguration(m => {
                m.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            AddSwagger(services);
        }

        private void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var groupName = "v1";

                options.SwaggerDoc(groupName, new OpenApiInfo
                {
                    Title = $"Test Nivelatorio {groupName}",
                    Version = groupName,
                    Description = "Test Nivelatorio API",
                    Contact = new OpenApiContact
                    {
                        Name = "BGlobal",
                        Email = string.Empty,
                        Url = new Uri("https://www.bglobalsolutions.com/"),
                    }
                });
            });
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

            app.UseCors(builder => builder.AllowAnyOrigin()
                                            .AllowAnyMethod()
                                            .AllowAnyHeader());

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Test Nivelatorio API V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
