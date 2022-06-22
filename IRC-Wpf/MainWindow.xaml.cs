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

            //从文件中读取保存的server
            //ImportServer();

            //绑定数据到显示列表中
            //this.server_list.ItemsSource = Servers;

            
        }
        private void SelectButton_Click(object sender, EventArgs e)
        {

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddWindow addDialog = new AddWindow();
            if (addDialog.ShowDialog() == true)
            {
                Console.WriteLine(addDialog.getHostName);
                Console.WriteLine(addDialog.getPort);
            }
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            ChannelWindow channelDialog = new ChannelWindow();
            if (channelDialog.ShowDialog() == true)
            {
                Console.WriteLine(channelDialog.getChannelName);
            }
            this.Close();
        }
        
    }
}
