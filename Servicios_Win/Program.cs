using Servicios_Win;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<MantenimientoInventario>();
    })
    .UseWindowsService()
    .Build();

await host.RunAsync();
