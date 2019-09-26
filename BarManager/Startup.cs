﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using BarManager.Models;
using BarManager.Hubs;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace BarManager
{
    public class Startup
    {
        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            _config = configuration;
            _util = new Util(logger);
        }

        public IConfiguration _config { get; }
        private Util _util;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.Lax;
            });

            services.AddDbContext<BarManagerContext>(options =>
                options.UseSqlServer(_util.getDbString(_config)));

            /*services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })*/
            services.AddIdentity<IdentityUser, IdentityRole>();
            services.AddAuthentication(options => 
                {
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
                }).AddCookie(/*options => {
                    options.LoginPath = "/Account/Login";
                    options.LogoutPath = "/Account/Logout";
                    // options.AccessDeniedPath = "/Login";
                }*/).AddGoogle(options =>
                {
                    options.ClientId = _config["LocalClientId"];
                    options.ClientSecret = _config["LocalClientSecret"];
                });

            services.AddMvc()
                 .AddRazorPagesOptions(options =>
                 {
                     options.Conventions.AuthorizeAreaPage("Identity", "/Recipes");
                     options.Conventions.AllowAnonymousToPage("/");
                 }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // TODO undo this for production
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                // app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSignalR(routes =>
            {
                routes.MapHub<IngredientHub>("/ingredientHub");
            });

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
