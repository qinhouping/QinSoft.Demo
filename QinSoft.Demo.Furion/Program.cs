// See https://aka.ms/new-console-template for more information

using Furion.Components;

Serve.Run(RunOptions.Default.WithArgs(args).ConfigureServices(services =>
{
    services.AddFileConfiger(options =>
    {
        options.ExpireIn = 600;
    });
    services.AddDatabaseManager(options =>
    {

    });

    services.AddServices();
    services.AddMappers();
    services.AddRepositories();

}).AddComponent<ServeServiceComponent>().UseComponent<ServeApplicationComponent>());