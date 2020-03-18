using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApplication1.Data;

using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Services;
using System.Collections.Generic;

namespace WebApplication1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<VmContext>(options =>
                options.UseSqlServer(connection));
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<VmContext>()
                .AddSignInManager<SignInManager<User>>()
                .AddUserManager<UserManager<User>>();

            services.AddScoped<IVirtualMachineProvider, VirtualMachineProvider>();

            services.Configure<VmOptions>(options =>
            {
                options.HDD = 100;
                options.RAM = 8;
                options.CoreCount = 8;
                options.NameMasks = new List<string>
                {
                    "XXXXXXXXXX",
                    "XXXXXXXX-#",
                    "XXX"
                };
                options.Pools = new List<string>{
                    "Тестовый",
                    "Общий",
                    "Выделенный"
                };
                options.Subdivisions = new List<string>{
                   "Отдел продаж" ,
                    " Отдел маркетинга",
                    " Отдел продуктов и решений"
                };
            });
            services.Configure<PasswordOptions>(options =>
            {
                options.RequiredLength = 6;
                options.RequireDigit = true;
                options.RequireUppercase = true;
            });

            services.Configure<SignInOptions>(options =>
            {
                options.RequireConfirmedAccount = false;
                options.RequireConfirmedEmail = false;
                options.RequireConfirmedPhoneNumber = false;
            });

            services.ConfigureApplicationCookie(options => options.LoginPath = "/Login");

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
