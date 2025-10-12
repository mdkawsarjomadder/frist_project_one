using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using frist_project_one.DTOs;

namespace frist_project_one.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryReadDto>();
            CreateMap<CategoryCreateDtos, Category>();
            CreateMap<CategoryUpdateDto, Category>();
        }
    }
}