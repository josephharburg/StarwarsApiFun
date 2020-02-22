using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWarsAPIApp.Models
{
    public class Character
    {
        public string name { get; set; }
        public string birth_year { get; set; }
        public string homeworld { get; set; }
        public string[] species { get; set; }
        public string spec { get { return species[0]; } set { spec = species[0]; } }



         

    }
}
