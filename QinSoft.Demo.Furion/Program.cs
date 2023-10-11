// See https://aka.ms/new-console-template for more information

using Furion.Components;
using QinSoft.Demo.Furion;

Serve.Run(RunOptions.Default
    .WithArgs(args)
    .AddComponent<ServeServiceComponent>()
    .AddWebComponent<ApiServiceComponent>()
    .AddComponent<BLLServiceComponent>()
    .AddComponent<DALServiceComponent>()
    .UseComponent<ServeApplicationComponent>());