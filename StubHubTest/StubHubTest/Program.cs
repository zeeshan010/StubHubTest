using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace Viagogo
{
    public class Event
    {
        public string Name { get; set; }
        public string City { get; set; }
        public decimal Price { get; set; }
        public double Distance { get; set; }
    }
    public class Customer
    {
        public string Name { get; set; }
        public string City { get; set; }
    }
    public class Solution
    {
      public static  Dictionary<string, double> dictionaryOfSameCity = new Dictionary<string, double>();
        static void Main(string[] args)
        {
            var events = new List<Event>{
                        new Event{ Name = "Phantom of the Opera", City = "New York"},
                        new Event{ Name = "Metallica", City = "Los Angeles"},
                        new Event{ Name = "Metallica", City = "New York"},
                        new Event{ Name = "Metallica", City = "Boston"},
                        new Event{ Name = "LadyGaGa", City = "New York"},
                        new Event{ Name = "LadyGaGa", City = "Boston"},
                        new Event{ Name = "LadyGaGa", City = "Chicago"},
                        new Event{ Name = "LadyGaGa", City = "San Francisco"},
                        new Event{ Name = "LadyGaGa", City = "Washington"}
                        };


            var customer = new List<Customer> { new Customer { Name = "John Smith", City = "New York" } };
            var query = customer.Where(x => x.Name == "John Smith").ToList();

            Console.WriteLine("Send Email to customer with Same city sorted by Price \n");
            foreach (var cus in query)
            {
                foreach (var ev in events.Where(x => x.City == cus.City))
                {
                    AddToEmail(cus, ev);
                }


            }

            Console.WriteLine("\n Send Email to customer with nearest 5 cities sorted by Price \n");
            foreach (var cus in query)
            {
                var custEvents = events.Select(x => new Event { Distance = GetDistance(cus.City, x.City), Name = x.Name, City = x.City }).ToList();
                foreach (var ev in custEvents.Where(x => x.Distance != -1).OrderBy(x => x.Distance).Take(5))
                {
                    AddToEmail(cus, ev);
                }
            }

            Console.WriteLine("\n Optimized for 3rd question \n");
            foreach (var cus in query)
            {
                var custEvents = events.Select(x => new Event { Distance = CheckAndGetDistance(cus.City, x.City), Name = x.Name, City = x.City }).ToList();
                foreach (var ev in custEvents.Where(x => x.Distance != -1).OrderBy(x => x.Distance).Take(5))
                {
                    AddToEmail(cus, ev);
                }
            }
        }

        static double CheckAndGetDistance(string fromCity, string toCity)
        {
            double distance = 0;
            if (dictionaryOfSameCity.ContainsKey(toCity))
            {
                distance = dictionaryOfSameCity[toCity];
            }
            else
            {
                distance = GetDistance(fromCity, toCity);
                dictionaryOfSameCity.Add(toCity, distance);
            }
            return distance;
        }
        static void AddToEmail(Customer c, Event e)
        {
            Console.Out.WriteLine($"Customer: {c.Name}, Event: {e.Name}" + (e.Distance > 0 ? $", ({e.Distance} miles away)" : "") + ($" for ${e.Price}"));
        }

        static int GetDistance(string fromCity, string toCity)
        {
            return AlphebiticalDistance(fromCity, toCity);
        }

        private static int AlphebiticalDistance(string s, string t)
        {
            var result = 0;
            var i = 0;
            for (i = 0; i < Math.Min(s.Length, t.Length); i++)
            {
                result += Math.Abs(s[i] - t[i]);
            }
            for (; i < Math.Max(s.Length, t.Length); i++)
            {
                result += s.Length > t.Length ? s[i] : t[i];
            }
            return result;
        }
    }
}


