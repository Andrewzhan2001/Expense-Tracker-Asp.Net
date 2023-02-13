using ExpenseRecorder.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<WebDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING")));
var app = builder.Build();

//Register Syncfusion license
/*Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("YOUR LICENSE KEY");*/
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBaFt/QHRqVVhkVFpHaV5HQmFJfFBmQmlZflR0fEU3HVdTRHRcQl9iSn5UdkdhWnpZdXE=;Mgo+DSMBPh8sVXJ0S0J+XE9AflRBQmJMYVF2R2BJeFRwdV9CaEwgOX1dQl9gSXxSdUVkWX1cc3FcQmg=;ORg4AjUWIQA/Gnt2VVhkQlFacldJXnxIe0x0RWFab1l6dFRMZFRBNQtUQF1hSn5Rd0ZjXn9ccndTR2NU;MTExOTA1NkAzMjMwMmUzNDJlMzBSNzVkS1JWMStUNlc0dXpaU1E0MHhwUjBtQTRYK0V3amtuOGhkaW4xTlN3PQ==;MTExOTA1N0AzMjMwMmUzNDJlMzBaSisrMlRHQzd5Y1FVcS9odGZOdUVIVUJTYnpUTFc1dGVTRmhZbFJ3cGhzPQ==;NRAiBiAaIQQuGjN/V0Z+WE9EaFtKVmBWf1ZpR2NbfE53flVHalhZVAciSV9jS31TdERgWXhecnZWQWddUw==;MTExOTA1OUAzMjMwMmUzNDJlMzBkVXdHODZHNmNEYURlS0w0THBTRzFyYXRSMHRlazlRMUlMTzF5clpmVUJVPQ==;MTExOTA2MEAzMjMwMmUzNDJlMzBleURPM3MvdW9YZFVNU1ZBNWpuTTZERVM3b2p6TDlieFVBakE1ZVFJU3VjPQ==;Mgo+DSMBMAY9C3t2VVhkQlFacldJXnxIe0x0RWFab1l6dFRMZFRBNQtUQF1hSn5Rd0ZjXn9ccnddRWNb;MTExOTA2MkAzMjMwMmUzNDJlMzBHeXRKUWVBNkl0ZGxJV2EvTzhRclpYY2YwMWtzcWJLc3J0UmJCK2toeTN3PQ==;MTExOTA2M0AzMjMwMmUzNDJlMzBpUEdiMWkzejV6WXBORDh5QnA3ZTlYOTA0ajlpd2N0RnppVjV3SkJST0hRPQ==;MTExOTA2NEAzMjMwMmUzNDJlMzBkVXdHODZHNmNEYURlS0w0THBTRzFyYXRSMHRlazlRMUlMTzF5clpmVUJVPQ==");

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
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.Run();
