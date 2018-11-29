using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOloCodeSample
{
    /// <summary>
    /// This class defines the complete set pizza orders as defined per the data retrieved from the test web service.
    /// </summary>
    public class Orders
    {
        private List<Pizza> _Pizzas;

        public Orders(List<Pizza> items)
        {
            if (items == null)
            {
                throw new NullReferenceException("items cannot be null.");
            }

            _Pizzas = items;
        }

        /// <summary>
        /// This method orders all existing pizza orders based on the first topping listed in each pizza order.
        /// </summary>
        public void Sort()
        {
            foreach (var p in _Pizzas)
            {
                p.Sort();
            }

            _Pizzas.Sort((a, b) => a.ToString().CompareTo(b.ToString()));
        }

        /// <summary>
        /// The complete list of all existing pizza orders.
        /// </summary>
        public List<Pizza> Pizzas {
            get {
                Sort();
                return _Pizzas;
            }
        }

    }
}
