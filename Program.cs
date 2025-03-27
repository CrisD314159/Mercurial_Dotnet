using MercurialBackendDotnet.DB;
using MercurialBackendDotnet.Exceptions.ExceptionsFilters;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContextPool<MercurialDBContext> (o =>
    o.UseNpgsql(builder.Configuration.GetConnectionString("DBConnection"))
);

builder.Services.AddControllers(options =>{
    options.Filters.Add<EntityAlreadyExistsExceptionFilter>();
    options.Filters.Add<EntityNotFoundExceptionFilter>();
    options.Filters.Add<InvalidDataExceptionFilter>();
    options.Filters.Add<NotVerifiedExceptionFilter>();
    options.Filters.Add<UnauthorizedExceptionFilter>();
    options.Filters.Add<UnexpectedExceptionFilter>();
    options.Filters.Add<VerificationExceptionFilter>();
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
