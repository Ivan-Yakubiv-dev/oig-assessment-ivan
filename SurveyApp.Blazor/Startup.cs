using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SurveyApp.Blazor.Data;

namespace SurveyApp.Blazor
{
	public class Startup
	{
		// TODO: use app configurations to set up database connection and typed options injection
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
			services.AddRazorPages();

			services.AddServerSideBlazor();
		}

		public void ConfigureDI(IServiceCollection services)
		{
			services.AddSingleton<WeatherForecastService>();
		}

		public void ConfigureMiddlewares(WebApplication webApp)
		{
			if (!_isDevelopmentEnv)
			{
				webApp.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days
				webApp.UseHsts();
			}

			webApp.UseHttpsRedirection();

			webApp.UseStaticFiles();

			webApp.UseRouting();

			webApp.MapBlazorHub();

			webApp.MapFallbackToPage("/_Host");
		}
	}
}
