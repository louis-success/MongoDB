
**Advanced Queries**

Aggregation is a pipeline 
Pipelines are composed of stages, broad units of work
Within stages, expressions are used to define the unit of work
expressions $expr are nothing but functions.

$Match   => $project  => $group
```
var matchStage = new BsonDocument("$match", new BsonDocument("Actor","Vadivelu"));
var projectStage = new BsonDocument("$project", new BsonDocument{
  {"\_id":0},
  {"Actor":1},
  {"Take Home": "$Salary"}   //{"<Cutom Title>", "<Actual Field">}
  }
var sortStage = new BsonDocument("$sort", new BsonDocument("rating", -1));
var limitStage = new BsonDocument("$limit, 10);

var pipeline = PipelineDefinition<Movie,BsonDocument>.Create(BsonDocument []{
  matchStage,
  projectStage,
  sortStage,
  limtStage
});

\_movieCollections.Aggregate(pipeline).ToList();
```
**WriteConcern**

MongoDB cluster are using ReplicaSet with multiple instances.
Cluster => Replicaset with 3 nodes [1 primary node - mongodb instance, 2 secondary nodes -mongodb instances]

When we issue InserOne() InsertMany() => MongoDB driver inserts document in to the primary node and acknowledge the insert to the driver.

WriteConcern({W:1}) => **DefaultWriteConcern**only one node write it and sends ack to client. only primary node is getting the document insert adn replication to secondary instances may happen later after the primary instance acknowledge the insert 
WriteConcern({W:2}) => One primary node and one secondary node is done with insert then acknowledge the insert operation
WriteConcern({W:3}) => One primary node and two secondary nodes are done with insert and then acknowledge the insert operation.

WriteConcern({W:0})  => No acknowledge is needed client will fire the insert and forget

w:majority => write into majority of the secondary nodes before sending ack

```
await _usersCollection
   .WithWriteConcern(WriteConcern.WMajority)
   .InsertOneAsync(user, cancellationToken: cancellationToken);
   
   .WithWriteConcern({w:2}).InsertOne();


```

********Joins******

$lookup is the pipe we are using for this in the pipeline


Compass UI  "Aggregation framework"

$Match Stage :

```
{
  "year":{"$gt":"1980", "$lt":"1990"}
}
```
$lookup Stage
```
{
  from: 'comments',
  //localField: '_id',
  //foreignField: 'movie_id',
  let:{'id':'$_id'},
  pipeline:[{'$match':{'$expr':{'$eq':['$movie_id','$$id']}}},{'$count':'count'}],
  as: 'Movie_comments'
}
```

When we export to C# language

```
new BsonArray
{
    new BsonDocument("$match", 
    new BsonDocument("year", 
    new BsonDocument
            {
                { "$gt", "1980" }, 
                { "$lt", "1990" }
            })),
    new BsonDocument("$lookup", 
    new BsonDocument
        {
            { "from", "comments" }, 
            { "let", 
    new BsonDocument("id", "$_id") }, 
            { "pipeline", 
    new BsonArray
            {
                new BsonDocument("$match", 
                new BsonDocument("$expr", 
                new BsonDocument("$eq", 
                new BsonArray
                            {
                                "$movie_id",
                                "$$id"
                            }))),
                new BsonDocument("$count", "count")
            } }, 
            { "as", "Movie_comments" }
        })
}
```

**Perform the same lookup with MQL**

```
var filter = new BsonDocument[]
{
  new BsonDocument("$match", 
    new BsonDocument("year",  
      new BsonDocument{
        {"$gt", 1980},
        {"$lt", 1990}
      })),
  new BsonDocument("$lookup", 
    new BsonDocument{
      {"from":"comment"},
      {"let": new BsonDocument("id","_id")},
      {"pipeline": BsonArray{
        new BsonDocument("$match",
          new BsonDocument("$expr",
            new BsonDocument("$eq",
                new BsonArray {
                "$movie_id",
                "$$id"
                }
             )
           )
         ),
         new BsonDocument("$count", "count")
         }
       },
       {"as":"movie_comments"}
       
       })
 };
         
 var pipeline = PipelineDefinition<Movie,BsonDocument>.Create(filter);
 
 var movies = _moviesCollection.Aggreation(pipeline).toList();
 ```
 **Perform the same lookup with MongoDB C# driver**


```
var movies = _moviesCollection.Aggregate()
            .Match(m=>(int)m.year <1990 && (int)m.year>1980)
            .LookUp(
              _commentsCollection,
              m=>m.id,
              c=>c.movie_id,
              (movie m)=>m.comments).ToList()
              
 
 ```
 **Read Concern**
 
 AS WriteConcern, ReadConcern allows to specify the read isolation between nodes.
 
 local       => Reads the data from the primary node
 Majority    => Reads the data which replicated in majority of the nodes.
 
 **BulkWrite**
 
 Multiple runtriple to update multiple writes. To avoid it, send the documents in bulk and get only one ack for the bulkwrite. With this we can achieve single roundtrip for the bulk write.
 
 db.collection.bulkWrite([
 updateOne:{}
 updateOne:{}
 ],
 {ordered:false}
 );
 
 By default mongodb is ordered, any single write is failed then subsequents writes will not happen and bulk ack will return false
 
