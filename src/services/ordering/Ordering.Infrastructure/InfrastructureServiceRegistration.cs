using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Persistance;
using Ordering.Infrastructure.Mail;
using Ordering.Infrastructure.Persistence;
using Ordering.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection service,IConfiguration configuration)
        {
            service.AddDbContext<OrderContext>(op => op.UseSqlServer(configuration.GetConnectionString("OrderingConnectionString")));
            service.AddScoped(typeof(IAsyncRepository<>), typeof(RepositotyBase<>));
            service.AddTransient<IOrderRepository, OrderRepository>();
            service.AddTransient<IEmailService, EmailService>();
            return service;
        }
    }
}
