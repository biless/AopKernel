using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Kernel.MVVM
{
    /// <summary>
    /// 命令基类
    /// </summary>
    public class CommandObject : ICommand
    {
        /// <summary>
        /// 封装一个函数
        /// </summary>
        public Action<object> execute { get; set; }

        /// <summary>
        /// 能否执行函数的委托
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns>能否执行</returns>
        public delegate bool DelegateCanExecute(object parameter);

        /// <summary>
        /// 能否执行函数
        /// </summary>
        public DelegateCanExecute canExecute { get; set; }

        /// <summary>
        /// 能否执行
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns>能否执行</returns>
        public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute(parameter);
        }

        /// <summary>
        /// 执行函数
        /// </summary>
        /// <param name="parameter">参数</param>
        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
                execute.Invoke(parameter);
        }

        public event EventHandler CanExecuteChanged;
    }
}
