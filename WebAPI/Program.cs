using DomainLayer.Data;
using DomainLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Repository_Layer.IRepository;
using Repository_Layer.Repository;
using Service_Layer.CustomServices;
using Service_Layer.ICustomServices;
using System;
using System.Globalization;
using WebAPI;
using WebAPI.HubConfig;
using WebAPI.Localization;
using WebAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
});
builder.Services.AddControllers();
//Sql Dependency Injection
var ConnectionString = builder.Configuration.GetConnectionString("MyConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(ConnectionString));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#region Service Injected
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUserChatService,UserChatService>();
//builder.Services.AddScoped<ICusto++mService<Departments>, DepartmentsService>();
//builder.Services.AddScoped<ICustomService<TextMessages>, UserChatService>();
//builder.Services.AddScoped<IUserChatRepo, UserChatRepo>();



builder.Services.AddTransient<ExceptionMiddleware>();
#endregion
# region APIVersioning
builder.Services.AddApiVersioning(opt =>
{
    opt.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.ReportApiVersions = true;
    opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                                                    new HeaderApiVersionReader("x-api-version"),
                                                    new MediaTypeApiVersionReader("x-api-version"));
});
#endregion
// Add ApiExplorer to discover versions
builder.Services.AddVersionedApiExplorer(setup =>
{
    setup.GroupNameFormat = "'v'VVV";
    setup.SubstituteApiVersionInUrl = true;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection(nameof(MailSettings)));
builder.Services.AddTransient<IMailService, MailService>();
builder.Services.AddLocalization();
builder.Services.AddSingleton<LocalizerMiddleware>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();
var app = builder.Build();

app.UseGlobalExceptionHandler();

// Migrate latest database changes during startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider
        .GetRequiredService<ApplicationDbContext>();

    // Here is the migration executed
    dbContext.Database.Migrate();
}
//var versionSet = app.NewApiVersionSet()
//                    .HasApiVersion(1.0)
//                    .HasApiVersion(2.0)
//                    .ReportApiVersions()
//                    .Build();
// Configure the HTTP request pipeline.

var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
        {
            //Show V2 first in Swagger
            foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions.Reverse())
            {
                options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());
            }
        });
}
var options = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(new CultureInfo("en-US"))
};
app.UseCors(options=>options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseRequestLocalization(options);
app.UseStaticFiles();
app.UseMiddleware<LocalizerMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();
app.MapHub<MyHub>("/toastr");
app.MapControllers();
//app.MapControllers();
app.Run();
