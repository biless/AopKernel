using Kernel.Aop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AopTest
{
    class OhterAsepct : AsepctAttribute
    {
        public override void afterAsepct()
        {
            Console.WriteLine("after");
        }

        public override void beforeAsepct()
        {
            Console.WriteLine("before");
        }
    }
}
