using QinSoft.Demo.Job;
using QinSoft.Demo.Job.Jobs;
using System.Net.Mime;
using static QinSoft.Demo.Job.Worker;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices((context, services) =>
{
    services.AddHostedService<Worker>();
    services.AddFileConfiger();
    services.AddJobs();
});

var app = builder.Build();

await app.RunAsync();
