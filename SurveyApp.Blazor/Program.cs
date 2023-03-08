using Microsoft.AspNetCore.Builder;
using SurveyApp.Blazor;

var webAppBuilder = WebApplication.CreateBuilder(args);
var startup = new Startup(webAppBuilder.Configuration, webAppBuilder.Environment);
startup.ConfigureApplication(webAppBuilder.Services);
startup.ConfigureDI(webAppBuilder.Services);

var webApp = webAppBuilder.Build();
startup.ConfigureMiddlewares(webApp);

webApp.Run();
