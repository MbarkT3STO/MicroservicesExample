using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CustomerService.Database.Entities;
using CustomerService.DTOs;

namespace CustomerService.AutoMapperProfiles;

public class CustomerMappingProfile : Profile
{
    public CustomerMappingProfile()
    {
        CreateMap<Customer, CustomerGetDto>();
    }
}
