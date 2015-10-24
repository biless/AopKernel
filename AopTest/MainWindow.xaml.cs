using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AopTest
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = Kernel.Aop.AopFactory.CreateClassProxy<MainWindowViewModel>();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MainWindowViewModel)?.change(textBox_Copy.Text);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            var task = F();
            task.ContinueWith(x => Console.WriteLine("1"));
            task.ContinueWith(x => Console.WriteLine("2"));
            task.ContinueWith(x => Console.WriteLine("3"));
        }

        private async Task F()
        {
            await Task.Delay(1000);
            Console.WriteLine("Finish");
        }
    }
}
