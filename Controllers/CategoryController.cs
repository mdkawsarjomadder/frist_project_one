using frist_project_one.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace frist_project_one.Controllers
{
    [ApiController]
    [Route("/api/categories/")]
    public class CategoryController : ControllerBase
    {
        private static List<Category> categories = new List<Category>();
        //GET: /api/categories/ => read categories

        [HttpGet]
        public IActionResult GetCategories([FromQuery] string searchValue = "")
        {
        var CategoryReadList = categories.Select(c => new CategoryReadDto
            {
                CategoryID = c.CategoryID,
                Name = c.Name,
                Description = c.Description,
                CategoryAt = c.CategoryAt
            }).ToList();
            return Ok(ApiResponse<List<CategoryReadDto>>.SuccessResponse(CategoryReadList,200,"Categories returned Successfully"));
        }//Get End

        //POST: /api/categories/ => Create A category

        [HttpPost]
        public IActionResult PostCategory([FromBody] CategoryCreateDtos categoryData)
        {

            var newCategory = new Category
            {
                CategoryID = Guid.NewGuid(),
                Name = categoryData.Name,
                Description = categoryData.Description,
                CategoryAt = DateTime.UtcNow,

            };
            categories.Add(newCategory);
            var CategoryReadDtoTwo = new CategoryReadDto
            {
                CategoryID = newCategory.CategoryID,
                Name = newCategory.Name,
                Description = newCategory.Description,
                CategoryAt = newCategory.CategoryAt
            };
            return Created($"/api/categories/{newCategory.CategoryID}", 
            ApiResponse<CategoryReadDto>.SuccessResponse(CategoryReadDtoTwo,201,"Category create a Successfully")
            );
        }//post end

        //PUT: /api/categories/{categoryId} => Create Update..!
        [HttpPut("{categoryId:guid}")]
        public IActionResult PutCategory(Guid categoryId, [FromBody] CategoryUpdateDto categoryData)
        {
            var FoundCategory = categories.FirstOrDefault(category => category.CategoryID == categoryId);

            if (FoundCategory == null)
            {
                return NotFound(ApiResponse<Object>.ErrorsResponse(new List<string> {"Category is not found With This ID."}, 400, "Validation Failde.!"));
            }
                FoundCategory.Name = categoryData.Name;
                FoundCategory.Description = categoryData.Description;

            return Ok(ApiResponse<object>.SuccessResponse(null,204,"Create a update successfully")); //204

        }//Put End
        
        //Delete: /api/categories/{categoryId} => Delete  Category by Id..!

        [HttpDelete("{categoryId:guid}")]
        public IActionResult DeleteCategory(Guid categoryId)
        {
            var FoundCategory = categories.FirstOrDefault(category => category.CategoryID == categoryId);

    if (FoundCategory == null)
    {
     return NotFound(ApiResponse<Object>.ErrorsResponse(new List<string> {"Category is not found With This ID."}, 400, "Validation Failde.!"));

     }
  
     categories.Remove(FoundCategory);
    return Ok(ApiResponse<object>.SuccessResponse(null, 204, "Category delete is a successfully.!")); //204
        }//Get End

        
        
    }
}

