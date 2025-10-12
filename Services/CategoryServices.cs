using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using frist_project_one.DTOs;
using frist_project_one.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace frist_project_one.Services
{
    public class CategoryServices:InterfacecategoryService
    {
        private static readonly List<Category> _categories = new List<Category>();

        private readonly IMapper _mapper;  //Mapper...!

        public CategoryServices(IMapper mapper)
        {
            _mapper = mapper;
        }

        public List<CategoryReadDto> GetAllCategories()  //Get Category All.
        {
            // return _categories.Select(c => new CategoryReadDto
            // {
            //     CategoryID = c.CategoryID,
            //     Name = c.Name,
            //     Description = c.Description,
            //     CategoryAt = c.CategoryAt
            // }).ToList();
            return _mapper.Map<List<CategoryReadDto>>(_categories);
        }  //Get  End

        public CategoryReadDto? GetCategorySingleByID(Guid categoryID)  //Get  Single Id In Category. 
        {
            var FoundCategory = _categories.FirstOrDefault(c => c.CategoryID == categoryID);
            return FoundCategory == null ? null : _mapper.Map<CategoryReadDto>(FoundCategory);
/*
            // if (FoundCategory == null)
            // {
            //     return null;
            // }
            //return new CategoryReadDto
            // {
            //     CategoryID = FoundCategory.CategoryID,
            //     Name = FoundCategory.Name,
            //     Description = FoundCategory.Description,
            //     CategoryAt = FoundCategory.CategoryAt
            // };
            // return _mapper.Map<CategoryReadDto>(FoundCategory);
*/            
        } //Get Single End

        public CategoryReadDto CreateCategory(CategoryCreateDtos categoryData) //create a category POST..!
        {
/*            
            // var newCategory = new Category
            // {
            //     CategoryID = Guid.NewGuid(),
            //     Name = categoryData.Name,
            //     Description = categoryData.Description,
            //     CategoryAt = DateTime.UtcNow,

            // };
*/            
            var newCategory = _mapper.Map<Category>(categoryData);
            newCategory.CategoryID = Guid.NewGuid();
            newCategory.Description = categoryData.Description;

            _categories.Add(newCategory);
/*
            // return new CategoryReadDto
            // {
            //     CategoryID = newCategory.CategoryID,
            //     Name = newCategory.Name,
            //     Description = newCategory.Description,
            //     CategoryAt = newCategory.CategoryAt
            // };
*/            
            return _mapper.Map<CategoryReadDto>(newCategory);
        } // create a category post END.!

        public CategoryReadDto? UpdateCategoryById(Guid categoryId, CategoryUpdateDto categoryData)  //Get  Single Id In Category. 
        {
            var FoundCategory = _categories.FirstOrDefault(category => category.CategoryID == categoryId);

            if (FoundCategory == null)
            {
                return null;
            }

/*
            // FoundCategory.Name = categoryData.Name;
            // FoundCategory.Description = categoryData.Description;
*/
            _mapper.Map(categoryData, FoundCategory);
/*
            // return new CategoryReadDto
            // {
            //     CategoryID = FoundCategory.CategoryID,
            //     Name = FoundCategory.Name,
            //     Description = FoundCategory.Description,
            //     CategoryAt = FoundCategory.CategoryAt
            // };
*/
            return _mapper.Map<CategoryReadDto>(FoundCategory);



        } //Get Single End

        public bool DeleteCategoryById(Guid categoryId)  //Delete a Category. 
        {
            var FoundCategory = _categories.FirstOrDefault(category => category.CategoryID == categoryId);
            if (FoundCategory == null)
            {
                return false;
            }
            _categories.Remove(FoundCategory);
            return true;

        }//Delete a category..!


        }
}



