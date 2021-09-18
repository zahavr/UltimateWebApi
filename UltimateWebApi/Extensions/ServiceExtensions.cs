using LoggerService;
using Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence;
using Contracts.Repositories;
using Repository;

namespace UltimateWebApi.Extensions
{
	public static class  ServiceExtensions
    {
        public static void ConfigureLoggerService(this IServiceCollection services) =>
            services.AddScoped<ILoggerManager, LoggerManager>();

        public static void ConfigureCors(this IServiceCollection services) =>
			services.AddCors(options =>
			{
				options.AddPolicy("CorsPolicy", builder => {
					builder
					.AllowAnyOrigin()
					.AllowAnyMethod()
					.AllowAnyHeader();
				});
			});

		public static void ConfigureSqlContext(this IServiceCollection services,
			IConfiguration configuration) =>
				services.AddDbContext<AppDbContext>(opts => 
					opts.UseSqlServer(configuration.GetConnectionString("sqlConnection"), b => b.MigrationsAssembly("Persistence")));

		public static void ConfigureRepositoryManager(this IServiceCollection services) =>
			services.AddScoped<IRepositoryManager, RepositoryManager>();
	}
}
