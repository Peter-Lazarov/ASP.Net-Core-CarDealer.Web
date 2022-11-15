namespace CarDealer.Web
{
    using Data;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Services;
    using Services.Implementation;
    using Infrastructure.Extensions;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<CarDealerDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<CarDealerDbContext>();
            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //services.AddTransient<ICustomerService, CustomerService>();
            //services.AddTransient<ICarService, CarService>();
            //services.AddTransient<ISupplierService, SupplierService>();
            //services.AddTransient<ISaleService, SaleService>();
            services.AddDomainServices();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "cars by parts",
                    template: "cars/parts",
                    defaults: new { controller = "Cars", action = "Parts" });

                routes.MapRoute(
                    name: "cars by make",
                    template: "cars/{make}",
                    defaults: new { controller = "Cars", action = "ByMake"});

                routes.MapRoute(
                    name: "customer with id",
                    template: "customers/{id}",
                    defaults: new { controller = "Customers", action = "TotalSales" });

                routes.MapRoute(
                    name: "customers",
                    template: "customers/all/{order}",
                    defaults: new { controller = "Customers", action = "All"});

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

//11:26
//42
//1:09
//excersize 2
//40

//excersize 3
//54
//1:12
//1:33
//1:40


