using AutoMapper;
using Common.Dto;
using Data.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Mapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<ItemDto, Item>().ReverseMap();
        }
    }
}
