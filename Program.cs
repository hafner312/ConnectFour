using ConnectFour;
using ConnectFour.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<GameState>();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Render (und aehnliche PaaS-Anbieter) terminieren TLS am Edge und leiten
// intern per HTTP weiter - ein zusaetzlicher Redirect hier wuerde eine
// Redirect-Schlaufe verursachen.
if (Environment.GetEnvironmentVariable("RENDER") != "true")
{
    app.UseHttpsRedirection();
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
