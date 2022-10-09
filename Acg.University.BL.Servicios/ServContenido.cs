using MongoDB.Driver;
using Acg.University.DAL.MongoDB;
using Acg.University.DAL.MongoDB.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acg.University.BL.Servicios
{
    public class ServContenido
    {
        private readonly ServMongoDb _servMongo;

        public ServContenido()
        {
            _servMongo = new ServMongoDb("mongodb://localhost:27017", "ACGUniversity");
        }

        public List<Contenido> Lista() => ListaAsync().GetAwaiter().GetResult();
        public Contenido? Consultar(string id) => ConsultarAsync(id).GetAwaiter().GetResult();
        public string Nuevo(Contenido contenido) => NuevoAsync(contenido).GetAwaiter().GetResult();
        public void Modificar(Contenido contenido) => ModificarAsync(contenido).GetAwaiter().GetResult();
        public void Borrar(string id) => BorrarAsync(id).GetAwaiter().GetResult();

        public async Task<List<Contenido>> ListaAsync() => await _servMongo.Contenidos.Find(_ => true).ToListAsync();
        public async Task<Contenido?> ConsultarAsync(string id) =>
            await _servMongo.Contenidos.Find(c => c.Id == id).FirstOrDefaultAsync();
        public async Task<string> NuevoAsync(Contenido contenido)
        {
            await _servMongo.Contenidos.InsertOneAsync(contenido);

            return contenido.Id;
        }
        public async Task ModificarAsync(Contenido contenido) =>
            await _servMongo.Contenidos.ReplaceOneAsync(c => c.Id == contenido.Id, contenido);
        public async Task BorrarAsync(string id) =>
            await _servMongo.Contenidos.DeleteOneAsync(c => c.Id == id);

        public Contenido CrearUnContenidoDePrueba()
        {
            Contenido c = new Contenido()
            {
                Asignatura = new IdNombres()
                {
                    Id = 4,
                    Nombre = "Matematicas 1"
                },
                Profesor = new IdNombres()
                {
                    Id = 2,
                    Nombre = "Pepe Potamo"
                },
                Temas = new List<Tema>()
                {
                    new Tema()
                    {
                        Titulo = "Las sumas",
                        Parrafos = new List<string>
                        {
                            "adsflkk jañsdfl jñasldkj ñasldkj ñasldk ñalksdf",
                            "asdfñ asldkj ñaslkdj dklfj ñasldkj ñalskd",
                            "dsaf klasdñfkj ñalskd",
                            "asdñll wlrjke ñqlkrje "
                        },
                        Etiqueta = new List<string>
                        {
                            "suma",
                            "sumando"
                        }
                    },
                    new Tema()
                    {
                        Titulo = "Las resta",
                        Parrafos = new List<string>
                        {
                            "adsflkk jañsdfl jñasldkj ñasldkj ñasldk ñalksdf",
                            "asdfñ asldkj ñaslkdj dklfj ñasldkj ñalskd",
                            "dsaf klasdñfkj ñalskd",
                            "asdñll wlrjke ñqlkrje "
                        },
                        Etiqueta = new List<string>
                        {
                            "resta",
                            "entero"
                        }
                    }
                }
            };

            return c;
        }
    }
}
