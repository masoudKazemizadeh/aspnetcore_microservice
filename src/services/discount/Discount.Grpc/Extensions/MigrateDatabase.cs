using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.Grpc.Extensions
{
    public static class MigrateDatabaseClass
    {
        public static IHost MigrateDatabase<TContext>(this IHost host, int? retry = 0)
        {
            var retryAvailablity = retry.Value;
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var configuration = services.GetRequiredService<IConfiguration>();
                var logger = services.GetRequiredService<ILogger<TContext>>();

                try
                {
                    logger.LogInformation("start initilizing database");

                    using var connection = new NpgsqlConnection(configuration.GetValue<string>("ConnectionString:postgresConnectionString"));
                    connection.Open();

                    using var command = new NpgsqlCommand { Connection = connection };
                    command.Parameters.Add("@cnt", NpgsqlTypes.NpgsqlDbType.Integer);
                    command.Parameters["@cnt"].Direction = System.Data.ParameterDirection.Output;
                    command.CommandText = "IF EXISTS (SELECT oid FROM pg_database WHERE datname = 'DiscountDb') BEGIN SET @cnt=1 END ELSE BEGIN SET @cnt=0 END";
                     command.ExecuteScalar();
                    var result = (int)command.Parameters["@cnt"].Value;
                    if (result ==1)
                        return host;
                    command.CommandText = @"CREATE TABLE Coupon(Id SERIAL PRIMARY KEY NOT NULL, ProductName varchar(24) NOT NULL, Description TEXT,Amount INT)";
                    command.ExecuteNonQuery();

                    command.CommandText = "INSERT INTO Coupon (ProductName,Description,Amount) VALUES('IPhone X','IPhone Discount',150)";
                    command.ExecuteNonQuery();
                    command.CommandText = "INSERT INTO Coupon (ProductName,Description,Amount) VALUES('Sumsung 10','Sumsung Discount',80)";
                    command.ExecuteNonQuery();

                    logger.LogInformation("database migration is finished successfully");
                }
                catch (NpgsqlException ex)
                {
                    logger.LogError(ex, "database migration has failed");

                }

                return host;


            }
        }
    }
}
