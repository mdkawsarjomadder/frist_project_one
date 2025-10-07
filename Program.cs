using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// add sevices   to the controller.........>>! step_1

builder.Services.AddControllers();
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
   {
        var errors = context.ModelState
              .Where(e => e.Value != null && e.Value.Errors.Count() > 0)
              .Select(e => new
              {
                  Field = e.Key,
                  Errors = e.Value != null ? e.Value.Errors.Select(x => x
                     .ErrorMessage).ToArray() : new string[0]
              }).ToList();
           
       return new BadRequestObjectResult(new
       {
           Message = "Validation Failed.!",
           Errors = errors
       });
   };
});

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

/*
// ---------------GET:/api/categories read Category?-----------------------!
app.MapGet("/api/categories", () => { });
*/

/*
// ---------------POST:/api/categories Create Category?-----------------------!
app.MapPost("/api/categories", ([FromBody] Category categoryData) =>
{});
*/

/*
// ---------------PUT:/api/categories/{categoryId} Update Category?-----------------------!
app.MapPut("/api/categories/{categoryId:guid}", () => { });
*/

/*
 // ---------------DELETE:/api/categories/{categoryId} DELETE Category?-----------------------!
app.MapDelete("/api/categories/{categoryId:guid}", () =>{});
*/

// add sevices   to the controller.........>>! step_2
app.MapControllers();
app.Run();


//DTO------------------!
public record class Category
{
    public Guid CategoryID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime CategoryAt { get; set; }
};


