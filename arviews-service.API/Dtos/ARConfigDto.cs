using System;
using System.Collections.Generic;

namespace arviews_service.API.Dtos
{
    public class ARConfigDto
    {
        public DateTime? CreatedTimestamp { get; set; }
        public Dictionary<string, string> Properties { get; set; }
    }
}
