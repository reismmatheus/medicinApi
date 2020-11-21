using AutoMapper;
using MedicinApi.Repositories.Model;
using MedicinApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicinApi.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Models.Auth, Repositories.Model.User>();
            //CreateMap<RegisterModel, Models.User>();
            //CreateMap<UpdateModel, Models.User>();
        }
    }
}
