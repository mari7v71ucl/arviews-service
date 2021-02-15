using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using arviews_service.API.Dtos;
using arviews_service.API.Models;
using AutoMapper;

namespace arviews_service.API.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ARConfig, ARConfigDto>();
            CreateMap<Workspace, WorkspaceDto>();
        }
    }
}
