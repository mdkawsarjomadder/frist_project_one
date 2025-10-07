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
            if (!string.IsNullOrEmpty(searchValue))
            {
                var searchValueCategories = categories.Where(c => c.Name.Contains(searchValue, StringComparison.OrdinalIgnoreCase)).ToList();
                return Ok(searchValueCategories);
            }
            return Ok(categories);
        }//Get End

        //POST: /api/categories/ => Create A category

        [HttpPost]
        public IActionResult PostCategory([FromBody] Category categoryData)
        {

            if (string.IsNullOrEmpty(categoryData.Name))
            {
                return BadRequest("Category Name is  Required and can not be empty.!");
            }
            if (categoryData.Name.Length < 2)
            {
                return BadRequest("Category Name  is Must Be Alteast 2 Chareaters.!");
            }

            var newCategory = new Category
            {
                CategoryID = Guid.NewGuid(),
                Name = categoryData.Name,
                Description = categoryData.Description,
                CategoryAt = DateTime.UtcNow,

            };
            categories.Add(newCategory);
            return Created($"/api/categories/{newCategory.CategoryID}", newCategory);
        }//post end

        //PUT: /api/categories/{categoryId} => Create Update..!
        [HttpPut("{categoryId:guid}")]
        public IActionResult PutCategory(Guid categoryId, [FromBody] Category categoryData)
        {

            if (categoryData == null)
            {
                return BadRequest("Category Data is missing.!");
            }
            var FoundCategory = categories.FirstOrDefault(category => category.CategoryID == categoryId);

            if (FoundCategory == null)
            {
                return NotFound("Category with this id Not Update exist.!");
            }

            if (!string.IsNullOrEmpty(categoryData.Name))
            {
                if (categoryData.Name.Length >= 2)
                {
                    FoundCategory.Name = categoryData.Name;
                }
                else
                {
                    return BadRequest("Category Name  is Must Be Alteast 2 Chareaters.!");
                }
            }
            if (!string.IsNullOrWhiteSpace(categoryData.Description))
            {
                FoundCategory.Description = categoryData.Description;
            }

            return NoContent(); //204

        }//Put End
        
        //Delete: /api/categories/{categoryId} => Delete  Category by Id..!

        [HttpDelete("{categoryId:guid}")]
        public IActionResult DeleteCategory(Guid categoryId)
        {
            var FoundCategory = categories.FirstOrDefault(category => category.CategoryID == categoryId);

    if (FoundCategory == null)
    {
        return NotFound("Category with this id Not exist.!");
     }
  
     categories.Remove(FoundCategory);
    return NoContent(); //204
        }//Get End

        
        
    }
}

