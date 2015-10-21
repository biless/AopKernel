using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;

namespace Kernel.Aop
{
    public class AopFactory
    {
        static ProxyGenerator proxyGenerator = new ProxyGenerator();

        static MethodInterceptor methodInterceptor = new MethodInterceptor();

        public static T CreateClassProxy<T>(params object[] args) where T : class
        {
            return (T)proxyGenerator.CreateClassProxy(typeof(T), args, methodInterceptor);
        }
    }
}
