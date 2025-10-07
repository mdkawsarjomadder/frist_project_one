using System.ComponentModel.DataAnnotations;

namespace frist_project_one.DTOs
{
    public class CategoryCreateDtos
    {
        [Required(ErrorMessage = "Category Name is Required.!")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Category name must be between 2 and 100 characters..!")]
        public string Name { get; set; } = string.Empty;

     [StringLength(500, ErrorMessage ="Category Description  cannot exceed 500 characters.!")]    
    public string Description { get; set; } = string.Empty;
    }
}