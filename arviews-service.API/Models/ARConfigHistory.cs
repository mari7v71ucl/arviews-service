using System.Collections.Generic;
using arviews_service.API.Dtos;

namespace arviews_service.API.Models
{
    public class ARConfigHistory
    {
        public string ViewId { get; set; }
        public List<ARConfigDto> Configs { get; set; }
    }
}
