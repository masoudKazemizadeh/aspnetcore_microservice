using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistance;
using Ordering.Domain.Entities;
using Ordering.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Repositories
{
    public class OrderRepository : RepositotyBase<Order>, IOrderRepository
    {
        private readonly OrderContext _orderContext;
        private readonly ILogger<OrderRepository> _logger;

        public OrderRepository(OrderContext orderContext, ILogger<OrderRepository> logger):base(orderContext,logger)
        {
            _orderContext = orderContext;
            _logger = logger;
        }

        public async Task<IReadOnlyList<Order>> GetByUserName(string username)
        {
            return await _orderContext.Orders.Where(x => x.UserName == username).ToListAsync();
        }
    }
}
