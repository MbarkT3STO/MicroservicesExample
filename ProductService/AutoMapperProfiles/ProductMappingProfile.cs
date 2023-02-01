using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProductService.Database.Entities;
using ProductService.DTOs;

namespace ProductService.AutoMapperProfiles;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<Product, ProductGetDto>();
    }
}
