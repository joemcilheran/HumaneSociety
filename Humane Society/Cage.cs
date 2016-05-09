using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humane_Society
{
    abstract class Cage
    {
        public string type;
        public string name;
        public string status;
        public Cage()
        { }
        public abstract string create_variable_string();
        
    }
}
