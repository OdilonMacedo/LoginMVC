using Components.Consumers;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddMassTransit(cfg =>
{
    cfg.UsingRabbitMq();
    cfg.AddRequestClient<LoginConsumer>(new Uri($"exchange:{KebabCaseEndpointNameFormatter.Instance.Consumer<LoginConsumer>()}"));

});

builder.Services.AddSession(options =>
{
    options.Cookie.IsEssential = true; // É essencial para o funcionamento da sessão compartilhada
    options.Cookie.Name = "test";
});

var app = builder.Build();

app.UseSession();

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
