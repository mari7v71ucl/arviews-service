using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace arviews_service.API.Dtos
{
    public class ARConfigDto
    {
        public DateTime? CreatedTimestamp { get; set; }
        public Dictionary<string, string> Properties { get; set; }
    }
}
