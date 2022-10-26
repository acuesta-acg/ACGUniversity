using CorrectorExamenesWS;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Corrector>();
        //services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
