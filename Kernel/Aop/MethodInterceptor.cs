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

        private List<AsepctAttribute> asepctAttributes;

        private List<TryCatchAsepctAttribute> tryCatchAsepctAttributes;

        public void Intercept(IInvocation invocation)
        {
            this.invocation = invocation;
            asepctAttributes = invocation.Method.GetCustomAttributes(typeof(AsepctAttribute), true).Cast<AsepctAttribute>().ToList();
            tryCatchAsepctAttributes = invocation.Method.GetCustomAttributes(typeof(TryCatchAsepctAttribute), true).Cast<TryCatchAsepctAttribute>().ToList();

            var ahainOfResponsibility = new AhainOfResponsibility {exectueNext = x => invocation.Proceed()};

            //普通切面
            if(asepctAttributes.Any())
                ahainOfResponsibility = new AhainOfResponsibility(ahainOfResponsibility) {exectueNext = asepct};

            //异常切面
            if(tryCatchAsepctAttributes.Any())
                ahainOfResponsibility = new AhainOfResponsibility(ahainOfResponsibility) { exectueNext = tryCatchAsepct };

            //开始执行
            ahainOfResponsibility.exectue();

            if (invocation.Method.Name.StartsWith("set_"))
            {
                var notifyPropertyChangedObject = invocation.InvocationTarget as NotifyPropertyChangedObject;
                // ReSharper disable once ExplicitCallerInfoArgument
                notifyPropertyChangedObject?.onPropertyChanged(invocation.Method.Name.Substring(4));
            }
        }

        private async void tryCatchAsepct(AhainOfResponsibility ahainOfResponsibility)
        {
            var attributes = tryCatchAsepctAttributes;

            try
            {
                tryCatchAttributeBeforeAsepct(attributes);
                ahainOfResponsibility.exectue();
                var task = invocation.ReturnValue as Task;
                if (task != null)
                    await task;
                tryCatchAttributeAfterAsepct(attributes);
            }
            catch
            {
                catchAsepct(attributes);
            }
        }

        private void argumentJudgementAsepct(AhainOfResponsibility ahainOfResponsibility)
        {

        }

        private async void asepct(AhainOfResponsibility ahainOfResponsibility)
        {
            var attributes = asepctAttributes;

            attributeBeforeAsepct(attributes);
            ahainOfResponsibility.exectue();
            var task = invocation.ReturnValue as Task;
            if (task != null)
                await task;
            attributeAfterAsepct(attributes);
        }

        private void catchAsepct(List<TryCatchAsepctAttribute> asepctAttributes)
        {
            asepctAttributes.ForEach(attribute =>
            {
                attribute.catchAsepct();
            });
        }

        private void tryCatchAttributeBeforeAsepct(List<TryCatchAsepctAttribute> asepctAttributes)
        {
            asepctAttributes.ForEach(attribute =>
            {
                if (attribute.methodAsepctEnum == MethodAsepctEnum.After) return;
                attribute.beforeAsepct();
            });
        }

        private void tryCatchAattributeAfterAsepctTask(Task task, List<TryCatchAsepctAttribute> asepctAttributes)
        {
            task.ContinueWith(x => tryCatchAttributeAfterAsepct(asepctAttributes));
        }

        private void tryCatchAttributeAfterAsepct(List<TryCatchAsepctAttribute> asepctAttributes)
        {
            asepctAttributes.Reverse();
            asepctAttributes.ForEach(attribute =>
            {
                if (attribute.methodAsepctEnum == MethodAsepctEnum.Before) return;
                attribute.afterAsepct();
            });
        }

        private void attributeBeforeAsepct(List<AsepctAttribute> asepctAttributes)
        {
            asepctAttributes.ForEach(attribute =>
            {
                if (attribute.methodAsepctEnum == MethodAsepctEnum.After) return;
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
                if (attribute.methodAsepctEnum == MethodAsepctEnum.Before) return;
                attribute.afterAsepct();
            });
        }
    }
}
