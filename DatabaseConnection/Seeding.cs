﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace DatabaseConnection
{
    class Seeding
    {
        static void Main() 
        {

            using (var ctx = new Context())
            {
                ctx.RemoveRange(ctx.Sales);
                ctx.RemoveRange(ctx.Movies);
                ctx.RemoveRange(ctx.Customers);

                ctx.AddRange(new List<Customer> {
                    new Customer { Username = "Björn1", FirstName = "Björn", LastName = "Karlsson", Password = "1", Mobile = "0700304050"},
                    new Customer { Username = "Robin1", FirstName = "Robin", LastName = "Nilsson", Password = "1", Mobile = "0700304050"},
                    new Customer { Username = "Kalle1", FirstName = "Kalle", LastName = "Andersson", Password = "1", Mobile = "0700304050"},
                    new Customer { Username = "Kim1", FirstName = "Kim", LastName = "Eriksson",  Password = "1", Mobile = "0700304050"},
                });


                // Här laddas data in från SeedData foldern för att fylla ut Movies tabellen
                var movies = new List<Movie>();
                var lines = File.ReadAllLines(@"..\..\..\SeedData\MovieGenre.csv");
                for (int i = 1; i < 200; i++)
                {
                    // imdbId,Imdb Link,Title,IMDB Score,Genre,Poster
                    var cells = lines[i].Split(',');
            
                    var url = cells[5].Trim('"');

                    // Hoppa över alla icke-fungerande url:er
                    try{ var test = new Uri(url); }
                    catch (Exception) { continue; }

                   
                    ctx.AddRange(new Movie { Title = cells[2], ImageURL = url, Rating = cells[3], Genre = cells[4]});
                }
               
                
                
                ctx.SaveChanges();
            }
        }
    }
}
