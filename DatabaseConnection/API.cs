using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace DatabaseConnection
{
    public class API
    {
        static Context ctx;

        static API()
        {
            ctx = new Context();
        }

        public static List<Movie> SortByRating(int a, int b)
        {
            return ctx.Movies.OrderByDescending(m => m.Rating).Skip(a).Take(b).ToList();
        }


        public static List<Movie> GetPreviousMovieList(int userId)
        {
            var salesList = ctx.Sales.Where(c => c.Customer.Id == userId).ToList();
            List<Movie> PreviousMovieList = new List<Movie>();
            foreach (var sale in salesList)
            {
                if (sale.ReturnDate < DateTime.Now)
                {
                    PreviousMovieList.Add(sale.Movie);
                }
            }
            return PreviousMovieList;
        }

        public static List<DateTime> GetReturnDates(int userId)
        {
            var salesList = ctx.Sales.Where(c => c.Customer.Id == userId).ToList();
            List<DateTime> ReturnDates = new List<DateTime>();
            foreach (var sale in salesList)
            {
                if (sale.ReturnDate > DateTime.Now)
                {
                    ReturnDates.Add(sale.ReturnDate);
                }
            }
            return ReturnDates;
        }

        public static List<Movie> GetCurrentMovieList(int userId)
        { 
            var salesList = ctx.Sales.Where(c => c.Customer.Id == userId).ToList();
            List<Movie> CurrentMovieList = new List<Movie>();
            foreach (var sale in salesList)
            {
                if (sale.ReturnDate > DateTime.Now)
                {
                    CurrentMovieList.Add(sale.Movie);
                }
            }
            return CurrentMovieList;
        }

        public static void AddUser(string username, string password)
        {
           
            //DatabaseConnection.API.AddUser(username);
            ctx.Add(new Customer { FirstName = username, Password = password});
            ctx.SaveChanges();
        }


        public static List<Movie> GetMovieSlice(int a, int b)
        {
            return ctx.Movies.OrderBy(m => m.Title).Skip(a).Take(b).ToList();
        }

        public static Customer GetCustomerByNameAndPass(string name, string password)
        {
            return ctx.Customers.Where(c => c.FirstName.ToLower() == name.ToLower() && c.Password == password.ToLower()).FirstOrDefault();
        }

        public static Customer GetCustomerByPassword(string pass)
        {
            return ctx.Customers.FirstOrDefault(c => c.Password.ToLower() == pass.ToLower());
        }
        public static bool RegisterSale(Customer customer, Movie movie)
        {
            try
            {
                // Här säger jag åt contextet att inte oroa sig över innehållet i dessa records.
                // Om jag inte gör detta så kommer den att försöka updatera databasens Id och cracha.
                ctx.Entry(customer).State = EntityState.Unchanged;
                ctx.Entry(movie).State = EntityState.Unchanged;

                ctx.Add(new Rental() { Date = DateTime.Now, ReturnDate = DateTime.Now.AddDays(2), Customer = customer, Movie = movie });
                return ctx.SaveChanges() == 1;
            }
            catch(DbUpdateException e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                System.Diagnostics.Debug.WriteLine(e.InnerException.Message);
                return false;
            }
        }
       
       

    }
}
