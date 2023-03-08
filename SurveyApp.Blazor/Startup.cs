using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SurveyApp.Blazor.Services;
using System;

namespace SurveyApp.Blazor
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
			services.AddRazorPages();

			services.AddServerSideBlazor();
		}

		public void ConfigureDI(IServiceCollection services)
		{
			var surveyAppApiUrl = _configuration.GetSection("SurveyAppAPI").GetValue<string>("BaseUrl");

			services.AddHttpClient<QuestionnaireApiService>(client =>
			{
				client.BaseAddress = new Uri($"{surveyAppApiUrl}/Questionnaire");
			});
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
