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
        static void Main(string[] args)
        {
            var events = new List<Event>{
                            new Event{ Name = "LadyGaga", City = "Chicago", Price=200},
                            new Event{ Name = "LadyGaga", City = "New York", Price=250},
                            new Event{ Name = "U2", City = "New York", Price=150},
                            new Event{ Name = "Dua Lipa", City = "New York", Price=175},
                            new Event{ Name = "U2", City = "Washington", Price = 300},
                            new Event{ Name = "Rolling Stones", City = "Los Angeles", Price = 200},
                            new Event{ Name = "Rolling Stones", City = "Pittsburgh", Price = 175}
                            };

            var customer = new List<Customer> { new Customer { Name = "John Smith", City = "New York" } };
            var query = customer.Where(x => x.Name == "John Smith").ToList();

            Console.WriteLine("Send Email to customer with Same city sorted by Price \n");
            foreach (var cus in query)
            {
                foreach (var ev in events.OrderBy(x => x.Price).Where(x => x.City == cus.City))
                {
                    AddToEmail(cus, ev);
                }
            }

            Console.WriteLine("\n Send Email to customer with nearest 5 cities sorted by Price \n");
            foreach (var cus in query)
            {
                var notSameCityEvents = events.Where(x => x.City != cus.City);
                foreach (var ev in notSameCityEvents)
                {
                    ev.Distance = GetDistance(cus.City, ev.City);
                }
                foreach (var ev in notSameCityEvents.Where(x => x.Distance != -1).OrderBy(x => x.Price).Take(5))
                {
                    AddToEmail(cus, ev);
                }
            }
        }
        static void AddToEmail(Customer c, Event e)
        {
            Console.Out.WriteLine($"Customer: {c.Name}, Event: {e.Name}" + (e.Distance > 0 ? $", ({e.Distance} miles away)" : "") + ($" for ${e.Price}"));
        }

        static double GetDistance(string fromCity, string toCity)
        {
            double distance = -1;
            try
            {
                switch (toCity)
                {
                    case "Chicago":
                        distance = 711.11;
                        break;
                    case "Washington":
                        distance = 203.46;
                        break;
                    case "Los Angeles":
                        distance = 2445.47;
                        break;
                    case "Pittsburgh":
                        distance = 314.72;
                        break;
                    default:
                        distance = 0;
                        break;
                }
            }
            catch (Exception ex)
            {
                return distance;
            }
            return distance;
        }
    }
}


