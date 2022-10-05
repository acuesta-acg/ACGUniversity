// See https://aka.ms/new-console-template for more information
using Acg.University.BL.Servicios;
using Acg.University.DAL.SqlServer;

Console.WriteLine("----   pruebas con nuestra base de datos  ------------");

var sUsr = new ServUsuarios(new UniversityDbContext());
//int idUsr = sUsr.NuevoUsuario("juan", "Hola", "Admin");
//int idRol = sUsr.NuevoRol("Admin");

//var usr = sUsr.ConsultarUsuario(13);
var l = sUsr.ListaUsuario();

var l2 = l.Select(x => new UsuarioMajo()
{
    Id = x.Id,
    Login = x.Login,
    Nombre = "lkdasjfñlasdk",
    Apellidos ="dafasdf asdfasdf"
}).ToList();

foreach (var usr in l2)
    Console.WriteLine(usr.Login);

// string x = (usr?.Roles == null) ? "Sin rol" : (usr.Roles.Count>0) ? usr.Roles[0].Nombre : "Sin rol";


//Console.WriteLine($"Hola usuario {usr?.Login ?? "usuario no encontrado"} rol: {x}");

//Console.WriteLine((idUsr <0) ? "No se ha creado el usuario" : $"El Id {idUsr}  del usuario nuevo");
// Console.WriteLine((idRol < 0) ? "No se ha creado el rol" : $"El Id {idRol}  del rol nuevo");


Console.WriteLine("--------------------------------------------------------");

public class UsuarioMajo
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string Nombre { get; set; }
    public string Apellidos { get; set; }
}