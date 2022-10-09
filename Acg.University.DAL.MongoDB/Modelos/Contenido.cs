using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acg.University.DAL.MongoDB.Modelos
{
    public class Contenido
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public IdNombres Asignatura { get; set; }
        public IdNombres Profesor { get; set; }
        public List<Tema> Temas { get; set; }
    }
}
