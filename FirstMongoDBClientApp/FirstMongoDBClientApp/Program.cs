using System;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FirstMongoDBClientApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //var connString = "mongodb+srv://m220student:m220password@mflix.gcgrf.mongodb.net/myFirstDatabase?retryWrites=true&w=majority";
            var connString = "mongodb://m220student:m220password@mflix-shard-00-00.gcgrf.mongodb.net:27017,mflix-shard-00-01.gcgrf.mongodb.net:27017,mflix-shard-00-02.gcgrf.mongodb.net:27017/myFirstDatabase?ssl=true&replicaSet=atlas-ppqo21-shard-0&authSource=admin&retryWrites=true&w=majority";
            var client = new MongoClient(connString);
            var db = client.GetDatabase("sample_mflix");
            var collection = db.GetCollection<BsonDocument>("movies");
            var result = collection.Find(new BsonDocument())
               .SortByDescending(m => m["runtime"])
               .Limit(10)
               .ToList();

            foreach (var r in result)
            {
                Console.WriteLine(r.GetValue("title"));
            }

        }
    }
}
