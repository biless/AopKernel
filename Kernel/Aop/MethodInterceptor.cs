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
        private IInvocation invocation;

        public void Intercept(IInvocation invocation)
        {
            this.invocation = invocation;

            var ahainOfResponsibility = new AhainOfResponsibility {exectueNext = x => invocation.Proceed()};

            ahainOfResponsibility = new AhainOfResponsibility(ahainOfResponsibility) {exectueNext = asepct};

            ahainOfResponsibility.exectue();

            if (invocation.Method.Name.StartsWith("set_"))
            {
                var notifyPropertyChangedObject = invocation.InvocationTarget as NotifyPropertyChangedObject;
                // ReSharper disable once ExplicitCallerInfoArgument
                notifyPropertyChangedObject?.onPropertyChanged(invocation.Method.Name.Substring(4));
            }
        }

        private void tryCatchAsepct(AhainOfResponsibility ahainOfResponsibility)
        {

        }

        private void argumentJudgementAsepct(AhainOfResponsibility ahainOfResponsibility)
        {

        }

        private void asepct(AhainOfResponsibility ahainOfResponsibility)
        {
            var att = invocation.Method.GetCustomAttributes(typeof(AsepctAttribute), true).Cast<AsepctAttribute>().ToList();
            attributeBeforeAsepct(att);

            ahainOfResponsibility.exectue();

            var task = invocation.ReturnValue as Task;
            if (task == null)
                attributeAfterAsepct(att);
            else
                attributeAfterAsepctTask(task, att);
        }


        private void attributeBeforeAsepct(List<AsepctAttribute> asepctAttributes)
        {
            asepctAttributes.ForEach(attribute =>
            {
                if (attribute.methodAsepctAroundEnum == MethodAsepctAroundEnum.After) return;
                attribute.beforeAsepct();
            });
        }

        private void attributeAfterAsepctTask(Task task, List<AsepctAttribute> asepctAttributes)
        {
            task.ContinueWith(x => attributeAfterAsepct(asepctAttributes));
        }

        private void attributeAfterAsepct(List<AsepctAttribute> asepctAttributes)
        {
            asepctAttributes.Reverse();
            asepctAttributes.ForEach(attribute =>
            {
                if (attribute.methodAsepctAroundEnum == MethodAsepctAroundEnum.Before) return;
                attribute.afterAsepct();
            });
        }
    }
}
