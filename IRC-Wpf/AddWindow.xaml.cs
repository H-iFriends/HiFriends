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
    /// AddWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AddWindow : Window
    {
        public AddWindow()
        {
            InitializeComponent();
        }
        //跳转
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            string serverName = ((TextBox) FindName("HostName")).Text;
            int serverPort = Int32.Parse(((TextBox) FindName("Port")).Text);

            ServerUtilities serverUtilities = new ServerUtilities();
            serverUtilities.AddServer(serverName, serverPort);
            serverUtilities.ExportServer();
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
