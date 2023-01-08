//Minimal API -- Giriş ve Swagger Kullanımı

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<CustomerDb>(options => options.UseInMemoryDatabase("CustomerDb"));
builder.Services.AddSwaggerGen();
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minimal API V1");
});

app.MapGet("/",()=>"Hello World");
app.Run();



class Customer
{
	public int Id { get; set; }
	public string Name { get; set; }

}

class CustomerDb : DbContext
{
	public CustomerDb(DbContextOptions options) : base(options)
	{
		
	}

	public DbSet<Customer> Customers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseInMemoryDatabase("CustomerDb");
    }
}