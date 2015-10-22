using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kernel.Aop
{
    /// <summary>
    /// 责任连锁
    /// </summary>
    class AhainOfResponsibility
    {
        private readonly AhainOfResponsibility ahainOfResponsibility = null;

        public Action<AhainOfResponsibility> exectueNext;

        public AhainOfResponsibility()
        { }

        public AhainOfResponsibility(AhainOfResponsibility ahainOfResponsibility)
        {
            this.ahainOfResponsibility = ahainOfResponsibility;
        }

        public void exectue()
        {
            exectueNext?.Invoke(ahainOfResponsibility);
        }
    }
}
