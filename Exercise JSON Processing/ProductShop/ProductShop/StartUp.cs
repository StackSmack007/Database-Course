using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ProductShop.Data;
using ProductShop.DTOS;
using ProductShop.DTOS.UsersAndProducts;
using ProductShop.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            Mapper.Initialize(cfg => cfg.AddProfile<ProductShopProfile>());

            var context = new ProductShopContext();
            //  context.Database.EnsureDeleted();
            //  context.Database.EnsureCreated();
            #region task 1
            //  string file_path1 = @"..\..\..\Datasets\users.json";
            //  string jsonContent1 = File.ReadAllText(file_path1);
            //  Console.WriteLine(ImportUsers(context, jsonContent1));
            #endregion

            #region task 2
            //  string file_path2 = @"..\..\..\Datasets\products.json";
            //  string jsonContent2 = File.ReadAllText(file_path2);
            //  Console.WriteLine(ImportProducts(context, jsonContent2));
            #endregion

            #region task 3
            //  string file_path3 = @"..\..\..\Datasets\categories.json";
            //  string jsonContent3 = File.ReadAllText(file_path3);
            //  Console.WriteLine(ImportCategories(context, jsonContent3));
            #endregion


            #region task 4
            //  string file_path4 = @"..\..\..\Datasets\categories-products.json";
            //  string jsonContent4 = File.ReadAllText(file_path4);
            //  Console.WriteLine(ImportCategoryProducts(context, jsonContent4));
            #endregion


            #region task 5
            // Console.WriteLine(GetProductsInRange(context));
            #endregion

            #region task 6
            //  Console.WriteLine(GetSoldProducts(context));
            #endregion

            #region task 7
            // Console.WriteLine(GetCategoriesByProductsCount(context));
            #endregion

            #region task 8
            Console.WriteLine(GetUsersWithProducts(context));

            #endregion



        }



        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            userDTOin[] users = JsonConvert.DeserializeObject<userDTOin[]>(inputJson);
            User[] realUsers = users.AsQueryable().ProjectTo<User>().ToArray();
            context.Users.AddRange(realUsers);
            int usersAdded = context.SaveChanges();
            return $"Successfully imported { usersAdded}";
        }

        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            var productsDTO = JsonConvert.DeserializeObject<productDTOin[]>(inputJson);
            var products = JsonConvert.DeserializeObject<Product[]>(inputJson);
            context.Products.AddRange(products);
            int rowsChanged = context.SaveChanges();
            return $"Successfully imported {rowsChanged}";
        }

        public static string ImportCategories(ProductShopContext context, string inputJson)
        {
            var categoriesDTO = JsonConvert.DeserializeObject<categoryDTOin[]>(inputJson).Where(x => x.Name != null).ToArray();
            var categories = categoriesDTO.AsQueryable().ProjectTo<Category>().ToArray();
            context.Categories.AddRange(categories);
            int rowsChanged = context.SaveChanges();
            return $"Successfully imported {rowsChanged}";
        }

        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {
            var validCategoryIds = new HashSet<int>(context.Categories.Select(x=>x.Id).ToArray());
            var validProductIds = new HashSet<int>(context.Products.Select(x=>x.Id).ToArray());

            var categoryProducts = JsonConvert.DeserializeObject<CategoryProduct[]>(inputJson)
                .Where(x=>validCategoryIds.Contains(x.CategoryId)&&validProductIds.Contains(x.ProductId))
                .ToArray();

            context.CategoryProducts.AddRange(categoryProducts);
            int rowsChanged = context.SaveChanges();
            return $"Successfully imported {rowsChanged}";

        }

        public static string GetProductsInRange(ProductShopContext context)
        {
            var products = context.Products.Where(x => x.Price >= 500 && x.Price <= 1000)
                                            .Select(x => new ProductDTOut()
                                            { Name = x.Name, Price = x.Price, Seller = x.Seller.FirstName +" " +x.Seller.LastName })
                                            .OrderBy(x => x.Price)
                                            .ToArray();

           var ContractResolver = new DefaultContractResolver()
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };

            var jsonSettings = new JsonSerializerSettings()
            {
                ContractResolver = ContractResolver,
                Formatting = Formatting.Indented
            };



            string jsonString = JsonConvert.SerializeObject(products, jsonSettings);
            return jsonString;

        }

        public static string GetSoldProducts(ProductShopContext context)
        {
            userDTOut[] usersWithSoldProducts = context.Users.Where(u => u.ProductsSold.Any(p => p.Buyer != null))
                .OrderBy(x => x.LastName).ThenBy(x => x.FirstName).ProjectTo<userDTOut>().ToArray();

            string resultJson = JsonConvert.SerializeObject(usersWithSoldProducts,Formatting.Indented);

            return resultJson;
        }

        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {


           var resultSet = context.Categories.ProjectTo<categoryDTOut>()
               .OrderByDescending(x => x.productsCount).ToArray();
          
           string resultJson = JsonConvert.SerializeObject(resultSet,Formatting.Indented);

            return resultJson;
        }

        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var usersWithBuyer = context.Users
                                              .Include(u=>u.ProductsSold)
                                              .ThenInclude(p => p.Buyer)
                                              .Where(u => u.ProductsSold.Any(p => p.Buyer != null))
                                              .ToList();

            foreach (User user in usersWithBuyer)
            {
                user.ProductsSold = user.ProductsSold.Where(p => p.Buyer != null).ToList();
            }
            usersWithBuyer = usersWithBuyer.OrderByDescending(x => x.ProductsSold.Count).ToList();
            usersAndProductsDTO resultDTObject = Mapper.Map<usersAndProductsDTO>(usersWithBuyer);
          
            var jsonSettings = new JsonSerializerSettings()
            {
                NullValueHandling=NullValueHandling.Ignore,
                Formatting = Formatting.Indented
            };


            string resultJSon = JsonConvert.SerializeObject(resultDTObject, jsonSettings);

            return resultJSon;
        }


    }
}