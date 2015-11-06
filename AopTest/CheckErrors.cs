using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kernel.Aop;

namespace AopTest
{
    class CheckErrors : IfElseAsepctAttribute
    {
        public override bool check(params object[] args)
        {
            return true;
        }

        public override void checkForErrors()
        {
            Console.WriteLine("hello");
        }
    }
}
