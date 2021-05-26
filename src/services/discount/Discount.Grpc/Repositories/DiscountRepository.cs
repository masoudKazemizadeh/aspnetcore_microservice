using Discount.Grpc.Entity;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
namespace Discount.Grpc.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _configuration;

        private readonly NpgsqlConnection connectionstring;
        public DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionstring = new NpgsqlConnection(_configuration.GetValue<string>("ConnectionString:postgresConnectionString"));

        }
        public async Task<bool> CreateDiscountAsync(Coupon model)
        {
            await using var dbConnection = connectionstring;
            var affectdRow = await dbConnection.ExecuteAsync("INSERT INTO Coupon (ProductName,Description,Amount) VALUES(@ProductName,@Description,@Amount)"
                , new { model.ProductName, model.Description, model.Amount });
            return affectdRow == 1;

        }

        public async Task<bool> DeleteDiscountAsync(string productName)
        {
            await using var dbConnection = connectionstring;
            var affectdRow = await dbConnection.ExecuteAsync("DELETE coupon where ProductName=@ProductName"
                , new { productName });
            return affectdRow == 1;
        }

        public async Task<Coupon> GetDiscountAsync(string productName)
        {
            await using var dbConnection = connectionstring;
            var result = await dbConnection.QueryFirstOrDefaultAsync<Coupon>("SELECT * FROM Coupon WHERE ProductName=@ProductName", new { productName });
            return result != null ? result : new Coupon { ProductName = "No Discount", Description = "No Discount Available for this product", Amount = 0 };
        }


        public async Task<bool> UpdateDiscountAsync(Coupon model)
        {
            await using var dbConnection = connectionstring;
            var affectdRow = await dbConnection.ExecuteAsync("UPDATE coupon SET ProductName=@ProductName,Description=@Description,Amount=@Amount"
                , new { model.ProductName, model.Description, model.Amount });
            return affectdRow == 1;
        }
    }

}
