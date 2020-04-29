using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using App.Application.Events;
using App.Application.Lookup.Queries;
using App.Application.Service;
using App.Persistence.Context;
using App.Persistence.Service;
using Clean.Application.System.Queries;
using Clean.Common;
using Clean.Common.Service;
using Clean.Persistence.Context;
using Clean.Persistence.Identity;
using Clean.Persistence.Identity.Policies;
using Clean.Persistence.Services;
using Clean.UI.Types;
using Clean.UI.Utilities;
using FluentValidation.AspNetCore;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Clean.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public string SUPER_ADMIN_POLICY { get { return "SuperAdminPolicy"; } private set { } }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // configuring the localization service 
            services.Configure<RequestLocalizationOptions>(o =>
            {
                var dti = new DateTimeFormatInfo();
                dti.Calendar = new System.Globalization.GregorianCalendar();
                var da = new CultureInfo("prs");
                var ps = new CultureInfo("ps");
                var en = new CultureInfo("en");
                da.DateTimeFormat = dti;
                ps.DateTimeFormat = dti;
                var supportedCultures = new List<CultureInfo> {
                    da,ps,en
                };
                o.DefaultRequestCulture = new RequestCulture(da);
                o.SupportedCultures = supportedCultures;
                o.SupportedUICultures = supportedCultures;
            });

            services.Configure<IISServerOptions>(options =>
            {
                options.AutomaticAuthentication = false;
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //services.AddSingleton<IHostedService, CurrentYearMarker>();

            services.AddSingleton<ScreenAccessProvider>();
            services.AddHttpContextAccessor();


            services.AddScoped<ICurrentUser, CurrentUser>();
            services.AddScoped<IProcessChangeListener, PassportChangeListener>();


            services.AddScoped<IAuthorizationHandler, NewlyRegisteredUsersHandler>();
            services.AddScoped<IAuthorizationHandler, SuperAdminOnlyHandler>();

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            // Register MediatR Supporting Assemblies
            services.AddMediatR(typeof(GetScreens).Assembly,typeof(GetOfficesQuery).Assembly);

            services.AddAntiforgery(option => option.HeaderName = "XSRF-TOKEN");

            services.AddDbContext<AppDbContext>();
            services.AddDbContext<BaseContext, AppDbContext>();
            
            services.AddDbContext<AppIdentityDbContext>();
            services.AddSession(option => { option.Cookie.IsEssential = true; });
            services.AddIdentity<AppUser, AppRole>(options => { 
                options.User.RequireUniqueEmail = true; 
                
            })
                    .AddRoles<AppRole>()
                    .AddErrorDescriber<IdentityLocalizedErrorDescribers>()
                    .AddEntityFrameworkStores<AppIdentityDbContext>()
                    .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {

                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 5;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                options.User.RequireUniqueEmail = true;
            });


            services.AddAuthorization
               (
                   options =>
                   {
                       options.AddPolicy("FreshUserPolicy", policy => { policy.Requirements.Add(new NewlyRegisteredUsers(true)); });
                       options.AddPolicy("SuperAdminPolicy", policy => { policy.Requirements.Add(new SuperAdminOnly()); });
                   }
               );


            services.AddRazorPages()
                .AddNewtonsoftJson(opts => {
                    
                })
                   //.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateProfileCommandValidator>())
                .AddRazorPagesOptions(o => { o.Conventions.Add(new CultureTemplateRouteModelConvention()); })
                .AddRazorPagesOptions(options =>
                    {
                        // Configure default routes.
                        //options.Conventions.AddPageRoute("/Retirement/Notify", "/Retirement");

                        // Authorize folders and pages.
                        options.Conventions.AuthorizeFolder("/Application");
                        options.Conventions.AuthorizeFolder("/Shared");
                        options.Conventions.AuthorizePage("/index");
                        options.Conventions.AuthorizeFolder("/Security");
                        options.Conventions.AuthorizeFolder("/Calculation");



                        //options.AllowMappingHeadRequestsToGetHandler = true;
                        options.Conventions.AllowAnonymousToPage("/Security/Register");
                    }

                );


            services.ConfigureApplicationCookie
                (
                    options =>
                    {
                        options.Cookie.IsEssential = true;
                        options.Cookie.SameSite = SameSiteMode.None;
                        options.LoginPath = "/Security/Login";
                        options.AccessDeniedPath = "/Security/AccessDenied";
                    }
                );
            // adding localization services 
            services.AddSingleton<CultureLocalizer>();
            services.AddLocalization(o =>
            {
                o.ResourcesPath = "Resources";
            });
            services
                .AddMvc(options =>
                    {

                        options.CacheProfiles.Add("SystemNoCache", new CacheProfile() { Duration = 0, NoStore = true, Location = ResponseCacheLocation.None });
                        options.Filters.Add(new ResponseCacheAttribute() { CacheProfileName = "SystemNoCache" });
                    }
                )
                .AddNewtonsoftJson(opts =>
                {
                    
                })
                .AddViewLocalization(LanguageViewLocationExpanderFormat.SubFolder)
                .AddDataAnnotationsLocalization()
                .AddModelBindingMessagesLocalizer(services);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,UserManager<AppUser> userManager,RoleManager<AppRole> role,BaseContext ctx,AppIdentityDbContext identityDbContext,ILoggerFactory loggerFactory)
        {
            Initializer.InitializeAsync(userManager, role, identityDbContext, ctx).Wait();
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseDeveloperExceptionPage();
                //app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            var dti = new DateTimeFormatInfo();
            dti.Calendar = new System.Globalization.GregorianCalendar();
            var da = new CultureInfo("prs");
            var ps = new CultureInfo("ps");
            var en = new CultureInfo("en");
            da.DateTimeFormat = dti;
            ps.DateTimeFormat = dti;
            var supportedCultures = new List<CultureInfo> {
                da,ps,en
            };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(da),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures,
            });
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCookiePolicy();
            app.UseSession();
            
            ContextHelper.Configure(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });
        }
    }
}
