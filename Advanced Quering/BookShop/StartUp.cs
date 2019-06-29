namespace BookShop
{
    using BookShop.Models;
    using BookShop.Models.Enums;
    using Data;
    using Initializer;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Z.EntityFramework.Plus;

    public class StartUp
    {
        public static void Main()
        {
            using (var db = new BookShopContext())
            {
                Book myBook = db.Books.First();

                PropertyInfo firstNameProperty = myBook.GetType().GetProperties().FirstOrDefault(x=>x.Name=="Title");

                Console.WriteLine(firstNameProperty.PropertyType.Name);

                #region Initialization
                //  DbInitializer.ResetDatabase(db);
                #endregion

                #region Task1
                //string ageRestriction = Console.ReadLine();
                //Console.WriteLine(GetBooksByAgeRestriction(db, ageRestriction));
                #endregion

                #region Task2
                // Console.WriteLine(GetGoldenBooks(db));
                #endregion

                #region Task3
                // Console.WriteLine(GetBooksByPrice(db));
                #endregion

                #region Task4
                // int year = int.Parse(Console.ReadLine());
                // Console.WriteLine(GetBooksNotReleasedIn(db,year));
                #endregion

                #region Task5
                // string categoriesSearched = Console.ReadLine();
                // Console.WriteLine(GetBooksByCategory(db, categoriesSearched));

                #endregion

                #region Task6

                //string date = Console.ReadLine();
                //Console.WriteLine(GetBooksReleasedBefore(db,date));

                #endregion


                #region Task7
                //string FirstNameSuffix = Console.ReadLine();
                //Console.WriteLine(GetAuthorNamesEndingIn(db,FirstNameSuffix));
                #endregion

                #region Task8
                // string titleSubstring = Console.ReadLine();
                // Console.WriteLine(GetBookTitlesContaining(db, titleSubstring));
                #endregion

                #region Task9
                // string LastNamePrefix = Console.ReadLine();
                // Console.WriteLine(GetBooksByAuthor(db, LastNamePrefix));
                #endregion

                #region Task10
                //  var requiredLength = int.Parse(Console.ReadLine());
                //  Console.WriteLine(CountBooks(db, requiredLength));
                #endregion

                #region Task11

                // Console.WriteLine(CountCopiesByAuthor(db));
                #endregion

                #region Task12

                //   Console.WriteLine(GetTotalProfitByCategory(db));
                #endregion

                #region Task13

                // Console.WriteLine(GetMostRecentBooks(db));
                #endregion

             
                #region Task14
                // IncreasePrices(db);
                #endregion

                #region Task15

            //    Console.WriteLine(RemoveBooks(db));
                #endregion
            }
        }

        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            AgeRestriction ageRestriction;
            if (!Enum.TryParse(command, true, out ageRestriction))
            {
                return "Unrecognised ageRestriction";
            }


            string[] bookTitles = context.Books.Where(b => b.AgeRestriction == ageRestriction)
                 .OrderBy(b => b.Title).Select(b => b.Title).ToArray();


            return string.Join(Environment.NewLine, bookTitles);
        }

        public static string GetGoldenBooks(BookShopContext context)
        {
            string[] bookTitles = context.Books.Where(b => b.EditionType == EditionType.Gold && b.Copies < 5000)
                             .OrderBy(b => b.BookId).Select(b => b.Title).ToArray();

            return string.Join(Environment.NewLine, bookTitles);
        }

        public static string GetBooksByPrice(BookShopContext context)
        {
            var books = context.Books.Where(b => b.Price > 40).OrderBy(b => -b.Price).Select(x => new { x.Title, x.Price }).ToArray();
            StringBuilder sb = new StringBuilder();

            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} - ${book.Price:F2}");
            }
            return sb.ToString().Trim();
        }

        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {

            string[] bookTitles = context.Books.Where(b => b.ReleaseDate.Value.Year != year)
                            .OrderBy(b => b.BookId).Select(b => b.Title).ToArray();
            return string.Join(Environment.NewLine, bookTitles);

        }

        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            string[] categoriesAllowed = input.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string[] bookTitles = context.Books
                                    .Where(x => x.BookCategories.Any(bc => categoriesAllowed.Contains(bc.Category.Name.ToLower())))
                                    .OrderBy(x => x.Title)
                                    .Select(x => x.Title)
                                    .ToArray();

            return string.Join(Environment.NewLine, bookTitles.OrderBy(x => x));
        }

        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            //  Return the title, edition type and price of all books that are released before a given date. The date will be a string in format dd-MM - yyyy.
            //  Return all of the rows in a single string, ordered by release date descending.
            DateTime endDate = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            StringBuilder sb = new StringBuilder();
            var books = context.Books
                .Where(x => x.ReleaseDate < endDate)
                .OrderByDescending(x => x.ReleaseDate)
                .Select(x => new
                {
                    x.Title,
                    x.EditionType,
                    x.Price
                }).ToArray();
            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} - {book.EditionType} - ${book.Price:F2}");
            }
            return sb.ToString().Trim();
        }

        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            string[] AuthorNames = context.Authors.Where(a => a.FirstName.EndsWith(input)).Select(x => x.FirstName + ' ' + x.LastName).OrderBy(x => x).ToArray();

            return string.Join(Environment.NewLine, AuthorNames);
        }

        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            string[] books = context.Books.Select(x => x.Title).Where(x => x.ToLower().Contains(input.ToLower())).OrderBy(x => x).ToArray();

            return string.Join(Environment.NewLine, books);

        }

        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            //Return all titles of books and their authors’ names for books, which are written by authors whose last names start with the given string.
            //Return a single string with each title on a new row.Ignore casing.Order by book id ascending.

            string[] booksTitles_AuthorNames = context.Books
                                          .OrderBy(x => x.BookId)
                                          .Include(x => x.Author)
                                          .Where(x => EF.Functions.Like(x.Author.LastName, $"{input}%"))
                                          //.Where(x => x.Author.LastName.ToLower().StartsWith(input.ToLower()))
                                          .Select(x => $"{x.Title} ({x.Author.FirstName} {x.Author.LastName})")
                                          .ToArray();

            return string.Join(Environment.NewLine, booksTitles_AuthorNames);


        }

        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            int bookCount = context.Books.Where(b => b.Title.Length > lengthCheck).Count();
            return bookCount;
        }

        public static string CountCopiesByAuthor(BookShopContext context)
        {
            //Return the total number of book copies for each author. Order the results descending by total book copies.
            //Return all results in a single string, each on a new line.
            var authors_Copies = context.Authors.Include(x => x.AuthorId).Select(x => new
            {
                FullName = x.FirstName + " " + x.LastName,
                BookCopies = x.Books.Sum(b => b.Copies)
            }).OrderBy(x => -x.BookCopies).ToArray();

            StringBuilder sb = new StringBuilder();
            authors_Copies.Select(x => sb.AppendLine($"{x.FullName} - {x.BookCopies}")).ToArray();

            return sb.ToString().Trim();
        }

        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            string[] result = context.Categories
                                     .Select(x => new
                                     {
                                         Category = x.Name,
                                         Revenue = x.CategoryBooks.Sum(i => i.Book.Copies * i.Book.Price)
                                     })
                                     .OrderByDescending(x => x.Revenue)
                                     .ThenBy(x => x.Category)
                                     .Select(x => $"{x.Category} ${x.Revenue:F2}").ToArray();

            return string.Join(Environment.NewLine, result);
        }

        public static string GetMostRecentBooks(BookShopContext context)
        {
            var categorysNewest3 = context.Categories
                                                 .Include(c => c.CategoryBooks)
                                                 .ThenInclude(bc => bc.Book)
                                                 .Select(x => new
                                                 {
                                                     Category = x.Name,
                                                     Books = x.CategoryBooks.Select(bc => bc.Book)
                                                     .OrderByDescending(b => b.ReleaseDate)
                                                         .Select(b => new
                                                         {
                                                             b.Title,
                                                             b.ReleaseDate
                                                         }).ToArray()
                                                 })
                                                 .OrderBy(x => x.Category)
                                                 .ToArray();
            StringBuilder sb = new StringBuilder();

            foreach (var category in categorysNewest3)
            {
                sb.AppendLine($"--{category.Category}");
                foreach (var book in category.Books.Take(3))
                {
                    sb.AppendLine($"{book.Title} ({book.ReleaseDate.Value.Year})");
                }
            }

            return sb.ToString().Trim();
        }

        public static void IncreasePrices(BookShopContext context)
        {
            int modifiedPricesCount = context.Books.Where(x => x.ReleaseDate.Value.Year < 2010)
                .Update(b=>new Book() { Price=b.Price+5});
        }

        public static int RemoveBooks(BookShopContext context)
        {
            var affectedRows = context.Books.Where(x => x.Copies < 4200).Delete();

            return affectedRows;

        }
    }
}