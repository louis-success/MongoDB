


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





