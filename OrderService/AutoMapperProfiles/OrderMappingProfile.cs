using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using OrderService.Database.Entities;
using OrderService.DTOs;

namespace OrderService.AutoMapperProfiles;

public class OrderMappingProfile : Profile
{
    public OrderMappingProfile()
    {
        CreateMap<Order, OrderGetDto>();
    }
}
