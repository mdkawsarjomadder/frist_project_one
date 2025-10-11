using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using frist_project_one.DTOs;

namespace frist_project_one.Services
{
    public class CategoryServices
    {
        private static readonly List<Category> categories = new List<Category>();

        public List<CategoryReadDto> GetAllCategories()
        {
            return categories.Select(c => new CategoryReadDto
            {
                CategoryID = c.CategoryID,
                Name = c.Name,
                Description = c.Description,
                CategoryAt = c.CategoryAt
            }).ToList();
        }
         
    }
}