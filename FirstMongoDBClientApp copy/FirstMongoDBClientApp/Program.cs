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
            var mongoClient = new MongoClient("mongodb+srv://m220student:m220password@mflix.gcgrf.mongodb.net/sample_mflix?retryWrites=true&w=majority");
            var mongoDB = mongoClient.GetDatabase("sample_mflix");
            //var collections = mongoDB.ListCollections().ToList();
            //Console.WriteLine("*****Available Collections*****");
            //foreach (var documentCollection in collections)
            //{
            //    Console.WriteLine($"{documentCollection..do}");
            //}
            var collection = mongoDB.GetCollection<BsonDocument>("movies");
            var document = collection.Find("{title: 'Blacksmith Scene'}").FirstOrDefault();
            Console.WriteLine("*****Search Document*****");
            Console.Write(document);

        }
    }
}
