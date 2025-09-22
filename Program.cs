using Blazor_CRM_Test_Proj.Client.Pages;
using Blazor_CRM_Test_Proj.Components;
using Blazor_CRM_Test_Proj.Data;
using Blazor_CRM_Test_Proj.Data.Repositories;
using Blazor_CRM_Test_Proj.Models;
using Blazor_CRM_Test_Proj.Services;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.Json.Serialization;

namespace Blazor_CRM_Test_Proj;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents()
            .AddInteractiveWebAssemblyComponents();

        // Add Entity Framework
        builder.Services.AddDbContext<CrmDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        // Add Repositories
        builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
        builder.Services.AddScoped<IRepository<Contact>, Repository<Contact>>();
        builder.Services.AddScoped<IRepository<Opportunity>, Repository<Opportunity>>();

        // Add Services
        builder.Services.AddScoped<ICustomerService, CustomerService>();
        builder.Services.AddScoped<IContactService, ContactService>();
        builder.Services.AddScoped<IOpportunityService, OpportunityService>();

            // Add Controllers with JSON options to handle circular references
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                    options.JsonSerializerOptions.WriteIndented = true;
                });

        // Add JWT Authentication
        builder.Services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"] ?? "CRMApp",
                    ValidAudience = builder.Configuration["Jwt:Audience"] ?? "CRMUsers",
                    IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? "YourSecretKeyThatIsAtLeast32CharactersLong"))
                };
            });

        builder.Services.AddAuthorization();

        // Add HttpClient with base address
        builder.Services.AddHttpClient("ApiClient", client =>
        {
            client.BaseAddress = new Uri("http://localhost:5249/");
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseWebAssemblyDebugging();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();
        app.UseAntiforgery();

        // Map API controllers
        app.MapControllers();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode()
            .AddInteractiveWebAssemblyRenderMode()
            .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);

        app.Run();
    }
}
