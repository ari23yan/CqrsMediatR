using Application.BackGroundWorker.AddReadPerson;
using Application.BackGroundWorker.DeleteReadPerson;
using Application.BaseChannel;
using Application.PersonFeatures.Command.Add.CreatePersonCommand;
using CqrsMediatR;
using FluentValidation.AspNetCore;
using Infrastructure.Persistence.Context;
using Infrastructure.Repositories.ReadRepository;
using Infrastructure.Repositories.WriteRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
                .AddFluentValidation(options =>
                {
                    // Validate child properties and root collection elements
                    options.ImplicitlyValidateChildProperties = true;
                    options.ImplicitlyValidateRootCollectionElements = true;

                    // Automatic registration of validators in assembly
                    options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
                });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddMediatR(typeof(SampleCQRSwithMediatREntrypoint).Assembly);

//builder.Services.AddScoped(typeof(IRequestHandler), typeof(RequestHandler));

builder.Services.AddMediatR(typeof(AddPersonCommandHandler).Assembly);

//builder.Services.AddHostedService<DeleteReadMovieWorker>();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Sample CQRS With MediatR.WebApi",
    });
});
builder.Services.AddScoped<WritePersonRepository>();

builder.Services.AddSingleton(typeof(ChannelQueue<>));

var mongoClient = new MongoClient("mongodb://localhost:27017");
var mongoDatabase = mongoClient.GetDatabase("CqrsMediatR");
builder.Services.AddSingleton(mongoDatabase);

builder.Services.AddScoped<ReadPersonRepository>();



builder.Services.AddHostedService<AddReadPersonWorker>();
builder.Services.AddHostedService<DeleteReadPersonWorker>();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
    option.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});
var app = builder.Build();






// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SampleCQRSwithMediatR.WebApi");
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
