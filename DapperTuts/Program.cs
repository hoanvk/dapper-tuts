using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperTuts
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
                //Repositories.Brand brandRepo = new Repositories.Brand();
                //int id = brandRepo.create(new Models.Brand() { brandName = "Blo bla" }).Result;
                //Console.WriteLine($"id = {id}");
                //Models.Brand brandModel = brandRepo.findOne("id", 2).Result;
                //Console.WriteLine($"brandModel: {JsonConvert.SerializeObject(brandModel)}");
                //brandModel.brandName = "Blizz";
                //int rowsAffected = brandRepo.update(brandModel).Result;
                //Console.WriteLine($"rowsAffected = {rowsAffected}");
                //int rowsAffected = brandRepo.delete("id", 2).Result;
                //Console.WriteLine($"rowsAffected = {rowsAffected}");
                //int id = Models.Product.query().create(new Models.Product()
                //{
                //    brandId = 1,
                //    categoryId = 1,
                //    listPrice = 510 * 1000000,
                //    modelYear = 2020,
                //    productName = "Camry 2.4"
                //}).Result;
                //Console.WriteLine($"id = {id}");
                //Console.WriteLine($"product: {JsonConvert.SerializeObject(Models.Product.query().findOne("id", id).Result)}");
                //int id = Models.Category.query().create(new Models.Category()
                //{
                //    categoryName = "Private Car"
                //}).Result;
                //Console.WriteLine($"id = {id}");
                //Models.Category category = Models.Category.query().findOne("id", id).Result;
                //Console.WriteLine($"category {JsonConvert.SerializeObject(category)}");

                //int id = Models.Store.query().create(new Models.Store()
                //{
                //    city = "Ha Noi",
                //    email = "john@doe.com",
                //    phone = "019234444",
                //    state = "North",
                //    storeName = "Tong kho",
                //    street = "To Hieu, Ha Dong",
                //    zipCode = "10000"
                //}).Result;
                //Console.WriteLine($"store: {JsonConvert.SerializeObject(Models.Store.query().findOne("id", id).Result)}");
                //int id = Models.Stock.query().create(new Models.Stock()
                //{
                //    productId = 1,
                //    storeId = 1,
                //    quantity = 100
                //}).Result;
                //Console.WriteLine($"store: {JsonConvert.SerializeObject(Models.Stock.query().findOne("id", id).Result)}");
                var products = Models.Product.query().findAll("id", 2).Result;
                foreach (var product in products)
                {
                    string json = JsonConvert.SerializeObject(product);
                    Console.WriteLine($"product: {json}");
                }
                //DefaultContractResolver contractResolver = new DefaultContractResolver
                //{
                //    NamingStrategy = new CamelCaseNamingStrategy()
                //};
                //var products = Models.Product.query().anonymous().Result;
                //foreach (var product in products)
                //{
                //    string json = JsonConvert.SerializeObject(product, new JsonSerializerSettings
                //    {
                //        ContractResolver = contractResolver,
                //        Formatting = Formatting.Indented
                //    });
                //    var model = JsonConvert.DeserializeObject<Models.Product>(json);
                //    Console.WriteLine($"product: {json}");
                //}

            }
            catch (Exception ex)
            {
                Console.WriteLine($"ex: {ex.InnerException}");
            }
            
            Console.ReadKey();
        }
    }
}
