global using LicentaApp.Interfaces;
global using LicentaApp.Models;
global using LicentaApp.Services;
global using MongoDB.Bson;
global using MongoDB.Bson.Serialization.Attributes;
global using MongoDB.Driver;
global using MongoDbGenericRepository.Attributes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.ConfigureMongoDb(builder.Configuration);

builder.Services.AddScoped<IAlbumService, AlbumService>();
builder.Services.AddScoped<IArtistService, ArtistService>();
builder.Services.AddScoped<IFeedbackService, FeedbackService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<ISongService, SongService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IGenreService, GenreService>();

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

//TODO: configure lost routes to return not found
app.MapControllerRoute(
    "Error",
    "/{*url}",
     new { controller = "Home", action = "Error" }
);
app.Run();

//TODO : Feedback form
//TODO : Review form
//TODO : Log in && Register form
//TODO : Log in mechanism
//TODO : CLEANUP CODE everywhere at the end
//TODO : NEXT PAGE BUTTONS
//TODO : PADDING ON FOOTER ???????????????
//TODO : SIZE = 0 
//TODO : validation on hrefs

