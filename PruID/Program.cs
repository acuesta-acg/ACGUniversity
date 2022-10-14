// See https://aka.ms/new-console-template for more information
using PruID;

Console.WriteLine("Hello, World!");

var t = new TratamientoExamenes(new ClienteGMAil(), new ServAlumnos());
t.MandarNotas();

var t2 = new TratamientoExamenes(new ClienteHotMail(), new ServAlumnos());
t2.MandarNotas();
