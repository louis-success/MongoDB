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
            var connString = "mongodb+srv://m220student:m220password@mflix.gcgrf.mongodb.net/myFirstDatabase?retryWrites=true&w=majority";
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

            //var mongoClient = new MongoClient("mongodb+srv://m220student:m220password@mflix.gcgrf.mongodb.net/myFirstDatabase?retryWrites=true&w=majority");
            //var mongoDB = client.GetDatabase("sample_mflix");
            //var collection = mongoDB.GetCollection<BsonDocument>("movies");
            //var document = collection.Find("{title: 'The Princess Bride'}").FirstOrDefault();
            //Console.WriteLine("*****Search Document*****");
            //Console.Write(document);

        }
    }
}
