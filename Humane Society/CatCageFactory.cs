using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humane_Society
{
    class CatCageFactory: CageFactory
    {
        public CatCageFactory()
        { }
        override public Cage create_cage(string Name, string Status)
        {
            return new CatCage(Name, Status);
        }
    }
}
