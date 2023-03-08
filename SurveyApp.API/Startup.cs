using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SurveyApp.API.DAL;
using SurveyApp.API.Services;
using SurveyApp.API.Services.Interfaces;

namespace SurveyApp.API
{
	public class Startup
	{
		private readonly IConfiguration _configuration;
		private readonly IWebHostEnvironment _environment;
		private bool _isDevelopmentEnv => _environment.EnvironmentName == "Development";

		public Startup(IConfiguration configuration, IWebHostEnvironment environment)
		{
			_configuration = configuration;
			_environment = environment;
		}

		public void ConfigureApplication(IServiceCollection services)
		{
			string dbConnectionString = _configuration.GetConnectionString("dbConnString");
			services.AddDbContext<SurveyDbContext>(options =>
				options.UseNpgsql(dbConnectionString, opts =>
				{
					opts.CommandTimeout(600);
					opts.MigrationsAssembly("SurveyApp.API");
					opts.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
				}));

			services.AddControllers();

			services.AddEndpointsApiExplorer();

			services.AddSwaggerGen();
		}

		public void ConfigureDI(IServiceCollection services)
		{
			services.AddScoped<IQuestionnaireService, QuestionnaireService>();
		}

		public void ConfigureMiddlewares(WebApplication webApp)
		{
			// NOTE: Ideally, custom middleware with global exception handler should appear here.
			//		 Exceptions handling is handy in combination with custom exceptions classes.
			//		 Also, custom logger implemented within ExceptionHandler would be a good centralized solution to log all errors in needed format.

			if (_isDevelopmentEnv)
			{
				webApp.UseSwagger();
				webApp.UseSwaggerUI(c =>
				{
					c.SwaggerEndpoint("/swagger/v1/swagger.json", "SurveyApp API V1");
					// NOTE: Set Swagger UI at apps root
					c.RoutePrefix = string.Empty;
				});
			}

			webApp.UseHttpsRedirection();

			webApp.UseAuthorization();

			webApp.MapControllers();
		}
	}
}
