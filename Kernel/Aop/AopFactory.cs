using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Kernel.MVVM;

namespace Kernel.Aop
{
    public class AopFactory
    {
        static readonly ProxyGenerator proxyGenerator = new ProxyGenerator();

        static readonly Interceptor.Aop Aop = new Interceptor.Aop();

        public static T CreateClassProxy<T>(params object[] args) where T : class
        {
            var xx = typeof (T).GetCustomAttributes(typeof(IInterceptor),true).Cast<IInterceptor>().ToArray();
            return (T)proxyGenerator.CreateClassProxy(typeof(T), args, xx);
        }
    }
}
