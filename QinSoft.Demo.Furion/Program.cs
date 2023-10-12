// See https://aka.ms/new-console-template for more information

Serve.Run(RunOptions.Default
    .WithArgs(args)
    .AddComponent<ServeServiceComponent>()
    .AddComponent<ApiComponent>()
    .AddComponent<JobComponent>()
    .UseComponent<ServeApplicationComponent>());