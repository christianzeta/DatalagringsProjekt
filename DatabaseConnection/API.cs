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

        public static List<Movie> GetSalesList(int userId)
        { 
            var salesList = ctx.Sales.Where(c => c.Customer.Id == userId).ToList();
            List<Movie> movieList = new List<Movie>();
            foreach (var sale in salesList)
            {
                movieList.Add(sale.Movie);
            }

            return movieList;
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

                ctx.Add(new Rental() { Date = DateTime.Now, Customer = customer, Movie = movie });
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
