namespace ProductShop
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;
    using ProductShop.Data;
    using ProductShop.Dtos.Export;
    using ProductShop.Dtos.Import;
    using ProductShop.Models;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    public class StartUp
    {
        private const string resultMessage = "Successfully imported {0}";
        public static void Main(string[] args)
        {
            Mapper.Initialize(cfg => cfg.AddProfile(new ProductShopProfile()));
            using (var context = new ProductShopContext())
            {
                //  context.Database.EnsureDeleted();
                //  context.Database.EnsureCreated();
                #region Task 1
                // string usersXml = ReadFromFile(@"../../../Datasets/users.xml");
                // System.Console.WriteLine(ImportUsers(context, usersXml));
                #endregion

                #region Task 2
                // string productsXml = ReadFromFile(@"../../../Datasets/products.xml");
                // System.Console.WriteLine(ImportProducts(context, productsXml));
                #endregion

                #region Task 3
                // string categoriesXml = ReadFromFile(@"../../../Datasets/categories.xml");
                // System.Console.WriteLine(ImportCategories(context, categoriesXml));
                #endregion

                #region Task 4
                // string categoryProductsXml = ReadFromFile(@"../../../Datasets/categories-products.xml");
                // System.Console.WriteLine(ImportCategoryProducts(context, categoryProductsXml));
                #endregion

                #region Task 5
                //  System.Console.WriteLine(GetProductsInRange(context));
                #endregion

                #region Task 6
                // System.Console.WriteLine(GetSoldProducts(context));
                #endregion

                #region Task 7
                // System.Console.WriteLine(GetCategoriesByProductsCount(context));
                #endregion

                #region Task 8
                System.Console.WriteLine(GetUsersWithProducts(context));
                #endregion
            }
        }

        private static string ReadFromFile(string path)
        {
            StringBuilder sb = new StringBuilder();
            using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                byte[] buffer = new byte[1024 * 4];
                var bytesRead = fileStream.Read(buffer, 0, buffer.Length);
                while (bytesRead > 0)
                {
                    string message_part = System.Text.Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    sb.Append(message_part);
                    bytesRead = fileStream.Read(buffer, 0, buffer.Length);
                }
            }
            return sb.ToString().Trim();
        }

        public static string ImportUsers(ProductShopContext context, string inputXml)
        {

            XmlSerializer serializer = new XmlSerializer(typeof(userImport_dto[]), new XmlRootAttribute("Users"));
            userImport_dto[] usersDtos = (userImport_dto[])serializer.Deserialize(new StringReader(inputXml));
           // User[] users = usersDtos.AsQueryable().ProjectTo<User>().ToArray();
            User[] users = usersDtos.Select(x=>Mapper.Map<User>(x)).ToArray();
            context.Users.AddRange(users);
            int addedUsersCount = context.SaveChanges();

            return string.Format(resultMessage, addedUsersCount);
        }

        public static string ImportProducts(ProductShopContext context, string inputXml)
        {
            //  int[] usersIdAllowed = context.Users.Select(x => x.Id).ToArray();

            XmlSerializer serializer = new XmlSerializer(typeof(productImport_dto[]), new XmlRootAttribute("Products"));
            productImport_dto[] productsDtos = (productImport_dto[])serializer.Deserialize(new StringReader(inputXml));
            Product[] products = productsDtos.AsQueryable()
                .ProjectTo<Product>()
                // .Where(x => usersIdAllowed.Contains(x.SellerId) && (x.BuyerId == null || usersIdAllowed.Contains(x.BuyerId.Value)))
                .ToArray();
            context.Products.AddRange(products);
            int addedProductsCount = context.SaveChanges();

            return string.Format(resultMessage, addedProductsCount);
        }

        public static string ImportCategories(ProductShopContext context, string inputXml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(categoryImport_dto[]), new XmlRootAttribute("Categories"));

            categoryImport_dto[] categoriesDTOs = (categoryImport_dto[])serializer.Deserialize(new StringReader(inputXml));

            Category[] categories = categoriesDTOs.AsQueryable()
                .ProjectTo<Category>()
                .ToArray();

            context.Categories.AddRange(categories);
            int addedCategoriesCount = context.SaveChanges();

            return string.Format(resultMessage, addedCategoriesCount);
        }

        public static string ImportCategoryProducts(ProductShopContext context, string inputXml)
        {
            int[] availableCategoryIds = context.Categories.Select(x => x.Id).ToArray();
            int[] availableProductIds = context.Products.Select(x => x.Id).ToArray();

            var serializer = new XmlSerializer(typeof(categoryProductImport_dto[]), new XmlRootAttribute("CategoryProducts"));

            categoryProductImport_dto[] catProdDTOs = (categoryProductImport_dto[])serializer.Deserialize(new StringReader(inputXml));

            CategoryProduct[] catProds = catProdDTOs
                .Where(x => availableCategoryIds.Contains(x.CategoryId) && availableProductIds.Contains(x.ProductId))
                .AsQueryable().ProjectTo<CategoryProduct>().ToArray();

            context.CategoryProducts.AddRange(catProds);

            int addedCategoryProductsCount = context.SaveChanges();

            return string.Format(resultMessage, addedCategoryProductsCount);
        }

        public static string GetProductsInRange(ProductShopContext context)
        {
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            var serializer = new XmlSerializer(typeof(productExport_dto[]), new XmlRootAttribute("Products"));

            productExport_dto[] productsFound = context.Products
                                                .Where(x => x.Price >= 500 && x.Price <= 1000)
                                                .Include(x => x.Buyer)
                                                .OrderBy(x => x.Price)
                                                .Take(10)
                                                .ProjectTo<productExport_dto>()
                                                .ToArray();

            StringBuilder sb = new StringBuilder();

            using (StringWriter stringWriter = new StringWriter(sb))
            {
                serializer.Serialize(stringWriter, productsFound, ns);
            }

            return sb.ToString().Trim();
        }

        public static string GetSoldProducts(ProductShopContext context)
        {

            var serializer = new XmlSerializer(typeof(usersWithProductSoldExport_dto[]), new XmlRootAttribute("Users"));

            var usersWhoSoldSomething = context.Users
                                               .Include(x => x.ProductsSold)
                                               .Where(x => x.ProductsSold.Any())
                                               .OrderBy(x => x.LastName)
                                               .ThenBy(x => x.FirstName)
                                               .Take(5)
                                               .Select(x => new usersWithProductSoldExport_dto()
                                               {
                                                   FirstName = x.FirstName,
                                                   LastName = x.LastName,
                                                   SoldProducts = x.ProductsSold.Select(p => new productExp1_dto()
                                                   {
                                                       Name = p.Name,
                                                       Price = p.Price
                                                   }).ToHashSet()
                                               })
                                               .ToArray();

            StringBuilder sb = new StringBuilder();

            using (StringWriter stringWriter = new StringWriter(sb))
            {
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                serializer.Serialize(stringWriter, usersWhoSoldSomething, ns);
            }
            return sb.ToString().Trim();
        }

        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var serializer = new XmlSerializer(typeof(categoryProducts_stats_dto[]), new XmlRootAttribute("Categories"));

            categoryProducts_stats_dto[] categoryStats = context.Categories
                                                                 .Include(x => x.CategoryProducts)
                                                                 .ThenInclude(x => x.Product)
                                                                 .Select(x => new categoryProducts_stats_dto()
                                                                 {
                                                                     Name = x.Name,
                                                                     Count = x.CategoryProducts.Count,
                                                                     TotalRevenue = x.CategoryProducts.Select(p => p.Product.Price).Sum(),
                                                                     AveragePrice = x.CategoryProducts.Select(p => p.Product.Price).Sum()
                                                                                 / x.CategoryProducts.Count
                                                                 })
                                                                 .OrderByDescending(x => x.Count)
                                                                 .ThenBy(x => x.TotalRevenue)
                                                                 .ToArray();

            StringBuilder sb = new StringBuilder();

            using (StringWriter stringWriter = new StringWriter(sb))
            {
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                serializer.Serialize(stringWriter, categoryStats, ns);
            }
            return sb.ToString().Trim();
        }

        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var serializer = new XmlSerializer(typeof(userSoldProductsExportDTO), new XmlRootAttribute("Users"));

            var usersWithSoldProduct = context.Users.Include(x => x.ProductsSold)
                 .Where(x => x.ProductsSold.Any())
                 .OrderByDescending(x => x.ProductsSold.Count)
                 .Select(x => new
                 {
                     x.FirstName,
                     x.LastName,
                     x.Age,
                     Products = new
                     {
                         CountOfSolldProducts = x.ProductsSold.Count,
                         Products = x.ProductsSold
                         .OrderByDescending(p=>p.Price)
                         .Select(p => new
                         {
                             p.Name,
                             p.Price
                         }).ToArray()
                     }
                 }).ToArray();

            userSoldProductsExportDTO result = new userSoldProductsExportDTO()
            {
                Count = usersWithSoldProduct.Count(),
                Users = usersWithSoldProduct.Take(10).Select(x => new userInfoAndSells_dto()
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Age = x.Age,
                    SoldProducts = new productsSolld_dto()
                    {
                        ProductsCount = x.Products.CountOfSolldProducts,
                        Products = x.Products.Products.Select(p => new productInfoExport_dto()
                        {
                            Name = p.Name,
                            Price = p.Price
                        }).ToList()
                    }
                }).ToList()
            };

            StringBuilder sb = new StringBuilder();

            using (StringWriter stringWriter = new StringWriter(sb))
            {
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                serializer.Serialize(stringWriter, result, ns);
            }
            string answer=sb.ToString().Trim();
            return answer;
        }

    }
}