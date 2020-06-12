using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace EmployeeStore.Models
{
    public static class AppDataSeed
    {
        public static void SeedData(IApplicationBuilder app)
        {
            ApplicationDbContext context = app.ApplicationServices.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();
            if (!context.AppInfos.Any())
            {
                context.AppInfos.Add(
                    new AppInfo
                    {
                        AppName = "Application Name",
                        AppAddress = "Custom address 142/52",
                        AppPhone1 = "8 (700) 100 10 10",
                        AppPhone2 = "8 (702) 200 20 20",
                        AppEmail = "Application@email.com",
                        AppDescription = "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium," +
                        "totam rem aperiam," + 
                        "eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo.Nemo enim ipsam voluptatem"+
                        "quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt."+
                        "Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit,"+
                        "sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem.Ut enim ad minima veniam,"+
                        "quis nostrum exercitationem ullam corporis suscipit laboriosam,"+
                        "nisi ut aliquid ex ea commodi consequatur ? Quis autem vel eum iure reprehenderit qui in ea voluptate velit esse quam nihil " +
                        "molestiae consequatur,vel illum qui dolorem eum fugiat quo voluptas nulla pariatur"
                    });
                context.SaveChanges();
            }
            if (!context.AppSocialAddresses.Any())
            {
                context.AppSocialAddresses.AddRange(
                    new AppSocialAddress
                    {
                        UrlAddress = "https://twitter.com/explore"
                    },
                    new AppSocialAddress
                    {
                        UrlAddress = "https://www.facebook.com/"
                    });
                context.SaveChanges();
            }
        }
    }
}
