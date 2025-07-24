using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;
using Domain;

namespace A
{
    public class B
    {
        private IMongoClient client;
        private IMongoDatabase database;

        public a()
        {
            // ruleid: csharp-mongo-hardcoded-secret
            new MongoClient(new MongoClientURI("mongodb://user:password@a.mongolab.com:37601/goparty"));
            // ok: csharp-mongo-hardcoded-secret
            new MongoClient(new MongoClientURI("mongodb://<user>:password@a.mongolab.com:37601/goparty"));
            // ok: csharp-mongo-hardcoded-secret
            new MongoClient(new MongoClientURI("mongodb://<user>:<password>@a.a.com:37601/goparty"));
            // ok: csharp-mongo-hardcoded-secret
            new MongoClient(new MongoClientURI("mongodb://a.mongolab.com:37601/goparty"));
        }
    }
}
