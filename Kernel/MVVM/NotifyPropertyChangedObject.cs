using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Kernel.MVVM
{
    public class NotifyPropertyChangedObject : INotifyPropertyChanged
    {
        /// <summary>
        /// 属性改变函数
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 消息通知函数
        /// </summary>
        /// <param name="propertyName">属性名</param>
        public virtual void onPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
