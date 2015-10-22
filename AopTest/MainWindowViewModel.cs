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

        [OhterAsepct]
        [TimeDifference]
        public virtual async Task changName(string name)
        {
            await Task.Delay(3000); //延时3s
            Name = name;
        }

        public void change(string name)
        {
            changName(name);
        }
    }
}
