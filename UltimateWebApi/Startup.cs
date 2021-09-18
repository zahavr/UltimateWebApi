using Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NLog;
using System.IO;
using UltimateWebApi.Extensions;
using UltimateWebApi.Middleware;

namespace UltimateWebApi
{
	public class Startup
	{
		private readonly IConfiguration _configuration;

		public Startup(IConfiguration configuration)
		{
			LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
			_configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			
			services.ConfigureCors();
			services.ConfigureLoggerService();
			services.ConfigureSqlContext(_configuration);
			services.ConfigureRepositoryManager();
			services.AddControllers(config =>
			{
				config.RespectBrowserAcceptHeader = true;
				config.ReturnHttpNotAcceptable = true;
			}).AddXmlSerializerFormatters();

			services.AddAutoMapper(typeof(Startup));
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "UltimateWebApi", Version = "v1" });
			});
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerManager logger)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UltimateWebApi v1"));
			} else
			{
				app.UseHsts();
			}
			app.ConfigureExceptionHandler(logger);
			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseCors("CorsPolicy");
			app.UseForwardedHeaders(new ForwardedHeadersOptions()
			{
				ForwardedHeaders = ForwardedHeaders.All
			});
			app.UseRouting();
			app.UseAuthorization();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
