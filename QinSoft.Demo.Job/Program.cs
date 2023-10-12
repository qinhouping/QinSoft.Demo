using QinSoft.Demo.Job;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices((context, services) =>
{
    services.AddHostedService<Worker>();
    services.AddFileConfiger();
    services.AddJobFactory().AddJobs();
});

var app = builder.Build();

await app.RunAsync();
