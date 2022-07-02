using EstacBem.Application.Interfaces;
using EstacBem.Application.Services;
using EstacBem.Domain.Interfaces;
using EstacBem.Infra.Context;
using EstacBem.Infra.Repositories;
using EstacBem.Infra.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace EstacBem.IoC
{
    public class DependencyInjection
    {
        public static IServiceCollection RegisterApps(IServiceCollection services)
        {
            //Context
            services.AddScoped<ApplicationDbContext>();

            //Repositories
            services.AddScoped<IBolsaoRepository, BolsaoRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IEstadiaRepository, EstadiaRepository>();
            services.AddScoped<IPrecificacaoRepository, PrecificacaoRepository>();
            services.AddScoped<IStatusRepository, StatusRepository>();
            services.AddScoped<IVeiculoRepository, VeiculoRepository>();


            //Services
            services.AddScoped<IBolsaoService, BolsaoService>();
            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IEstadiaService, EstadiaService>();
            services.AddScoped<IPrecificacaoService, PrecificacaoService>();
            services.AddScoped<IStatusService, StatusService>();
            services.AddScoped<IVeiculoService, VeiculoService>();

            //UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
