using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Skype.Interviews.Samples.DevHub.hubs;

namespace Microsoft.Skype.Interviews.Samples.DevHub
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowCredentials();
                    policy.SetIsOriginAllowed(p => p.StartsWith("http://localhost") || p.StartsWith("https://localhost") );
                    policy.AllowAnyMethod();
                    policy.AllowAnyHeader();

                });
            });
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors();
            app.UseSignalR(routes =>
            {
                routes.MapHub<AddinsHub>("/hubs/addins");
            });

            app.UseWelcomePage();
        }
    }
}
