using System;
using System.Collections.Generic;
using System.Text;

namespace CRUDquery.Classes
{
    /// <summary>
    ///     Клас для роботи з містами
    /// </summary>
    class City
    {
        public int id { get; set; }
        public string name { get; set; }

        public override string ToString()
        {
            return this.id  + ": " + this.name;
        }
    }
}
