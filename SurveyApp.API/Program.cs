using Microsoft.AspNetCore.Builder;
using SurveyApp.API;

var webAppBuilder = WebApplication.CreateBuilder(args);
var startup = new Startup(webAppBuilder.Configuration, webAppBuilder.Environment);
startup.ConfigureDatabase(webAppBuilder.Services);
startup.ConfigureApplication(webAppBuilder.Services);
startup.ConfigureDI(webAppBuilder.Services);

var webApp = webAppBuilder.Build();
startup.ConfigureMiddlewares(webApp);
startup.SeedDefaultData(webApp);

webApp.Run();
