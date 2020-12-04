using System;
using System.Collections.Generic;
using System.Text;

namespace CRUDquery.Classes
{
    /// <summary>
    ///     Клас для роботи з користувачами
    /// </summary>
    class User
    {
        public int id { get; set; }
        public string name { get; set; }
        public string telNumer { get; set; }

        public int cityId { get; set; }

        public override string ToString()
        {
            return this.id + ": Name: " + this.name + " Numer: " + this.telNumer + " CityId: " + this.cityId;
        }
    }
}
