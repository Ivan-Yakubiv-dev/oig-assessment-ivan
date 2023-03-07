using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SurveyApp.API.Services;
using SurveyApp.API.Services.Interfaces;

namespace SurveyApp.API
{
	public class Startup
	{
		// TODO: Use app configurations to set up database connection and typed options injection
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
				webApp.UseSwaggerUI();
			}

			webApp.UseHttpsRedirection();

			webApp.UseAuthorization();

			webApp.MapControllers();
		}
	}
}
