using System;
using Castle.DynamicProxy;
using Kernel.MVVM;

namespace Kernel.Aop.Interceptor
{
    public class NotifyInterceptor : Attribute, IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            invocation.Proceed();
            if (!invocation.Method.Name.StartsWith("set_")) return;
            var notifyPropertyChangedObject = invocation.InvocationTarget as NotifyPropertyChangedObject;
            // ReSharper disable once ExplicitCallerInfoArgument
            notifyPropertyChangedObject?.onPropertyChanged(invocation.Method.Name.Substring(4));
        }
    }
}
