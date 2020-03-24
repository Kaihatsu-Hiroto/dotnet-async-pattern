using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace WpfApp2
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Counter = 0;
        }

        public int Counter { get; private set; }

        private void ButtonWait_Click(object sender, RoutedEventArgs e)
        {
            NewMethod();
        }

        private void NewMethod()
        {
            // 元スレッドをロックして待つ
            Task.Run(() =>
            {
                Thread.Sleep(1500);
                Counter++;
            }).Wait();
            MyLabel.Content = string.Format("{0}回目", Counter);
        }


        private async void ButtonAwait_Click(object sender, RoutedEventArgs e)
        {
            await NewMethodAsync();
        }

        private async Task NewMethodAsync()
        {
            // 元スレッドをロックせずに待つ
            await Task.Run(() =>
            {
                Thread.Sleep(1500);
                Counter++;
            });
            MyLabel.Content = string.Format("{0}回目", Counter);
        }

        private async void ButtonTask_Click(object sender, RoutedEventArgs e)
        {
            // 元スレッドをロックせずに待つ
            MyLabel.Content = await NewMethodTask();
        }

        private Task<string> NewMethodTask()
        {
            return Task.Run<string>(() =>
            {
                Thread.Sleep(1500);
                Counter++;
                return string.Format("{0}回目", Counter);
            });
        }
    }
}
