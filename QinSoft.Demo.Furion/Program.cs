// See https://aka.ms/new-console-template for more information

Serve.Run(RunOptions.Default
    .WithArgs(args)
    .AddComponent<ServeServiceComponent>()
    .AddWebComponent<ApiServiceComponent>()
    .AddComponent<BLLServiceComponent>()
    .AddComponent<DALServiceComponent>()
    .UseComponent<ServeApplicationComponent>());