using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthService.Database.Entities;
using AuthService.Models;
using AutoMapper;

namespace AuthService.AutoMapperProfiles;

public class RefreshTokenMappingProfile : Profile
{
    public RefreshTokenMappingProfile()
    {
        CreateMap<RefreshToken, RefreshTokenGetModel>();

        CreateMap<RefreshTokenCreateModel, RefreshToken>();
    }
}
