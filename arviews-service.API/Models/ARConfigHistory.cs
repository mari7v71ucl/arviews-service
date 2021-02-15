using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using arviews_service.API.Dtos;

namespace arviews_service.API.Models
{
    public class ARConfigHistory
    {
        public string ViewId { get; set; }
        public List<ARConfigDto> Configs { get; set; }
    }
}
