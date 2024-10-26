using ASPGTRTraining.DataAccess;
using ASPGTRTraining.DataAccess.Repositories;
using ASPGTRTraining.DataAccess.Repositories.implement;
using ASPGTRTraining.DataAccess.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//ADD DbContext
builder.Services.AddDbContext<ASPGTRTrainingDBContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("conn"), 
        b => b.MigrationsAssembly("ASPGTRTraining.DataAccess"))
    .UseLowerCaseNamingConvention(); 
    
    // This ensures that all table/column names are lowercase in PostgreSQL
});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
    var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
