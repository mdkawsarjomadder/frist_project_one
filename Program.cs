using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);


//Swagger_Server Setup:------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
// ------------------Swagger----------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

List<Category> categories = new List<Category>();

app.MapGet("/", () => "Hello.! Welcome To  Method..!");

// ---------------GET:/api/categories read Category?-----------------------!
app.MapGet("/api/categories", ( [FromQuery] string searchValue = "") =>
{

   
    if (!string.IsNullOrEmpty(searchValue))
    {
        var searchValueCategories = categories.Where(c => c.Name.Contains(searchValue, StringComparison.OrdinalIgnoreCase)).ToList();
        return Results.Ok(searchValueCategories);
    }
    return Results.Ok(categories); //200
});
// ---------------POST:/api/categories Create Category?-----------------------!
app.MapPost("/api/categories", ([FromBody] Category categoryData) =>
{
    if (string.IsNullOrEmpty(categoryData.Name))
    {
        return Results.BadRequest("Category Name is  Required and can not be empty.!");
    }
    if (categoryData.Name.Length > 2)
        {
             return Results.BadRequest("Category Name  is Must Be Alteast 2 Chareaters.!");
        }

    var newCategory = new Category
    {   
        CategoryID = Guid.NewGuid(),
        Name = categoryData.Name,
        Description = categoryData.Description,
        CategoryAt = DateTime.UtcNow,

    };
    categories.Add(newCategory);
    return Results.Created($"/api/categories/{newCategory.CategoryID}",newCategory); //201
   
});

// ---------------PUT:/api/categories/{categoryId} Update Category?-----------------------!
app.MapPut("/api/categories/{categoryId:guid}", (Guid categoryId, [FromBody] Category categoryData) =>
{
    if (categoryData == null)
    {
        return Results.BadRequest("Category Data is missing.!");
    }
    var FoundCategory = categories.FirstOrDefault(category => category.CategoryID == categoryId);

    if (FoundCategory == null)
    {
        return Results.NotFound("Category with this id Not Update exist.!");
     } 
     
    if (!string.IsNullOrEmpty(categoryData.Name))
    {
        if (categoryData.Name.Length >= 2)
        {
            FoundCategory.Name = categoryData.Name;
        }
        else
        {
             return Results.BadRequest("Category Name  is Must Be Alteast 2 Chareaters.!");
        }
    }
    if (!string.IsNullOrWhiteSpace(categoryData.Description))
    {
        FoundCategory.Description = categoryData.Description;
    }
    
    return Results.NoContent(); //204
     
});


 // ---------------DELETE:/api/categories/{categoryId} DELETE Category?-----------------------!
app.MapDelete("/api/categories/{categoryId:guid}", (Guid categoryId) =>
{
    var FoundCategory = categories.FirstOrDefault(category => category.CategoryID == categoryId);

    if (FoundCategory == null)
    {
        return Results.NotFound("Category with this id Not exist.!");
     }
  
     categories.Remove(FoundCategory);
    return Results.NoContent(); //204
     
});


app.Run();


//DTO------------------!
public record class Category
{
    public Guid CategoryID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime CategoryAt { get; set; }
};


