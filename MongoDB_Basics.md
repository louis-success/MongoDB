


**Update Document through shell**

$inc, $set, $push

```mongo "mongodb+srv://<username>:<password>@<cluster>.mongodb.net/admin"
use sample_training

db.zips.find({ "zip": "12534" }).pretty()

db.zips.find({ "city": "HUDSON" }).pretty()

db.zips.find({ "city": "HUDSON" }).count()

db.zips.updateMany({ "city": "HUDSON" }, { "$inc": { "pop": 10 } })

db.zips.updateOne({ "zip": "12534" }, { "$set": { "pop": 17630 } })

db.zips.updateOne({ "zip": "12534" }, { "$set": { "population": 17630 } })

db.grades.find({ "student_id": 151, "class_id": 339 }).pretty()

db.grades.find({ "student_id": 250, "class_id": 339 }).pretty()

db.grades.updateOne({ "student_id": 250, "class_id": 339 },
                    { "$push": { "scores": { "type": "extra credit",
                                             "score": 100 }
                                }
                     })
```
                     
                     
**Delete Documents through shell**

Delete documents that matches with the given query:


db.<collection_name>.find("Key":"value").deleteOne() - delete any one document that matches with the query
db.<collection_name>.find("key":"value").deleteMany() - delete all the documents that matches with the query

```

use sample_training

db.inspections.find({ "test": 1 }).pretty()

db.inspections.find({ "test": 3 }).pretty()

**db.inspections.deleteMany({ "test": 1 })

db.inspections.deleteOne({ "test": 3 })**

db.inspection.find().pretty()

show collections

**Drop the insepections collections**
db.<collection_Name>.drop()

db.inspection.drop()
```

**Comparison Operators**

|operator|Description|
|--------|-----------|
|$eq|equal to =|
|$ne|not equal to| !=
|$lt|less than equal to <|
|$gt|greater than equal to >|
|$lte|less than or equal to <=|
|$gte|greater than or equal to >=|


**Syntax:**
{"field":{"operator":"value"}}

**Example**
{"salary":{"$gte":100000}}

Query operators provides additional ways to locate data within the database.
Comparision operators allows us to find data in the specified range.

{<field>:{<operator>:<value>}}
  
  $eq is the default operator if it is not specified.

```
  use sample_training
  
  db.trips.find({ "tripduration": { "$lte" : 70 },
                "usertype": { "$ne": "Subscriber" } }).pretty()

db.trips.find({ "tripduration": { "$lte" : 70 },
                "usertype": { "$eq": "Customer" }}).pretty()

db.trips.find({ "tripduration": { "$lte" : 70 },
                "usertype": "Customer" }).pretty()

```

**Logical Query operators**

|operator|Description|
|--------|-----------|
|$and|Match all query clauses|
|$or|Match atleast one query clause| 
|$nor|Fail to match both the given clauses|
|$not|Negate the query requirement|
|$lte|less than or equal to <=|


**Syntax:**

{"$<operator>":[{<clause1>},{<clause2>}]}
  
{$not:{<clause1>}}


```

db.routes.find({ "$and": [ { "$or" :[ { "dst_airport": "KZN" },
                                    { "src_airport": "KZN" }
                                  ] },
                          { "$or" :[ { "airplane": "CR2" },
                                     { "airplane": "A81" } ] }
                         ]}).pretty()


```

**Expressive Query Operators - Aggregation expressions **

$FieldName used to get the value of the field in expression

$expr

**Syntax:**

{"$<operator>":{"<field>":"<Value>"}}

```
db.trips.find({ "$expr": { "$eq": [ "$end station id", "$start station id"] }
              }).count()


db.trips.find({ "$expr": { "$and": [ { "$gt": [ "$tripduration", 1200 ]},
                         { "$eq": [ "$end station id", "$start station id" ]}
                       ]}}).count()
```


**Array Operators**

$push 

  * Add an element into an array
  * Turns a field into an array field 


Syntax:

{"<array_field>": {"$size":<valueNumber>}} => size of the array field should match with the valuenumber exactly
  
{"<array_field>": {"$all":<array>}}. => array elements should match with all the given array elements regardless of their order
  

```
mongo "mongodb+srv://<username>:<password>@<cluster>.mongodb.net/admin"

use sample_airbnb

db.listingsAndReviews.find({ "amenities": {
                                  "$size": 20,
                                  "$all": [ "Internet", "Wifi",  "Kitchen",
                                           "Heating", "Family/kid friendly",
                                           "Washer", "Dryer", "Essentials",
                                           "Shampoo", "Hangers",
                                           "Hair dryer", "Iron",
                                           "Laptop friendly workspace" ]
                                         }
                            }).pretty()

```

More functions

db.<collections>.find({<query>},{<projections>});
  
 Projections specifies which field should or should not be included in the result set cursor.
 
 field: 1 should include
 field: 0 should not include
 
 we have to use either 1 for all field or 0 we can't mix 1 & 0
 the only case where we can mix is to use 0 for \_id:0, field:1
 
 {<field>:{"$<elemMatch>":{"field":<value>}}}   => match the individual field value of an sub document.

```
db.listingsAndReviews.find({ "amenities":
        { "$size": 20, "$all": [ "Internet", "Wifi",  "Kitchen", "Heating",
                                 "Family/kid friendly", "Washer", "Dryer",
                                 "Essentials", "Shampoo", "Hangers",
                                 "Hair dryer", "Iron",
                                 "Laptop friendly workspace" ] } },
                            {"price": 1, "address": 1}).pretty()


db.listingsAndReviews.find({ "amenities": "Wifi" },
                           { "price": 1, "address": 1, "_id": 0 }).pretty()


db.listingsAndReviews.find({ "amenities": "Wifi" },
                           { "price": 1, "address": 1,
                             "_id": 0, "maximum_nights":0 }).pretty()


```

```
use sample_training

db.grades.findOne()

db.grades.find({ "class_id": 431 },
               { "scores": { "$elemMatch": { "score": { "$gt": 85 } } }
             }).pretty()

db.grades.find({ "scores": { "$elemMatch": { "type": "extra credit" } }
               }).pretty()

```

