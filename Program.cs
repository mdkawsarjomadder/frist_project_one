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


app.MapGet("/hello", () =>
{

     return Results.Content("<h1>Hello! World</h1>", "text/html"); //200
});
app.MapPost("/hello", () =>
{
    return Results.Created();//201
});
app.MapPut("/hello", () =>
{
    return Results.NoContent();  // 204
});
app.MapDelete("/hello", () =>
{
    return Results.NoContent();//204
});

app.Run();

