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
app.MapPut("/api/categories/{categoryId}", (Guid categoryId, [FromBody] Category categoryData) =>
{
    var FoundCategory = categories.FirstOrDefault(category => category.CategoryID == categoryId);

    if (FoundCategory == null)
    {
        return Results.NotFound("Category with this id Not Update exist.!");
     }

    FoundCategory.Name = categoryData.Name;
    FoundCategory.Description = categoryData.Description;
    return Results.NoContent(); //204
     
});


 // ---------------DELETE:/api/categories/{categoryId} DELETE Category?-----------------------!
app.MapDelete("/api/categories/{categoryId}", (Guid categoryId) =>
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
    public string? Description { get; set; }
    public DateTime CategoryAt { get; set; }
};


