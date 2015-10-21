using Kernel.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AopTest
{
    public class MainWindowViewModel : NotifyPropertyChangedObject
    {
        public virtual string Name { get; set; }

        public virtual void changName(string name)
        {
            Name = name;
        }
    }
}
