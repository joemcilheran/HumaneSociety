using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humane_Society
{
    class DogCageFactory: CageFactory
    {
        public DogCageFactory()
        { }
        override public Cage create_cage(string Name, string Status)
        {
            return new DogCage(Name, Status);
        }
    }
}
