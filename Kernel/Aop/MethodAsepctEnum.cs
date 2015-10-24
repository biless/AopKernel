using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kernel.Aop
{
    /// <summary>
    /// 方法切面
    /// </summary>
    public enum MethodAsepctEnum
    {
        /// <summary>
        /// 两者都要
        /// </summary>
        Both,
        /// <summary>
        /// 前切面
        /// </summary>
        Before,
        /// <summary>
        /// 后切面
        /// </summary>
        After
    }
}
