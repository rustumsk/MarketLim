using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

var builder = WebApplication.CreateBuilder(args);
var databaseConnectionString = BuildSqliteConnectionString(
    builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=App_Data/marketlim.db",
    builder.Environment.ContentRootPath);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(databaseConnectionString));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();

static string BuildSqliteConnectionString(string connectionString, string contentRootPath)
{
    var builder = new SqliteConnectionStringBuilder(connectionString);

    if (string.IsNullOrWhiteSpace(builder.DataSource) ||
        builder.DataSource.Equals(":memory:", StringComparison.OrdinalIgnoreCase))
    {
        return builder.ConnectionString;
    }

    if (!Path.IsPathRooted(builder.DataSource))
    {
        builder.DataSource = Path.GetFullPath(Path.Combine(contentRootPath, builder.DataSource));
    }

    var databaseDirectory = Path.GetDirectoryName(builder.DataSource);
    if (!string.IsNullOrWhiteSpace(databaseDirectory))
    {
        Directory.CreateDirectory(databaseDirectory);
    }

    return builder.ConnectionString;
}
