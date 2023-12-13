using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Lib2.Models;

namespace Lib2
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {   
            // // Dịch vụ Identify
            // services.AddIdentity<User, IdentityRole>()
            //     .AddEntityFrameworkStores<LibraryContext>()
            //     .AddDefaultTokenProviders();

            // Thêm controller
            services.AddControllersWithViews();

            services.AddDbContext<LibraryContext>(options =>
            {
                options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            });

            var context = (LibraryContext)services.BuildServiceProvider().GetRequiredService(typeof(LibraryContext));
            var book = new Book(
                "Tri tue do thai", "Eran Katz", "novel" , false, "~/CODE/Lib2/BookContent/tri-tue-do-thai.txt"
            );
            context.Books.Add(book);
            context.SaveChanges();
            var user = new User(
                "Jane", "Jane123", "janesmith@gmail.com"
            );
            context.Users.Add(user);
            context.SaveChanges();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Resigter}/{action=Index}");
            });
        }
    }
}
