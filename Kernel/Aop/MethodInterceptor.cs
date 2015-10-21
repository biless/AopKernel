using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Kernel.MVVM;

namespace Kernel.Aop
{
    class MethodInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            invocation.Proceed();
            if (invocation.Method.Name.StartsWith("set_"))
            {
                var notifyPropertyChangedObject = invocation.InvocationTarget as NotifyPropertyChangedObject;
                notifyPropertyChangedObject?.onPropertyChanged(invocation.Method.Name.Substring(4));
            }
        }
    }
}
