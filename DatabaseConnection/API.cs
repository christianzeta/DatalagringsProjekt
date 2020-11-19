using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace DatabaseConnection
{
    public class API
    {
        public static List<Movie> GetSalesList(int userId)
        { 
            using var ctx = new Context();
            var salesList = ctx.Sales.Where(c => c.Customer.Id == userId).ToList();
            List<Movie> movieList = new List<Movie>();
            foreach (var sale in salesList)
            {
                movieList.Add(sale.Movie);
            }

            return movieList;
        }

        public static List<Movie> GetMovieSlice(int a, int b)
        {
            using var ctx = new Context();
            return ctx.Movies.OrderBy(m => m.Title).Skip(a).Take(b).ToList();
        }
        public static Customer GetCustomerByName(string name)
        {
            using var ctx = new Context();
            return ctx.Customers.FirstOrDefault(c => c.Name.ToLower() == name.ToLower());
        }
        public static bool RegisterSale(Customer customer, Movie movie)
        {
            using var ctx = new Context();
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
