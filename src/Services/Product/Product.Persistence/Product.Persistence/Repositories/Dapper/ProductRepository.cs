using Product.Application.Application.Interfaces;
using Product.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Product.Persistence.Data;
using Dapper;
using System;
using System.Linq;
using System.Data;
using Product.Application.Dtos;
using AutoMapper;

namespace Product.Persistence.Repositories.Dapper
{
    public class ProductRepository : ProductDapperConnection, IProductRepository
    {
        private readonly IMapper _mapper;
        public ProductRepository(IConfiguration configuration,IMapper mapper)
          : base(configuration)
        {
            _mapper = mapper;
        }


        public async Task<List<ErpProduct>> GetAllAsync()
        {
          //  try
            {
                
                var query = "SELECT * FROM erpproducts";
                using (var connection = CreateConnection())
                {
                    return (await connection.QueryAsync<ErpProduct>(query)).ToList();
                }
            }
            //catch (Exception ex)
            {
              //  throw new Exception(ex.Message, ex);
            }
        }

        public async Task<ErpProduct> GetByIdAsync(int id)
        {
            try
            {
                var query = "SELECT * FROM erpproducts WHERE \"RecId\" = @Id";

                var parameters = new DynamicParameters();
                parameters.Add("Id", id, DbType.Int64);

                using (var connection = CreateConnection())
                {
                    return (await connection.QueryFirstOrDefaultAsync<ErpProduct>(query, parameters));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<ErpProduct> CreateAsync(ErpProduct entity)
        {
            try
            {
                var query = "INSERT INTO erpproducts (\"ProductName\",\"Barcode\",\"Price\",\"Description\") VALUES (@ProductName,@Barcode,@Price,@Description)";
              
                //var parameters = new DynamicParameters();
                //parameters.Add("ProductName", entity.ProductName, DbType.String);
                //parameters.Add("Barcode", entity.Barcode, DbType.String);
                //parameters.Add("Price", entity.Price, DbType.Decimal);
                //parameters.Add("Description", entity.Description, DbType.String);


                using (var connection = CreateConnection())
                {
                var count=  await connection.ExecuteAsync(query, entity);
              
                return entity;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<int> UpdateAsync(ErpProduct entity)
        {
            try
            {
                var query = "UPDATE Products SET ProductName = @ProductName, Barcode=@Barcode,Price = @Price, Description = @Quantity WHERE Id = @Id";


                using (var connection = CreateConnection())
                {
                    return (await connection.ExecuteAsync(query, entity));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<int> DeleteAsync(ErpProduct entity)
        {
            try
            {
                var query = "DELETE FROM erpproducts WHERE \"RecId\" = @Id";

                var parameters = new DynamicParameters();
                parameters.Add("Id", entity.RecId, DbType.Int64);

                using (var connection = CreateConnection())
                {
                    return (await connection.ExecuteAsync(query, parameters));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

      
    }
}
