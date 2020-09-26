using AutoMapper;
using Voting.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Voting.DAL.SQLLite.Entities;

namespace Voting.Service
{
    public class Service_Mapper
    {
        public static IMapper mapper;
        public static void Map()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDto>();
                cfg.CreateMap<UserDto, User>();


                //cfg.CreateMap<TradesDto, MarketTimeSalesDto>().ForMember(dest => dest.ARB_NAME, opt => opt.Ignore())
                //.ForMember(dest => dest.reuters, opt => opt.Ignore());


            });


            mapper = config.CreateMapper();
        }
    }
}
