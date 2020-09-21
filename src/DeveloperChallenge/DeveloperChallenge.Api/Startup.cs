using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DeveloperChallenge.Api.DTO;
using DeveloperChallenge.Application.Parser;
using DeveloperChallenge.Application.Parser.Interfaces;
using DeveloperChallenge.Application.Services;
using DeveloperChallenge.Application.Services.Interfaces;
using DeveloperChallenge.Domain.Entities;
using Infra.Repositories.DbConfiguration.EFCore;
using Infra.Repositories.Interfaces;
using Infra.Repositories.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DeveloperChallenge.Api
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
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IBankTransactionService, BankTransactionsService>();
            services.AddScoped<IBankTransactionRepository, BankTransactionRepository>();
            services.AddScoped<IOFXParser, OFXParser>();
            services.AddScoped<DbContext, NiboContext>();

            services.AddCors(options => options.AddPolicy("NiboPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));


            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BankTransaction, BankTransactionDTO>();
            });

            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
                Path.Combine(env.ContentRootPath, @"NiboApp/dist")),
                RequestPath = new PathString("/NiboApp/dist")
            });

            app.UseRouting();
            app.UseCors("NiboPolicy");

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers().RequireCors("NiboPolicy");
            });
        }
    }
}
