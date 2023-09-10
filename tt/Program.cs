using tt.Classes;
using tt.httpservices;
using tt.Services;
using tt.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder
        .WithOrigins("https://localhost:44478", "https://localhost:7201", "http://localhost:5221")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
});
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<GameService>();
builder.Services.AddSingleton<AIService>();
builder.Services.AddSingleton<OpenAIApiService>();
builder.Services.AddSingleton<OpenaiService>(); 

builder.Services.AddSignalR(hubOptions => {hubOptions.EnableDetailedErrors = true;});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting(); // Add this line

app.UseAuthorization();
//p.UseAuthentication();



    // Map the regular API endpoints
 app.MapControllerRoute(
     name: "default",
      pattern: "{controller}/{action=Index}/{id?}");

    // Map the WebSocket endpoint for the ChatHub
    app.MapHub<ChatHub>("/chathub");
app.MapHub<LobbyHub>("/lobby");

 app.MapFallbackToFile("index.html");



app.Run();

