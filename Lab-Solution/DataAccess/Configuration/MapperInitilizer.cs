using AutoMapper;
using BussinessObject.Models;
using DataAccess.Dtos;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Configuration
{
    public class MapperInitilizer : Profile
    {
        public MapperInitilizer()
        {
            CreateMap<ApplicationUser, RegisterUserDTO>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailDTO>().ReverseMap();
            CreateMap<Order, OrderDTO>()
                .ForMember(dest => dest.orderDetailDTOs, act => act.MapFrom(src => src.OrderDetails))
                .ReverseMap();
        }
    }
}
