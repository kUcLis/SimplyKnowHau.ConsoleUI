using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyKnowHau.Data.Model
{
    public class Species
    {
        public int Id { get; set; }

        public string Specie { get; set; }

        public Species(int id, string specie)
        {
            Id = id;
            Specie = specie;
        }
    }
}
