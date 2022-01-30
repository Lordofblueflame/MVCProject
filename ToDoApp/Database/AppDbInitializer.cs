using Microsoft.AspNetCore.Identity;
using ToDoApp.Enums;
using ToDoApp.Models;
using ToDoApp.Service;


namespace ToDoApp.Database;

public class AppDbInitializer
{
    
    

    public static async Task SeedUsers(IApplicationBuilder applicationBuilder)
    {
        using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        {
        var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

        string appUserEmail = "example@example.com";
        var appUser = await userManager.FindByEmailAsync(appUserEmail);
        if (appUser == null)
        {
            var newAppUser = new IdentityUser()
            {
                UserName = appUserEmail,
                Email = appUserEmail,
                EmailConfirmed = true
            };
            await userManager.CreateAsync(newAppUser, "Example!23");
        }
        }
    }
    public static async Task Seed(IApplicationBuilder applicationBuilder)
         {
             using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
             {
                 var data = serviceScope.ServiceProvider.GetService<AppDbContext>();
                 var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                 string appUserEmail = "example@example.com";
                 var appUser = await userManager.FindByEmailAsync(appUserEmail);
                 
                 
                 data.Database.EnsureCreated();
                 
                 if (!data.ToDoTasks.Any())
                 {
                     data.ToDoTasks.AddRange(new List<ToDoTask>()
                     {
                         new ToDoTask()
                         {
                             Title = "Kupic mleko",
                             Category = Category.ToDo,
                             Owner = appUser
                             
                         },
                         new ToDoTask()
                         {
                             Title = "Projekt ASP",
                             Category = Category.Inprogres,
                             Owner = appUser
                         },
                         new ToDoTask()
                         {
                             Title = "Zdac sesje",
                             Category = Category.Inprogres,
                             Owner = appUser
                         },
                         new ToDoTask()
                         {
                             Title = "Kupic kredki",
                             Category = Category.Done,
                             Owner = appUser
                         },
                         
                     });
                     data.SaveChanges();
                 }
             }
         }
}