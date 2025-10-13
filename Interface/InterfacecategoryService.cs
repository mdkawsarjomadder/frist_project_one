using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using frist_project_one.DTOs;

namespace frist_project_one.Interface
{
    public interface InterfacecategoryService
    { 
/*         List<CategoryReadDto> GetAllCategories(); // Get all categories

        CategoryReadDto? GetCategorySingleByID(Guid categoryID); // Get single category by ID

        CategoryReadDto CreateCategory(CategoryCreateDtos categoryData); // Create new category (POST)

        CategoryReadDto? UpdateCategoryById(Guid categoryId, CategoryUpdateDto categoryData); // Update category by ID

        bool DeleteCategoryById(Guid categoryId); // Delete category by ID
*/
    
    /*Asynchronous add */
     
        Task<List<CategoryReadDto>> GetAllCategories(); // Get all categories

        Task<CategoryReadDto?> GetCategorySingleByID(Guid categoryID); // Get single category by ID

        Task<CategoryReadDto> CreateCategory(CategoryCreateDtos categoryData); // ✅ Fixed spelling here

        Task<CategoryReadDto?> UpdateCategoryById(Guid categoryId, CategoryUpdateDto categoryData); // Update category by ID

        Task<bool> DeleteCategoryById(Guid categoryId); // Delete category by ID
    }
}