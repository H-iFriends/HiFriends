using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using IRC_Business.IntelligentCompletion;

namespace IRC_Wpf
{
    /// <summary>
    /// AddChatRoomWindow.xaml 是进入聊天室以后，如果想继续添加聊天室的窗口
    /// </summary>
    public partial class AddChatRoomWindow : Window
    {
        private ObservableCollection<Channel> Channels;
        private UserInfo UserInfo;

        public AddChatRoomWindow(ObservableCollection<Channel> channels, UserInfo userInfo)
        {
            InitializeComponent();
            this.Channels = channels;
            this.UserInfo = userInfo;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            //主机名
            string hostName = HostName.Text;
            //端口名
            int port = Int32.Parse(Port.Text);
            //频道名
            string channelName = searchChannelName.Text;

            /*在下面写添加频道的代码*/
            //同一个服务器不能建立两条连接
            Client client = new Client(hostName, port);
            Channel toAdd = new Channel(client, new ChannelInfo(channelName, "topic", 0));
            
            //检查频道列表中有无重复的服务器和端口
            // bool existed = false;
            foreach (var channel in Channels)
            {
                if (channel.Client.HostName == hostName && channel.Client.Port == port)
                {
                    client = channel.Client;
                    toAdd = new Channel(client, new ChannelInfo(channelName, "topic", 0));
                    // existed = true;
                }
            }

            // if (!existed)
            // {
            //     client = new Client(hostName, port);
            //     toAdd = new Channel(client, new ChannelInfo(channelName, "topic", 0));
            // }

            if (!client.Connect())
                throw new Exception("Connection failed.");
            if (!client.Login(this.UserInfo.Nick, this.UserInfo.User, this.UserInfo.RealName, this.UserInfo.Password))
                throw new Exception("Login failed.");
            // Thread.Sleep(10000);
            if (!client.Join(channelName))
                throw new Exception("Join channel failed.");
            this.Channels.Add(toAdd);
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void searchChannel_Click(object sender, RoutedEventArgs e)
        {
            string searchWord = searchChannelName.Text;
            //然后执行搜索操作，返回一个list,备注里是我自己新建了一个，写好list之后删掉1、2行注释，把第3行等号后面加你的list的名字
            //List<ChatRoom> searchChatRooms = new List<ChatRoom>();（删）
            //searchChatRooms.Add(new ChatRoom { Name = "eee" });（删）
            // SearchListDataBinding.ItemsSource= searchChatRooms;（取消注释）
        }

        //选中表项操作
        private void SearchListDataBinding_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ChatRoom? chatRoomSelected = SearchListDataBinding.SelectedItem as ChatRoom;
            if (chatRoomSelected != null && chatRoomSelected is ChatRoom)
            {
                MessageBox.Show("选中的聊天室:" + chatRoomSelected.Name);
            }
            // channelName = chatRoomSelected.Name;
        }
    }
}