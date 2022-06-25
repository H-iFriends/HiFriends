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
using System.Windows.Shapes;
using IRC;
using IRC.entity;

namespace IRC_Wpf
{
    /// <summary>
    /// ChannelWindow.xaml 初始进入时添加完服务器后添加频道的界面
    /// </summary>
    public partial class ChannelWindow : Window
    {
        private Client Client;
        private UserInfo UserInfo;
        private string hostName;
        private int port;

        public ChannelWindow(Client client, UserInfo userInfo)
        {
            InitializeComponent();
            this.hostName = client.HostName;
            this.port = client.Port;
            // this.Client = new Client(hostName, port);
            this.Client = client;
            this.UserInfo = userInfo;
        }

        //点击事件
        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MainWindow mw = new MainWindow();
            mw.Show();
        }

        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            string channelName = searchChannelName.Text;
            
            // Thread.Sleep(10000);
            //加入频道,待改
            if (!this.Client.Join(channelName))
            {
                throw new Exception("Join channel failed.");
            }

            //加入成功
            var channel = new Channel(this.Client, new ChannelInfo(channelName, "topic", 0));
            ChatWindow cw = new ChatWindow(channel, this.UserInfo);
            cw.Show();
        }

        private void searchChannel_Click(object sender, RoutedEventArgs e)
        {
            string channelName = searchChannelName.Text;
            //找到该频道
            if (this.Client.List(channelName))
            {
            }
            else
            {
                throw new Exception("Channel not found.");
            }
            //然后执行搜索操作，返回一个list,备注里我自己新建了一个，写好list之后删掉1、2行注释，把第3行等号后面加你的list的名字
            //List<ChatRoom> searchChatRooms = new List<ChatRoom>();（删）
            //searchChatRooms.Add(new ChatRoom { Name = "eee" });（删）
            //SearchListDataBinding.ItemsSource= searchChatRooms;（取消注释）
        }
    }
}