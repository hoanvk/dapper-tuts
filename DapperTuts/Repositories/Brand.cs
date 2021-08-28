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
    public class Brand:Repository
    {
        public Brand():base()
        {
            database = "production";
        }
        
        private string database;
        /// <summary>
        /// Create
        /// </summary>
        /// <param name="brand"></param>
        /// <returns></returns>
        public async Task<int> create(Models.Brand brand)
        {
            var parameters = new { brandName = brand.brandName };
            var sql = $"insert into {database}.brands (brand_name) values (@brandName); SELECT LAST_INSERT_ID();";
            using (var connection = new MySqlConnection(connString))
            {
                var id = await connection.ExecuteScalarAsync(sql, parameters);
                return int.Parse(id.ToString());
            }
        }
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="brand"></param>
        /// <returns></returns>
        public async Task<int> update(Models.Brand brand)
        {
            var parameters = new { brandName = brand.brandName, id = brand.id };
            var sql = $"update {database}.brands set brand_name = @brandName where id = @id;";
            using (var connection = new MySqlConnection(connString))
            {
                return await connection.ExecuteAsync(sql, parameters);                
            }
        }
        /// <summary>
        /// Find One
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<Models.Brand> findOne(string key, object value)
        {
            var parameters = new { value = value };
            var sql = $"select * from {database}.brands where {key} = @value LIMIT 1;";
            using (var connection = new MySqlConnection(connString))
            {
                return await connection.QueryFirstOrDefaultAsync<Models.Brand>(sql, parameters);                
            }
        }
        /// <summary>
        /// Find all
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<IList<Models.Brand>> findAll(string key, object value)
        {
            var parameters = new { value = value };
            var sql = $"select * from {database}.brands where {key} = @value;";
            using (var connection = new MySqlConnection(connString))
            {
                var model = await connection.QueryAsync<Models.Brand>(sql, parameters);
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
            var sql = $"delete from {database}.brands where {key} = @value;";
            using (var connection = new MySqlConnection(connString))
            {
                return await connection.ExecuteAsync(sql, parameters);                
            }
        }
    }
}
