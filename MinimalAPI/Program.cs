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
app.MapGet
    ("/customers",
    async (CustomerDb db) => await db.Customers.ToListAsync()
    );
app.MapGet("/customers/{id}", async (CustomerDb db, int id) => await db.Customers.FindAsync(id));
app.MapPost("/customers", async (CustomerDb db,Customer cus) =>
{
    await db.Customers.AddAsync(cus);
	await db.SaveChangesAsync();
	return Results.Created($"/customers/{cus.Id}",cus);
}
);
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