using CloudComputingPT.Data;
using CloudComputingPT.DataAccess.Interfaces;
using CloudComputingPT.DataAccess.Repositories;
using Google.Cloud.Diagnostics.AspNetCore;
using Google.Cloud.SecretManager.V1;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
namespace CloudComputingPT
{
    public class Startup
    {
        private IWebHostEnvironment _host;
        string projectId;
        public Startup(IConfiguration configuration, IWebHostEnvironment host)
        {
            Configuration = configuration;
            _host = host;
            projectId = configuration.GetSection("ProjectId").Value;
            string prefixAbsolutePath = _host.ContentRootPath;
            System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS",
                prefixAbsolutePath + "/wwwroot" + "/cloudcomputing2558-ce9a289f4b14.json"
                );
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
                options.OnAppendCookie = cookieContext =>
                    CheckSameSite(cookieContext.Context, cookieContext.CookieOptions);
                options.OnDeleteCookie = cookieContext =>
                    CheckSameSite(cookieContext.Context, cookieContext.CookieOptions);
            });
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(
                    Configuration.GetConnectionString("DefaultConnection")));


            //services.AddIdentity<IdentityUser, IdentityRole>()
            //.AddEntityFrameworkStores<ApplicationDbContext>()
            //.AddDefaultUI().AddDefaultTokenProviders();
            //services.AddControllersWithViews();
            //services.AddRazorPages();

            services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
         .AddDefaultUI()
         .AddEntityFrameworkStores<ApplicationDbContext>()
         .AddDefaultTokenProviders();

            SecretManagerServiceClient client = SecretManagerServiceClient.Create();

            SecretVersionName secretVersionName = new SecretVersionName(projectId, "homeassignmentdemo", "2");

            AccessSecretVersionResponse result = client.AccessSecretVersion(secretVersionName);

            string str = result.Payload.Data.ToStringUtf8();
            dynamic myPass = JsonConvert.DeserializeObject(str);
            string clientSecret = myPass.web.client_secret;

            services.AddAuthentication().AddGoogle(options =>
            {
                options.ClientId = "98773056871-6s16041blvn1jp2qdkpoe8oal18uhslf.apps.googleusercontent.com";// "310025673018-5pip0bf5cimb6q8r71bgdaloasrqi4bn.apps.googleusercontent.com";
                options.ClientSecret = clientSecret;
            });

            services.AddScoped<IPubSubAccess, PubSubAccess>();
            services.AddScoped<ICacheAccess, CacheAccess>();
            services.AddScoped<ILogAccess, LogsAccess>();
            

            services.AddGoogleExceptionLogging(options =>
            {
                options.ProjectId = projectId;
                options.ServiceName = "CloudComputing2";
                options.Version = "0.01";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseGoogleExceptionLogging();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
        private void CheckSameSite(HttpContext httpContext, CookieOptions options)
        {
            if (options.SameSite == SameSiteMode.None)
            {
                var userAgent = httpContext.Request.Headers["User-Agent"].ToString();
                // TODO: Use your User Agent library of choice here.

                // For .NET Core < 3.1 set SameSite = (SameSiteMode)(-1)
                options.SameSite = SameSiteMode.Unspecified;

            }
        }

    }

}
