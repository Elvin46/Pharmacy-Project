using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyProject2.Models
{
    partial class DrugType
    {
        public string Name { get; }
        private List<Drug> _drugs;
        public int Id { get; }
        private static int _counter;
        public DrugType(string name)
        {
            Name = name;
            _counter++;
            Id = _counter;
            _drugs = new List<Drug>();
        }
    }
}
