using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humane_Society
{
    class BirdCageFactory: CageFactory
    {
        public BirdCageFactory()
        { }
        override public Cage create_cage(string Name, string Status)
        {
            return new BirdCage(Name,Status);
        }
    }
}
