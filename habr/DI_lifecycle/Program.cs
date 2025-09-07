// See https://aka.ms/new-console-template for more information
using DI_lifecycle;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

Console.WriteLine("Hello, World!");

PostmanHandler postman;

var services = ConfigureServices();
var serviceProvider = services.BuildServiceProvider();

postman = serviceProvider.GetService<PostmanHandler>();

postman.PickUpLetter();
postman = serviceProvider.GetService<PostmanHandler>();
postman.DeliverLetter();
postman = serviceProvider.GetService<PostmanHandler>();
postman.GetSignature();
postman = serviceProvider.GetService<PostmanHandler>();
postman.HandOverLetter();

Console.WriteLine("-----------------Scope changed!---------------------");

var scopeFactory = serviceProvider.GetService<IServiceScopeFactory>();
using (var scope = scopeFactory.CreateScope())
{
    postman = scope.ServiceProvider.GetService<PostmanHandler>();

    postman.PickUpLetter();
    postman = serviceProvider.GetService<PostmanHandler>();
    postman.DeliverLetter();
    postman = serviceProvider.GetService<PostmanHandler>();
    postman.GetSignature();
    postman = serviceProvider.GetService<PostmanHandler>();
    postman.HandOverLetter();
}

Console.ReadKey();


static IServiceCollection ConfigureServices()
{
    var services = new ServiceCollection();
    services.AddTransient<ITransientPostmanService, PostmanService>();
    services.AddScoped<IScopedPostmanService, PostmanService>();
    services.AddSingleton<ISingletonPostmanService, PostmanService>();

    services.AddTransient<PostmanHandler>();

    services.AddLogging(loggerBuilder =>
    {
        loggerBuilder.ClearProviders();
        loggerBuilder.AddConsole();
    });

    return services;
}
