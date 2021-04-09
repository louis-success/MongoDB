
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
There are many ways to work with MongoDB driver.

No single approach is right way
**MQL for price >400 & price<600:** Pass MQL as string

db.collection.find({"$and":[{"price":{"$lt":"400"},{"price":{"$gt":"600"}}]})

**Bson document syntax:**.  Use nested bson document

var filters= new BsonDocument("$and,new BsonArray{
new BsonDocument("price", new BsonDocument("$gt", 400)),
new BsonDocument("price", new BsonDocument("lt", 600))
});

**Builders syntax:**


MongoDB Drivers[MongoDB.Drivers] provides Builders class

```
using MongoDB.Drivers

var builder = Builders<BsonDocument C# Model Type i.e., Car>.Filters;

var filter = builder.Gt("price", 400) & builder.Lt("price", 600);

var client = new MongoClient("connectionString");
var db = client.GetDatabase("DbName");
var collection = db.GetCollection("CollectionName");

var result = collection.find(filter);

```
using your c# mongo db driver c# model class is mapped with Mongo db Bson Document.

It will takes care of pascal convension C# property to lower case mongo db document elements.
also any C# model class property is mapped with \_id property of mongodb bson document by decorating the c# property with [BsonId] attribute.

**Mapping Class**
```

[BsonId]
public int Id {get; set;}     //will be mapped to \_id of mongo db bsondodcument

public double Price {get; set;}    //will be mapped to price element of mongodb bsondocument

Also you can map any field to specific bson document element/property using [BsonElement("<PropertyName>")] attribute

{
_id : 0,
make : "Maruti",
model: "Swift",
price: 700000
}


public class Car
{
[BsonId]
public int Id {get; set; }
[BsonElement("make")]
public string Manufacturer {get; set; }
[BsonElement("model")]
public string ModelName {get; set; }
[BsonElement("price")]
public double SellingPrice {get; set; }
}

}
```

**LINQ with the MongoDB Driver**
```
var client = new MongoClient("connectionString");
var db = client.GetDatabase("Dealers");
var collection = db.GetCollection<Car>("cars");

var result = collection.find(car=>car.price >1000000).ToList();
```

var car = new Car(){
Id =1,
Manufacturer = "MAruti",
ModelName = "Swift",
SellingPrice = 700000
};
**InSert Record**
collection.InsertOne(car);

**Update Record**
var filter = new BsonDocument("Id",1);
var replace = new BsonDocument{{"Id" ,123},{"price" , 800000}};
var result = collection.ReplaceOne(filter,replace); //**ReplaceOne(filter, replace);**

var filter = new BsonDocument("Id",1);
var replace = new BsonDocument("$set", new BsonDocument("price",800000));
var updateOptions = new UpdateOptions {"IsUpsert"= 1};
var result = collection.UpdateOne(filter,replace, updateOptions); //**UpdateOne(filter, replace, updateOptions);**

**Update with Mapping Class, Builder & LINQ**

```
var filter = Builder<Car>.Filter.Eq(c=>c.Id, 1);
var replacement = Builder<Car>.Update.Set(c=>c.SellingPrice, 800000);
collection.UpdateOne(filter, replacement);
```

**Delete record**

collection.DeleteOne(c=>c.Id == 1);



