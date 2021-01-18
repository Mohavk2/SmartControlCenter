using AutoMapper;
using CommonInfrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestData.Models;

namespace HostWeb.Mapping
{
    public class ScriptProfile : Profile
    {
        public ScriptProfile()
        {
            CreateMap<ScriptP, ScriptDTO>();
        }
    }
}
