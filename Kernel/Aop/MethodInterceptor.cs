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
            var att = invocation.Method.GetCustomAttributes(typeof(AsepctAttribute), true).Cast<AsepctAttribute>().ToList();
            attributeBeforeAsepct(att);
            invocation.Proceed();
            var task = invocation.ReturnValue as Task;

            if (task == null)
                attributeAfterAsepct(att);
            else
                attributeAfterAsepctTask(task,att);

            if (invocation.Method.Name.StartsWith("set_"))
            {
                var notifyPropertyChangedObject = invocation.InvocationTarget as NotifyPropertyChangedObject;
                notifyPropertyChangedObject?.onPropertyChanged(invocation.Method.Name.Substring(4));
            }
        }

        private void attributeBeforeAsepct(List<AsepctAttribute> asepctAttributes)
        {
            asepctAttributes.OrderBy(p=>p.index).ToList().ForEach(attribute => attribute.beforeAsepct());
        }

        private void attributeAfterAsepctTask(Task task,List<AsepctAttribute> asepctAttributes)
        {
            task.ContinueWith(x => attributeAfterAsepct(asepctAttributes));
        }

        private void attributeAfterAsepct(List<AsepctAttribute> asepctAttributes)
        {
            asepctAttributes.Reverse();
            asepctAttributes.OrderBy(p => p.index).ToList().ForEach(attribute => attribute.afterAsepct());
        }
    }
}
