﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;

namespace LicentaApp.Models
{
    [CollectionName("feedback")]
    public class FeedbackModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("id")]
        public string Id { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("message")]
        public string Message { get; set; }
    }
}
