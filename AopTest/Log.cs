using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kernel.Aop;

namespace AopTest
{
    public class Log : TryCatchAsepctAttribute
    {
        public override void beforeAsepct()
        {
            Console.WriteLine("before Log");
        }

        public override void afterAsepct()
        {
            Console.WriteLine("after Log");
        }

        public override void catchAsepct()
        {
            Console.WriteLine("catch Log");
        }
    }
}
