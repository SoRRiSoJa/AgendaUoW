using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FluentValidation;
using FluentValidation.AspNetCore;
using System.Globalization;
using Microsoft.OpenApi.Models;
namespace AgendaUoW
{
    using AgendaUoW.Domain.Models;
    using AgendaUoW.Domain.Repositories;
    using AgendaUoW.Domain.Services;
    using AgendaUoW.Domain.UoW;
    using AgendaUoW.Middlewares;
    using AgendaUoW.Persistence.Config;
    using AgendaUoW.Persistence.Repositories;
    using AgendaUoW.Persistence.UoW;
    using AgendaUoW.Services;
    using AgendaUoW.Validators;
    
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
            services.AddApplicationInsightsTelemetry();
            services.AddControllers().AddFluentValidation();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AgendaUoW", Version = "v1" });
            });
            services.AddScoped<DbSession>();
            AddIoCRepositories(services);
            AddIoCServices(services);
            AddIoCValidations(services);
            services.AddHttpContextAccessor();
            RegisterMapping.Register();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AgendaUoW v1"));
            }
            var supportedCultures = new[] { new CultureInfo("pt-BR") };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(culture: "pt-BR", uiCulture: "pt-BR"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseNotOkResponseMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        #region Métodos Auxiliares
        private void AddIoCRepositories(IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IContatoRepository, ContatoRepository>();
        }
        private void AddIoCServices(IServiceCollection services)
        {
            services.AddTransient<IContatoService, ContatoService>();
        }
        private void AddIoCValidations(IServiceCollection services)
        {
            services.AddTransient<IValidator<Contato>, ContatoValidator>();
        }
        #endregion
    }
}
