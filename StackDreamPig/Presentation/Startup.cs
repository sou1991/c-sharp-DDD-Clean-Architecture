using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Member.Commands;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Presentation.Controllers;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Application.Member.Query;
using Application.Books.Commands;
using Application.Books.Query;

namespace Presentation
{
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
            
            services.AddTransient<ICreateMemberCommand, CreateMemberCommand>();
            services.AddTransient<ISearchMemberQuary, SearchMemberQuary>();
            services.AddTransient<IDataBaseService, DataBaseService>();
            services.AddTransient<IBooksRegistCommand, BooksRegistCommand>();
            services.AddTransient<ISearchBooksQuery, SearchBooksQuery>();
            services.AddTransient<IUpdateMemberCommnd, UpdateMemberCommnd>();

            services.AddControllersWithViews();


            services.AddDbContext<DataBaseService>(options =>
            {
                options.UseNpgsql(Configuration.GetValue<string>("ConnString"));
                //assembly => assembly.MigrationsAssembly(typeof(DataBaseService).Assembly.FullName));
            });

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(20);
                options.Cookie.HttpOnly = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Login}/{id?}");
            });
        }
    }
}
