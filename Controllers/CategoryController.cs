using frist_project_one.DTOs;
using frist_project_one.Interface;
using frist_project_one.Services;
using Microsoft.AspNetCore.Mvc;

namespace frist_project_one.Controllers
{
    [ApiController]
    [Route("/api/categories/")]
    public class CategoryController : ControllerBase
    {
        private InterfacecategoryService _categoryService;

        public CategoryController( InterfacecategoryService categoryServices)
        {
            _categoryService = categoryServices;
        }
        //GET: /api/categories/ => read categories

        [HttpGet]
        public async Task<IActionResult> GetCategories(
        [FromQuery] int PageNumber = 1,
        [FromQuery] int PageSize = 6,
        [FromQuery] string? search = null,
        [FromQuery] string? SortOrder = null
        )    {
        var CategoryReadList = await _categoryService.GetAllCategories(PageNumber, PageSize, search, SortOrder);

        return Ok(ApiResponse<PaginationResult<CategoryReadDto>>.SuccessResponse(
        CategoryReadList, 200, "Categories returned Successfully"));


        }//Get End
        
           //GET: /api/categories/{categoryId} => read a category By Id.!

        [HttpGet("{categoryId:guid}")]
        public async Task<IActionResult> GetCategoryById(Guid categoryID)
        {
            var category =await _categoryService.GetCategorySingleByID(categoryID);
           
            if (category == null)
    {
     return NotFound(ApiResponse<Object>.ErrorsResponse(new List<string> {"Category is not found With This ID."}, 400, "Validation Failde.!"));

     }
            return Ok(ApiResponse<CategoryReadDto>.SuccessResponse(category,200,"Categories returned is Successfully"));
        }//Get End

        //POST: /api/categories/ => Create A category

        [HttpPost]
        public async Task<IActionResult> PostCategory([FromBody] CategoryCreateDtos categoryData)
        {

            var newCategory = await _categoryService.CreateCategory(categoryData);

            return Created(nameof(GetCategoryById),ApiResponse<CategoryReadDto>.SuccessResponse(newCategory, 201, "Category create a Successfully")
            );
        }//post end

        //PUT: /api/categories/{categoryId} => Create Update..!
        
        [HttpPut("{categoryId:guid}")]
        public async Task<IActionResult> PutCategory(Guid categoryId, [FromBody] CategoryUpdateDto categoryData)
        {
            var UpdateCategory = await _categoryService.UpdateCategoryById(categoryId, categoryData);

            if (UpdateCategory == null)
            {
                return NotFound(ApiResponse<Object>.ErrorsResponse(new List<string> {"Category is not found With This ID."}, 400, "Validation Failde.!"));
            }

            return Ok(ApiResponse<CategoryReadDto>.SuccessResponse(UpdateCategory,200,"Create a update successfully")); //204

        }//Put End
        
        //Delete: /api/categories/{categoryId} => Delete  Category by Id..!

        [HttpDelete("{categoryId:guid}")]
        public async Task<IActionResult> DeleteCategory(Guid categoryId )
        {

        var FoundCategory =await _categoryService.DeleteCategoryById(categoryId);

        if (!FoundCategory)
        {
        return NotFound(ApiResponse<Object>.ErrorsResponse(new List<string> {"Category is not found With This ID."}, 404, "Validation Failde.!"));
        }   
        return Ok(ApiResponse<object>.SuccessResponse(null, 204, "Category delete is a successfully.!")); //204
        }//Get End

        
        
    }
}

