using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using frist_project_one.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace frist_project_one.Services
{
    public class CategoryServices
    {
        private static readonly List<Category> _categories = new List<Category>();

        public List<CategoryReadDto> GetAllCategories()  //Get Category All.
        {
            return _categories.Select(c => new CategoryReadDto
            {
                CategoryID = c.CategoryID,
                Name = c.Name,
                Description = c.Description,
                CategoryAt = c.CategoryAt
            }).ToList();
        }  //Get  End

        public CategoryReadDto? GetCategorySingleByID(Guid categoryID)  //Get  Single Id In Category. 
        {
            var FoundCategory = _categories.FirstOrDefault(c => c.CategoryID == categoryID);
            if (FoundCategory == null)
            {
                return null;
            }
            return new CategoryReadDto
            {
                CategoryID = FoundCategory.CategoryID,
                Name = FoundCategory.Name,
                Description = FoundCategory.Description,
                CategoryAt = FoundCategory.CategoryAt
            };
        } //Get Single End

        public CategoryReadDto CreateCategory(CategoryCreateDtos categoryData) //create a category POST..!
        {
            var newCategory = new Category
            {
                CategoryID = Guid.NewGuid(),
                Name = categoryData.Name,
                Description = categoryData.Description,
                CategoryAt = DateTime.UtcNow,

            };
            _categories.Add(newCategory);

            return new CategoryReadDto
            {
                CategoryID = newCategory.CategoryID,
                Name = newCategory.Name,
                Description = newCategory.Description,
                CategoryAt = newCategory.CategoryAt
            };
        } // create a category post END.!

        public CategoryReadDto? UpdateCategoryById(Guid categoryId, CategoryUpdateDto categoryData)  //Get  Single Id In Category. 
        {
            var FoundCategory = _categories.FirstOrDefault(category => category.CategoryID == categoryId);

            if (FoundCategory == null)
            {
                return null;
            }
            FoundCategory.Name = categoryData.Name;
            FoundCategory.Description = categoryData.Description;

            return new CategoryReadDto
            {
                CategoryID = FoundCategory.CategoryID,
                Name = FoundCategory.Name,
                Description = FoundCategory.Description,
                CategoryAt = FoundCategory.CategoryAt
            };



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



