using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Kernel.MVVM;

namespace Kernel.Aop
{
    public class MethodInterceptor : Attribute, IInterceptor
    {
        private IInvocation invocation;

        private List<AsepctAttribute> asepctAttributes;

        private List<TryCatchAsepctAttribute> tryCatchAsepctAttributes;

        private List<IfElseAsepctAttribute> ifElseAsepctAttributes;

        public void Intercept(IInvocation invocation)
        {
            this.invocation = invocation;
            asepctAttributes = invocation.Method.GetCustomAttributes(typeof(AsepctAttribute), true).Cast<AsepctAttribute>().ToList();
            tryCatchAsepctAttributes = invocation.Method.GetCustomAttributes(typeof(TryCatchAsepctAttribute), true).Cast<TryCatchAsepctAttribute>().ToList();
            ifElseAsepctAttributes = invocation.Method.GetCustomAttributes(typeof(IfElseAsepctAttribute), true).Cast<IfElseAsepctAttribute>().ToList();

            var ahainOfResponsibility = new AhainOfResponsibility {exectueNext = x => invocation.Proceed()};

            //普通切面
            if(asepctAttributes.Any())
                ahainOfResponsibility = new AhainOfResponsibility(ahainOfResponsibility) {exectueNext = asepct};

            //是否执行函数切面
            if(ifElseAsepctAttributes.Any())
                ahainOfResponsibility = new AhainOfResponsibility(ahainOfResponsibility) { exectueNext = ifElseAsepct };

            //异常切面
            if (tryCatchAsepctAttributes.Any())
                ahainOfResponsibility = new AhainOfResponsibility(ahainOfResponsibility) { exectueNext = tryCatchAsepct };

            //开始执行
            ahainOfResponsibility.exectue();
        }

        private void ifElseAsepct(AhainOfResponsibility ahainOfResponsibility)
        {
            foreach (var attribute in ifElseAsepctAttributes)
            {
                if (attribute.check(invocation.Arguments)) continue;
                attribute.checkForErrors();
                return;
            }
            ahainOfResponsibility.exectue();
        }


        private async void tryCatchAsepct(AhainOfResponsibility ahainOfResponsibility)
        {
            var attributes = tryCatchAsepctAttributes;
            var invocationAsepct = invocation;
            try
            {
                tryCatchAttributeBeforeAsepct(attributes);
                ahainOfResponsibility.exectue();
                var task = invocationAsepct.ReturnValue as Task;
                if (task == null)
                {
                    tryCatchAttributeAfterAsepct(attributes);
                }
                else
                {
                    await task;
                    tryCatchAattributeAfterAsepctTask(task, attributes);
                }
            }
            catch
            {
                catchAsepct(attributes);
            }
        }

        private void argumentJudgementAsepct(AhainOfResponsibility ahainOfResponsibility)
        {

        }

        private void asepct(AhainOfResponsibility ahainOfResponsibility)
        {
            var attributes = asepctAttributes;
            var invocationAsepct = invocation;
            attributeBeforeAsepct(attributes);
            ahainOfResponsibility.exectue();
            var task = invocationAsepct.ReturnValue as Task;
            if (task == null)
                attributeAfterAsepct(attributes);
            else
                attributeAfterAsepctTask(task, attributes);
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
            task.ContinueWith(x => tryCatchAttributeAfterAsepct(asepctAttributes), TaskContinuationOptions.OnlyOnRanToCompletion);
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
            task.ContinueWith(x => attributeAfterAsepct(asepctAttributes), TaskContinuationOptions.OnlyOnRanToCompletion);
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
