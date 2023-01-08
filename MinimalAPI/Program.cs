//Minimal API -- Giriş ve Swagger Kullanımı

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/",()=>"Hello World");
app.Run();