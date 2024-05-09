using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Reflection;
using ZalexInc.Certification.API.Errors;
using ZalexInc.Certification.API.MiddleWares;
using ZalexInc.Certification.API.Resolvers;
using ZalexInc.Certification.Application.Commands;
using ZalexInc.Certification.Application.Handlers;
using ZalexInc.Certification.Application.Profiles;
using ZalexInc.Certification.Application.Validators;
using ZalexInc.Certification.Domain.Interfaces;
using ZalexInc.Certification.Infrastructure.Data;
using ZalexInc.Certification.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ContractResolver = new CustomContractResolver();
    });

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Logging.ClearProviders(); // Clear existing logging providers
builder.Logging.AddConsole(); // Add console logging provider
builder.Services.AddScoped<ICertificateRepository, CeritifcateRepository>();
builder.Services.AddAutoMapper(typeof(MappingProfiles));
builder.Services.AddTransient<IValidator<CreateCertificateCommand>, CreateCertificateCommandValidator>();
builder.Services.AddTransient<IValidator<UpdateCertificateCommand>, UpdateCertificateCommandValidator>();

builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblies(typeof(CertificateHandler).Assembly));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<APIKeyMiddleware>();
app.UseStatusCodePagesWithReExecute("/error/{0}");
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();
