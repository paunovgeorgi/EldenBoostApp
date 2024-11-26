using EldenBoost.Extensions;
using EldenBoost.Hubs;
using EldenBoost.ModelBinders;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationDatabase(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddApplicationIdentity();

builder.Services.AddControllersWithViews(options =>
{
    options.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider());
    options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
});

builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseStatusCodePagesWithRedirects("Home/Error?statusCode={0}");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

await app.CreateAdminRoleAsync();


app.MapControllerRoute(
     name: "areas",
           pattern: "/{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
        name: "SecurePattern",
        pattern: "/{controller}/{action}/{id}/{information}",
        defaults: new { Controller = "Service", Action = "Details" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.MapHub<ChatHub>("/chatHub");

app.Run();
