using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOloCodeSample
{
    class Program
    {
        const string oloaddress = "http://files.olo.com/pizzas.json";

        static void Main(string[] args)
        {
            //retrieve test data
            Orders data = getData();

            if (data == null)
            {
                Console.WriteLine("Failed to load data from web service.");
            }

            if (data.Pizzas.Count == 0)
            {
                Console.WriteLine("Zero records retreived from web service.");
            }

            //First we select each distinct order from the test data.
            var combos =
                from p in data.Pizzas.Distinct()
                select new { toppingslist = string.Join(", ", p.Toppings.OrderBy(a => a)) };

            //Next we group our test data based each order's topping list and 
            //count the number of instance for each grouping.
            var combos2 =
                from p in combos
                group p by p.toppingslist into groupedtopings
                select new { toppings = groupedtopings.Key, numoforders = groupedtopings.Count() };

            int c = 1;

            //Iterate through our toppings list, taking only the top 20 items (in descending order), 
            //displaying each topping combination's ranking, value and count..
            foreach (var val in combos2.OrderByDescending(g => g.numoforders).Take(20))
            {
                Console.WriteLine(string.Format("{0})  Toppings: {1}, Count: {2}.", c, val.toppings, val.numoforders));
                c += 1;
            }

            Console.WriteLine("Please press any key to exit...");
            Console.ReadKey();
        }

        /// <summary>
        /// This method retrieves the test data from the web service.
        /// </summary>
        /// <returns>Orders<see cref="Orders"/></returns>
        static Orders getData()
        {         
            var webRequest = WebRequest.Create(oloaddress) as HttpWebRequest;
                      
            webRequest.ContentType = "application/json";
            webRequest.UserAgent = "Nothing";

            // I have omitted code here to handle network related errors for the sake of brevity.  
            using (var s = webRequest.GetResponse().GetResponseStream())
            {
                using (var sr = new StreamReader(s))
                {
                    var contributorsAsJson = sr.ReadToEnd();
                    List<Pizza> contributors = JsonConvert.DeserializeObject<List<Pizza>>(contributorsAsJson);
                    return new Orders(contributors);
                }
            }     
        }
    }
}

