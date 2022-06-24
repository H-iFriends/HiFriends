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
using System.Windows.Shapes;
using IRC_Business.Server;

namespace IRC_Wpf
{
    /// <summary>
    /// AddWindow.xaml 第一次进入时在服务器列表添加服务器
    /// </summary>
    public partial class AddWindow : Window
    {
        public ServerUtilities ServerUtilities { get; set; }
        public AddWindow(ServerUtilities serverUtilities)
        {
            InitializeComponent();
            this.ServerUtilities = serverUtilities;
        }
        //跳转
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            //这个地方不用再改了，我留了你的版本
            this.DialogResult = true;
            string serverName = getHostName;
            int serverPort = int.Parse(getPort);

            this.ServerUtilities.AddServer(serverName, serverPort);
            this.ServerUtilities.ExportServer();
        }
        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //数据
        public string getHostName
        {
            get { return HostName.Text; }
        }
        public string getPort
        {
            get { return Port.Text; }
        }
    }
}
