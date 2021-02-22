using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace arviews_service.API.Models
{
    public class ARConfig
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required]
        public string ViewId { get; set; }
        public DateTime? CreatedTimestamp { get; set; }
        [Required]
        public Dictionary<string, string> Properties { get; set; }
    }
}
