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
using IRC_Business.Server;

namespace IRC_Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ServerUtilities ServerUtilities = new ServerUtilities();

        public MainWindow()
        {
            InitializeComponent();

            //从文件中读取保存的server
            this.ServerUtilities.Servers = ServerUtilities.ImportServer();
            // servers = new List<Server>() {new("a", 1), new("b", 2)};
            //绑定数据到显示列表中

            this.serverList.DataContext = this.ServerUtilities.Servers;
            // this.serverList.DisplayMemberPath = "ServerName";
        }

        private void SelectButton_Click(object sender, EventArgs e)
        {
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddWindow addDialog = new AddWindow(this.ServerUtilities);
            if (addDialog.ShowDialog() == true)
            {
                //获取数据
                Console.WriteLine(addDialog.getHostName);
                Console.WriteLine(addDialog.getPort);
                try
                {
                    //重新导入一次服务器列表
                    this.ServerUtilities.Servers = ServerUtilities.ImportServer();

                    this.serverList.DataContext = null;

                    this.serverList.Items.Refresh();
                    this.serverList.DataContext = this.ServerUtilities.Servers;
                    this.serverList.Items.Refresh();
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }
            }
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            ChannelWindow channelDialog = new ChannelWindow();
            if (channelDialog.ShowDialog() == true)
            {
                //执行连接服务器操作
                /*写在这里*/
            }

            this.Close();
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            Server? toRemove = this.serverList.SelectedItem as Server;
            if (toRemove != null)
            {
                this.ServerUtilities.RemoveServer(toRemove.ServerName, toRemove.ServerPort);
            }

            //重写xml文件
            this.ServerUtilities.ExportServer();
            // this.ServerUtilities.Servers = this.ServerUtilities.ImportServer();
            // this.ServerUtilities.Servers = this.ServerUtilities.ImportServer();
            // this.serverList.Items.Refresh();
            try
            {
                this.serverList.DataContext = null;

                this.serverList.Items.Refresh();
                //重新导入一次服务器列表
                this.ServerUtilities.Servers = ServerUtilities.ImportServer();


                this.serverList.DataContext = this.ServerUtilities.Servers;
                this.serverList.Items.Refresh();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }
    }
}