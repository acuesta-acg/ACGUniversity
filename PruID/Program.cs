// See https://aka.ms/new-console-template for more information
using PruID;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

Console.WriteLine("Hello, World!");
/*
var t = new TratamientoExamenes(new ClienteGMAil(), new ServAlumnos());
t.MandarNotas();

var t2 = new TratamientoExamenes(new ClienteHotMail(), new ServAlumnos());
t2.MandarNotas();
 */
// using IHost h = Host.CreateDefaultBuilder(args)

using var h = Host.CreateDefaultBuilder(args)
               .ConfigureServices((_, services) =>
                    services.AddTransient<IServTransient, TratamientoExamenes>()
                    .AddScoped<IServScope, TratamientoExamenes>()
                    .AddSingleton<IServSingleton, TratamientoExamenes>()
                    .AddTransient<LogTratamientoExamenes>()         // Transient para que se cree siempre una nueva instancia.
               ).Build();

Console.WriteLine("---------   Ejecutar(h.Services, \"scope 1\")   -----------------------");
Ejecutar(h.Services, "scope 1");
Console.WriteLine("---------   Ejecutar(h.Services, \"scope 2\")  -----------------");
Ejecutar(h.Services, "scope 2");

static void Ejecutar(IServiceProvider provider, string scope)
{
    using IServiceScope sc = provider.CreateScope();
    IServiceProvider p = sc.ServiceProvider;

    var log = p.GetRequiredService<LogTratamientoExamenes>();
    log.Informac($"Ejecución 1 {scope}");

    Console.WriteLine("..........");
    
    log = p.GetRequiredService<LogTratamientoExamenes>();
    log.Informac($"Ejecución 2 {scope}");

    Console.WriteLine();
}