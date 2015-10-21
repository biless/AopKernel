using Kernel.Aop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AopTest
{
    class TimeDifference : AsepctAttribute
    {
        DateTime ds;
        public override void afterAsepct()
        {
            Console.WriteLine(DateTime.Now.Subtract(ds).TotalMilliseconds);
        }

        public override void beforeAsepct()
        {
            ds = DateTime.Now;
            Console.WriteLine(ds);
        }
    }
}
