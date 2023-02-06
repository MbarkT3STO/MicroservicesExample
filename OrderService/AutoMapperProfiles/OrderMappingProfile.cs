using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using OrderService.CQRS.Commands;
using OrderService.Database.Entities;
using OrderService.DTOs;

namespace OrderService.AutoMapperProfiles;

public class OrderMappingProfile : Profile
{
    public OrderMappingProfile()
    {
        CreateMap<Order, OrderGetDto>();
        CreateMap<CreateOrderCommand, Order>();
    }
}
