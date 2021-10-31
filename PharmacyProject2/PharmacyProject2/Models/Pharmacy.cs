using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyProject2.Models
{
    class Pharmacy
    {
        public string Name { get; }
        public List<DrugType> _drugTypes;
        public int Id { get; }
        private static int _counter;
        public static int cashBox;
        public Pharmacy(string name)
        {
            Name = name;
            _counter++;
            Id = _counter;
            _drugTypes = new List<DrugType>();
        }
        public bool CreateDrugType(DrugType drugType)
        {
            DrugType findDrug = _drugTypes.Find(x => x.Name.ToLower() == drugType.Name.ToLower());
            if (findDrug != null)
            {
                return false;
            }
            _drugTypes.Add(drugType);
            return true;
        }
        public override string ToString()
        {
            return Id + "." + Name; 
        }
    }
}
