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
        public static ProxyGenerator proxyGenerator = new ProxyGenerator();

        public static T CreateClassProxy<T>() where T : class
        {
            return proxyGenerator.CreateClassProxy<T>(new MethodInterceptor());
        }

        public static T CreateClassProxy<T>(params object[] args) where T : class
        {
            return (T)proxyGenerator.CreateClassProxy(typeof(T), args, new MethodInterceptor());
        }
    }
}
