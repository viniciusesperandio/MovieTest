using MovieApp.Services;
using MovieApp.Settings;
using MovieApp.Exceptions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.Configure<ApiSetting>(builder.Configuration.GetSection("ApiSettings"));


builder.Services.AddHttpClient<IMovieService, MovieService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"]);

    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {builder.Configuration["ApiSettings:ApiToken"]}");
});

builder.Services.AddLogging(config =>
{
    config.AddConsole();
    config.AddDebug();
});
builder.Services.AddControllers();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Movies/Index";
});

var app = builder.Build();

app.MapGet("/", () => Results.Redirect("/Movies"));

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler(options => { });
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseMiddleware<ExceptionHandler>();
app.MapControllers();
app.MapRazorPages();

app.Run();
