using AutoMapper;
using TestNivelatorio.Requests;
using TestNivelatorio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestNivelatorio.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<VehiculoRequest, Vehiculo>();
        }
    }
}
