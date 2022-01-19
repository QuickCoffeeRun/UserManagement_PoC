using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using UserManagement_PoC.Context;
using UserManagement_PoC.Swagger.OperationFilters;

namespace UserManagement_PoC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options => options
                .UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IDataContext>(provider => provider.GetService<DataContext>());
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddAutoMapper(typeof(Startup));

            services.AddLogging();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "UserManagement_PoC", Version = "v1" });
                c.OperationFilter<FileUploadOperationFilter>();
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UserManagement_PoC v1"));
            }

            using (var scope = app.ApplicationServices.CreateScope())
            {
                using (var context = scope.ServiceProvider.GetService<DataContext>())
                    context.Database.Migrate();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
