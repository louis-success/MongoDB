
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

**MQL for price >400 & price<600:**

db.collection.find({"$and":[{"price":{"$lt":"400"},{"price":{"$gt":"600"}}]})

**Bson document syntax:**

var filters= new BsonDocument("$and,new BsonArray{
new BsonDocument("price", new BsonDocument("$gt", 400)),
new BsonDocument("price", new BsonDocument("lt", 600))
});

**Builders syntax:**


MongoDB Drivers[MongoDB.Drivers] provides Builders class

```
using MongoDB.Drivers

var builder = Builders<BsonDocument>.Filters;

var filter = builder.Gt("price", 400) & builder.Lt("price", 600);

var client = new MongoClient("connectionString");
var db = client.GetDatabase("DbName");
var collection = db.GetCollection("CollectionName");

var result = collection.find(filter);

```

