global using LicentaApp.Interfaces.IRepository;
global using LicentaApp.Interfaces.IService;
using LicentaApp.Repositories;
using LicentaApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.ConfigureMongoDb(builder.Configuration);

builder.Services.AddScoped<IAlbumService, AlbumService>();
builder.Services.AddScoped<IAlbumRepository, AlbumRepository>();

builder.Services.AddScoped<IArtistService, ArtistService>();
builder.Services.AddScoped<IArtistRepository, ArtistRepository>();

builder.Services.AddScoped<IFeedbackService, FeedbackService>();

builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();

builder.Services.AddScoped<ISongService, SongService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//TODO: configure lost routes to return not found
app.MapControllerRoute(
    "Error",
    "/{*url}",
     new { controller = "Home", action = "Error" }
);
app.Run();

//TODO : CLEANUP CODE everywhere at the end
//TODO : PADDING ON FOOTER ???????????????
//TODO : dupa ce postez review cum fac sa il vad in pagina (trebuie dat refresh)?
//TODO : Services sunt Repo si Repo sunt services ?