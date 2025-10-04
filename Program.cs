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
app.MapGet("/api/categories", () =>
{

    return Results.Ok(categories); //200
});
// ---------------POST:/api/categories Create Category?-----------------------!
app.MapPost("/api/categories", () =>
{

    var newCategory = new Category
    {
        // CategoryID = Guid.NewGuid(),
        CategoryID = Guid.Parse("35d876f6-ba77-4f49-bf4a-913c27dbe87b"),
        Name = "Laptop",
        Description = "Laptop is my persion document. ",
        CategoryAt = DateTime.UtcNow 
    };
    categories.Add(newCategory);
    return Results.Created($"/api/categories/{newCategory.CategoryID}",newCategory); //201
});

// ---------------PUT:/api/categories Update Category?-----------------------!
app.MapPut("/api/categories", () =>
{
    var FoundCategory = categories.FirstOrDefault(category => category.CategoryID == Guid.Parse("35d876f6-ba77-4f49-bf4a-913c27dbe87b"));

    if (FoundCategory == null)
    {
        return Results.NotFound("Category with this id Not Update exist.!");
     }

    FoundCategory.Name = "Smart Phone";
    FoundCategory.Description = "Smart Phone Is Nice Category.!";
    return Results.NoContent(); //204
     
});


 // ---------------DELETE:/api/categories DELETE Category?-----------------------!
app.MapDelete("/api/categories", () =>
{
    var FoundCategory = categories.FirstOrDefault(category => category.CategoryID == Guid.Parse("35d876f6-ba77-4f49-bf4a-913c27dbe87b"));

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
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime CategoryAt { get; set; }
};


