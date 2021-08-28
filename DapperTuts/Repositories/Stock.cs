using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperTuts.Repositories
{
    public class Stock:Repository
    {
        public Stock():base()
        {
            database = "production";
        }
        
        private string database;
        /// <summary>
        /// Create
        /// </summary>
        /// <param name="stock"></param>
        /// <returns></returns>
        public async Task<int> create(Models.Stock stock)
        {            
            var sql = $"insert into {database}.stocks (store_id, product_id, quantity) values (@storeId, @productId, @quantity); SELECT LAST_INSERT_ID();";
            using (var connection = new MySqlConnection(connString))
            {
                var id = await connection.ExecuteScalarAsync(sql, stock);
                return int.Parse(id.ToString());
            }
        }
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="stock"></param>
        /// <returns></returns>
        public async Task<int> update(Models.Stock stock)
        {            
            var sql = $"update {database}.stocks set store_id = @storeId, product_id = @productId, quantity = @quantity, updated_at = @updatedAt where id = @id;";
            using (var connection = new MySqlConnection(connString))
            {
                return await connection.ExecuteAsync(sql, stock);                
            }
        }
        /// <summary>
        /// Find One
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<Models.Stock> findOne(string key, object value)
        {
            var parameters = new { value = value };
            var sql = $"select * from {database}.stocks where {key} = @value LIMIT 1;";
            using (var connection = new MySqlConnection(connString))
            {
                return await connection.QueryFirstOrDefaultAsync<Models.Stock>(sql, parameters);                
            }
        }
        /// <summary>
        /// Find all
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<IList<Models.Stock>> findAll(string key, object value)
        {
            var parameters = new { value = value };
            var sql = $"select * from {database}.stocks where {key} = @value;";
            using (var connection = new MySqlConnection(connString))
            {
                var model = await connection.QueryAsync<Models.Stock>(sql, parameters);
                return model.ToList();
            }
        }
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<int> delete(string key, object value)
        {
            var parameters = new { value = value };
            var sql = $"delete from {database}.stocks where {key} = @value;";
            using (var connection = new MySqlConnection(connString))
            {
                return await connection.ExecuteAsync(sql, parameters);                
            }
        }
    }
}
