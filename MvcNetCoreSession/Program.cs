var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//DEBEMOS HABILITAR EL SERVICIO DE MEMORIA CAHCE PORQUE COMPARTEN CARACTERISTICAS
builder.Services.AddDistributedMemoryCache();
//CONFIGURAR SESSION CON UN TIEMPO DE INACTIVIDAD
builder.Services.AddSession(options =>
{
    options.IdleTimeout=TimeSpan.FromMinutes(10);
});


var app = builder.Build();

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

//HABILITAMOS SESSION PARA EL SERVIDOR
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
