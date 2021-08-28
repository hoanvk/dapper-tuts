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
    public class Category:Repository
    {
        public Category():base()
        {
            database = "production";
        }
        
        private string database;
        /// <summary>
        /// Create
        /// </summary>
        /// <param name="Category"></param>
        /// <returns></returns>
        public async Task<int> create(Models.Category Category)
        {
            var parameters = new { categoryName = Category.categoryName };
            var sql = $"insert into {database}.categories (category_name) values (@categoryName); SELECT LAST_INSERT_ID();";
            using (var connection = new MySqlConnection(connString))
            {
                var id = await connection.ExecuteScalarAsync(sql, parameters);
                return int.Parse(id.ToString());
            }
        }
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="Category"></param>
        /// <returns></returns>
        public async Task<int> update(Models.Category Category)
        {
            var parameters = new { categoryName = Category.categoryName, id = Category.id };
            var sql = $"update {database}.categories set category_name = @categoryName where id = @id;";
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
        public async Task<Models.Category> findOne(string key, object value)
        {
            var parameters = new { value = value };
            var sql = $"select * from {database}.categories where {key} = @value LIMIT 1;";
            using (var connection = new MySqlConnection(connString))
            {
                return await connection.QueryFirstOrDefaultAsync<Models.Category>(sql, parameters);                
            }
        }
        /// <summary>
        /// Find all
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<IList<Models.Category>> findAll(string key, object value)
        {
            var parameters = new { value = value };
            var sql = $"select * from {database}.categories where {key} = @value;";
            using (var connection = new MySqlConnection(connString))
            {
                var model = await connection.QueryAsync<Models.Category>(sql, parameters);
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
            var sql = $"delete from {database}.categories where {key} = @value;";
            using (var connection = new MySqlConnection(connString))
            {
                return await connection.ExecuteAsync(sql, parameters);                
            }
        }

    }
}
