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
    public class Product:Repository
    {
        public Product():base()
        {
            database = "production";
        }
        
        private string database;
        /// <summary>
        /// Create
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task<int> create(Models.Product product)
        {            
            var sql = $@"insert into {database}.products (product_name,
                brand_id,
                category_id,
                model_year,
                list_price 
            ) values (@productName, 
                @brandId,
                @categoryId,
                @modelYear,
                @listPrice); SELECT LAST_INSERT_ID();";
            using (var connection = new MySqlConnection(connString))
            {
                var id = await connection.ExecuteScalarAsync(sql, product);
                return int.Parse(id.ToString());
            }
        }
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task<int> update(Models.Product product)
        {            
            var sql = $@"update {database}.products set product_name = @productName ,
                brand_id = @brandId ,
                category_id = @categoryId ,
                model_year = @modelYear ,
                list_price = @listPrice  where id = @id;";
            using (var connection = new MySqlConnection(connString))
            {
                return await connection.ExecuteAsync(sql, product);                
            }
        }
        /// <summary>
        /// Find One
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<Models.Product> findOne(string key, object value)
        {
            var parameters = new { value = value };
            var sql = $"select * from {database}.products where {key} = @value LIMIT 1;";
            using (var connection = new MySqlConnection(connString))
            {
                return await connection.QueryFirstOrDefaultAsync<Models.Product>(sql, parameters);                
            }
        }
        /// <summary>
        /// Find all
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<IList<Models.Product>> findAll(string key, object value)
        {
            return await findAll(new List<KeyValuePair<string, object>>() { new KeyValuePair<string, object>(key, value) });
        }
        public async Task<IList<Models.Product>> findAll(IList<KeyValuePair<string, object>> keyValues)
        {
            dynamic exo = new System.Dynamic.ExpandoObject();
            string where = string.Empty;
            foreach (var keyValue in keyValues)
            {
                where = string.IsNullOrEmpty(where) ? string.Empty : " and ";
                
                where += $"{keyValue.Key} = @{keyValue.Key}";
                ((IDictionary<String, Object>)exo).Add(keyValue.Key, keyValue.Value);
            }
            where = string.IsNullOrEmpty(where) ? string.Empty : $" where {where}";
            var sql = $"select * from {database}.products{where}";
            using (var connection = new MySqlConnection(connString))
            {
                var model = await connection.QueryAsync<Models.Product>(sql, (object)exo);
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
            var sql = $"delete from {database}.products where {key} = @value;";
            using (var connection = new MySqlConnection(connString))
            {
                return await connection.ExecuteAsync(sql, parameters);                
            }
        }

        public async Task<IList<Models.Product>> select()
        {
           var sql = $@"select 
                p.`id`,
                p.`product_name`,
                p.`brand_id`,
                p.`category_id`,
                p.`model_year`,
                p.`list_price`,
                c.id,
                c.category_name 
                from {database}.products p
                inner
                join {database}.categories c on p.category_id = c.id;";
            using (var connection = new MySqlConnection(connString))
            {
                var model = await connection.QueryAsync<Models.Product, Models.Category, Models.Product>(sql, (product, category) => {
                    product.category = category;
                    return product;
                });
                return model.ToList();
            }
        }
        public async Task<Array> anonymous()
        {
            var sql = $@"select 
                p.`id`,
                p.`product_name`,
                p.`brand_id`,
                p.`category_id`,
                p.`model_year`,
                p.`list_price`,                
                c.category_name 
                from {database}.products p
                inner
                join {database}.categories c on p.category_id = c.id;";
            using (var connection = new MySqlConnection(connString))
            {
                var model = await connection.QueryAsync(sql);
                return model.ToArray();
            }
        }
    }
}
