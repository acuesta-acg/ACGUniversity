using Acg.University.DAL.MongoDB.Modelos;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acg.University.DAL.MongoDB
{
    public class ServMongoDb
    {
        private IMongoDatabase _database;

        public ServMongoDb(string cConex, string nombreBD)
        {
            var c = new MongoClient(cConex);
            _database = c.GetDatabase(nombreBD);
        }

        public IMongoCollection<Contenido> Contenidos => _database.GetCollection<Contenido>("Contenidos");
    }
}
