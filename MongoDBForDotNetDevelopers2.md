
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


