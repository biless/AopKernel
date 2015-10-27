using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kernel.Aop
{
    public abstract class IfElseAsepctAttribute : Attribute
    {
        public abstract bool check(params object[] args);

        public abstract void checkForErrors();
    }
}
