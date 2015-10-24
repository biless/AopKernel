﻿using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kernel.Aop
{
    public abstract class AsepctAttribute : Attribute
    {
        public MethodAsepctEnum methodAsepctEnum = MethodAsepctEnum.Both;

        public abstract void beforeAsepct();

        public abstract void afterAsepct();
    }
}
