using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Product.Domain.Entities;
using System.Data;

namespace Product.Persistence.Data
{
    public abstract class ProductDapperConnection
    {
        private readonly IConfiguration _configuration;

        protected ProductDapperConnection(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected IDbConnection CreateConnection()
        {
            string msCnnStr = _configuration.GetConnectionString("PostgreConnection");
            return new NpgsqlConnection(msCnnStr);
        }
    }
}