using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humane_Society
{
    abstract class CageFactory
    {
        public CageFactory()
        { }
        public abstract Cage create_cage(string Name, string Status);
    }
}
