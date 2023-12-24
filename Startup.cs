using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Lib2.Models;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;

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

            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddHttpContextAccessor();


            // Thêm controller
            services.AddControllersWithViews();

            services.AddDbContext<LibraryContext>(options =>
            {
                options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"),
                sqlServerOptionsAction: sqlOptions => 
                {
                    sqlOptions.EnableRetryOnFailure();
                }
                );
            });

            // Tạo dữ liệu mặc định 
            var context = (LibraryContext)services.BuildServiceProvider().GetRequiredService(typeof(LibraryContext));
            if(context.Books.Count() == 0)
            {
                var books = new List<Book>()
                {
                    new Book("Tri tue do thai", "Eran Katz", "novel" , 0, "~/CODE/Lib2/BookContent/tri-tue-do-thai.txt"),
                    new Book("Cay cam ngot cua toi", "Jose Mauro De Vasconcelos", "novel" , 0, "~/CODE/Lib2/BookContent/cay-cam-ngot-cua-toi.txt")
                };
            context.Books.AddRange(books);
            context.SaveChanges();
            }
            if (context.Users.Count() == 0)
            {
                var users = new List<User>()
                {
                    new User("Jane","janesmith@gmail.com", "Jane123"),
                    new User("Alice", "aliceinwonderland@gmail.com", "Alice456")
                };
                context.Users.AddRange(users);
                context.SaveChanges();
            }
            if(context.Librarians.Count() == 0)
            {
                var librarian = new Librarian("aliceinwonderland@gmail.com","Alice456");
                context.Librarians.Add(librarian);
                context.SaveChanges();
            }
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
            app.UseCookiePolicy();
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Resigter}/{action=Login}");
            });
        }
    }
}
