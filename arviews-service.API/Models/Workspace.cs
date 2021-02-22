using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace arviews_service.API.Models
{
    public class Workspace
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [Required]
        public string WorkspaceId { get; set; } // provided by the integrated system
        public List<string> ArViews { get; set; }
        public bool IsClientAccessForbidden { get; set; }
    }
}
