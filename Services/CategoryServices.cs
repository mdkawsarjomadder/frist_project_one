using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using frist_project_one.Data;
using frist_project_one.DTOs;
using frist_project_one.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace frist_project_one.Services
{
    public class CategoryServices:InterfacecategoryService
    {
        // private static readonly List<Category> _categories = new List<Category>();
        private readonly AppDbContext _appDbContext; //Database ..!`
        private readonly IMapper _mapper;  //Mapper...!

        public CategoryServices(AppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }


        public async  Task<List<CategoryReadDto>> GetAllCategories()  //Get Category All.
        {
            /*            
                        // return _categories.Select(c => new CategoryReadDto
                        // {
                        //     CategoryID = c.CategoryID,
                        //     Name = c.Name,
                        //     Description = c.Description,
                        //     CategoryAt = c.CategoryAt
                        // }).ToList();
            */
            var categories = await _appDbContext.Categories.ToListAsync();
            return _mapper.Map<List<CategoryReadDto>>(categories);
        }  //Get  End


        public async Task<CategoryReadDto?> GetCategorySingleByID(Guid categoryID)  //Get  Single Id In Category. 
        {

            var FoundCategory = await _appDbContext.Categories.FindAsync(categoryID);
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

        public async Task<CategoryReadDto> CreateCategory(CategoryCreateDtos categoryData) //create a category POST..!
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

            await _appDbContext.Categories.AddAsync(newCategory);
            await _appDbContext.SaveChangesAsync();
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

        public async Task<CategoryReadDto?> UpdateCategoryById(Guid categoryId, CategoryUpdateDto categoryData)  //Get  Single Id In Category. 
        {
            var FoundCategory = await _appDbContext.Categories.FindAsync(categoryId);

            if (FoundCategory == null)
            {
                return null;
            }

            /*
                        // FoundCategory.Name = categoryData.Name;
                        // FoundCategory.Description = categoryData.Description;
            */
            _mapper.Map(categoryData, FoundCategory);

             _appDbContext.Categories.Update(FoundCategory); 
           await _appDbContext.SaveChangesAsync();
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

        public async Task<bool> DeleteCategoryById(Guid categoryId)  //Delete a Category. 
        {
            var FoundCategory = await _appDbContext.Categories.FindAsync(categoryId);
            if (FoundCategory == null)
            {
                return false;
            }
            _appDbContext.Categories.Remove(FoundCategory);
            await _appDbContext.SaveChangesAsync();
            return true;

        }//Delete a category..!


        }
}



