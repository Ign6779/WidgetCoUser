using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WidgetCoUser.Repositories;
using WidgetCoUser.Repositories.Interfaces;
using WidgetCoUser.Services;
using WidgetCoUser.Services.Interfaces;
using WidgetCoUser.Models;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication() // Required for your setup
    .ConfigureServices((context, services) => 
    {
        // Load configuration from local.settings.json and environment variables
        var configuration = new ConfigurationBuilder()
            .SetBasePath(context.HostingEnvironment.ContentRootPath)
            .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        // Register configuration so it’s accessible throughout the app
        services.AddSingleton(new WidgetCoUser.FunctionConfiguration(configuration));

        services.AddDbContext<WidgetCoUser.CosmosContext>();

        // Register services directly here
        services.AddScoped<IRepository<User>, UserRepository>();
        services.AddScoped<IUserService, UserService>();
        
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
    })
    .Build();

host.Run();