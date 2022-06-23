using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml.Serialization;

namespace IRC_Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            init();
        }

        //点击事件
        private void SelectButton_Click(object sender, EventArgs e)
        {

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddWindow addDialog = new AddWindow();
            if (addDialog.ShowDialog() == true)
            {
                //获取数据
                Console.WriteLine(addDialog.getHostName);
                Console.WriteLine(addDialog.getPort);
            }
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            ChannelWindow channelDialog = new ChannelWindow();
            if (channelDialog.ShowDialog() == true)
            {
                //执行连接操作
            }
            this.Close();
        }
        //数据初始化
        private void init()
        {
            //一个list传进来，展示已添加的服务器
            //serverList.ItemsSource=你传入list的名字；
        }
        
    }
}
