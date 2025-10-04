var builder = WebApplication.CreateBuilder(args);


//Swagger_Server Setup:------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => "Hello.! Welcome To  Method..!");
var products = new List<Product>()
{
    new Product("Redmi Node7",20000),
    new Product("Redmi Node10",22000)
};

app.MapGet("/products", () =>
{
    
     return Results.Ok(products); //200
});


app.Run();


//DTO------------------!
public record Product(string Name, decimal Price);


