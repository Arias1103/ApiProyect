using System;
using System.IO;
using ApiProyect.DTOs;
using ApiProyect.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace ApiProyect.Helpers
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Branch, BranchDTO>().ReverseMap();
            CreateMap<CreationBranchDTO, Branch>();

            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<CreationProductDTO, Product>();
            
        }
    }
}
