
Relevant driver links:

    MongoClient
    MongoDatabase
    MongoCollection


The MongoDB driver is one such package that is managed by NuGet.
Nuget package manager has MongoDb.Driver package which we have to install and then do the below steps

  1. Create MongoDB client that point to your data store
  2. Specify the MongoDB database which you want to connect
  3. Specify the collection which you want to connect
  4. Get the data by doing CRUD operations on Collection

```
  var client = new MongoClient("<uri to mongodb>");
  var db = client.GetDatabase("<DB name>");
  var collection = db.GetCollection<BsonDocument>("<Collection name">);
  var data = collection.find("{id:1234}").FirstOrDefault();
  
```

