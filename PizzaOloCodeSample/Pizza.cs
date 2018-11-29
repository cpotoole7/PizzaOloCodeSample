using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace PizzaOloCodeSample
{
    /// <summary>
    /// This class defines pizza toppings as per the data retrieved from the test web service.
    /// </summary>    
    [DataContract]
    public class Pizza
    {        
        /// <summary>
        /// List of all toppings associated with this pizza object.
        /// </summary>
        [DataMember(Name = "toppings")]
        public List<string> Toppings { get; set; }

        /// <summary>
        /// This method sorts all toppings alphabetically.
        /// </summary>
        public void Sort()
        {
            this.Toppings.Sort((a, b) => a.CompareTo(b));
        }

        /// <summary>
        /// This method overides the existing ToString() base method.  The method first 
        /// sorts all existing toppings and then comma delimited string of all toppings.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            Sort();
            return string.Join(", ", this.Toppings.ToArray());
        }
    }
}
