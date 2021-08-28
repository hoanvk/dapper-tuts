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
    public class Store:Repository
    {
        public Store():base()
        {
            database = "sales";
        }
        
        private string database;
        /// <summary>
        /// Create
        /// </summary>
        /// <param name="store"></param>
        /// <returns></returns>
        public async Task<int> create(Models.Store store)
        {            
            var sql = $@"insert into {database}.stores (`store_name`,
                `phone`,
                `email`,
                `street`,
                `city`,
                `state`,
                `zip_code` 
            ) values (@storeName ,
                @phone ,
                @email ,
                @street ,
                @city ,
                @state ,
                @zipCode); SELECT LAST_INSERT_ID();";
            using (var connection = new MySqlConnection(connString))
            {
                var id = await connection.ExecuteScalarAsync(sql, store);
                return int.Parse(id.ToString());
            }
        }
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="store"></param>
        /// <returns></returns>
        public async Task<int> update(Models.Store store)
        {            
            var sql = $@"update {database}.stores set store_name = `store_name` = @storeName,
                `phone` = @phone,
                `email` = @email,
                `street` = @street,
                `city` = @city,
                `state` = @state,
                `zip_code` = @zipCode,  where id = @id;";
            using (var connection = new MySqlConnection(connString))
            {
                return await connection.ExecuteAsync(sql, store);                
            }
        }
        /// <summary>
        /// Find One
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<Models.Store> findOne(string key, object value)
        {
            var parameters = new { value = value };
            var sql = $"select * from {database}.stores where {key} = @value LIMIT 1;";
            using (var connection = new MySqlConnection(connString))
            {
                return await connection.QueryFirstOrDefaultAsync<Models.Store>(sql, parameters);                
            }
        }
        /// <summary>
        /// Find all
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<IList<Models.Store>> findAll(string key, object value)
        {
            var parameters = new { value = value };
            var sql = $"select * from {database}.stores where {key} = @value;";
            using (var connection = new MySqlConnection(connString))
            {
                var model = await connection.QueryAsync<Models.Store>(sql, parameters);
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
            var sql = $"delete from {database}.stores where {key} = @value;";
            using (var connection = new MySqlConnection(connString))
            {
                return await connection.ExecuteAsync(sql, parameters);                
            }
        }
    }
}
