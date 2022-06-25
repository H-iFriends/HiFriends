using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using IRC_Business.IntelligentCompletion;
using IRC;
using IRC.entity;
using IRC_Business.nlp;

namespace IRC_Wpf
{
    /// <summary>
    /// ChatWindow.xaml 聊天窗口
    /// 这个里面chatroom的定义是我自己随便定的，chatroom就是channel，后面记得改掉
    /// </summary>
    public partial class ChatWindow : Window
    {
        public Completion Completion;
        public Channel CurrentChannel;
        public Client CurrentClient;
        private UserInfo UserInfo;
        public ObservableCollection<Channel> Channels = new ObservableCollection<Channel>();
        private string LastSend = "";
        public string Content = "";

        public ChatWindow(Channel channel, UserInfo userInfo)
        {
            this.Channels.Add(channel);
            this.CurrentChannel = channel;
            // this.CurrentClient = new Client(CurrentChannel.hostName, CurrentChannel.hostPort);
            this.CurrentClient = this.CurrentChannel.Client;
            this.UserInfo = userInfo;
            InitializeComponent();
            initWindow();
        }

        //按钮点击事件
        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string toSend = msg_input.Text;
            if (!string.IsNullOrEmpty(toSend))
            {
                //空指针需处理
                if (!this.CurrentClient.Privmsg(CurrentChannel.ChannelInfo.name, toSend)) //频道名称
                {
                    throw new Exception("Message sent failed.");
                }
            }
        }

        private void addChatRoom_Click(object sender, RoutedEventArgs e)
        {
            AddChatRoomWindow addChatRoomWindow = new AddChatRoomWindow(this.Channels, this.UserInfo);
            if (addChatRoomWindow.ShowDialog() == true)
            {
                //这目前看来不需要填什么
            }
        }
        //删除当前已加入聊天室
        private void delChatRoom_Click(object sender, RoutedEventArgs e)
        {
            Channel? channel = JoinedListDataBinding.SelectedItem as Channel;
            if (channel != null)
            {
                this.Channels.Remove(channel);
            }
            else throw new Exception("selected item is not a Channel.");
        }
        //+1按钮
        private void plusOne_Click(object sender, RoutedEventArgs e)
        {
            this.CurrentClient.Privmsg(this.CurrentChannel.ChannelInfo.name, LastSend);
        }
        //一键水群按钮
        private void quickSend_Click(object sender, RoutedEventArgs e)
        {
            string toSend = msg_input.Text;
            foreach (var channel in Channels)
            {
                channel.Client.Privmsg(channel.ChannelInfo.name, toSend);
            }
        }
        //已加入列表双击函数
        private void JoinedDataBinding_DoubleClick(object sender, RoutedEventArgs e)
        {

        }
        //热门聊天室列表表项点击事件
        private void HotListDataBinding_SelectionChanged(object sender, RoutedEventArgs e)
        {
            ChatRoom? chatRoomSelected = HotListDataBinding.SelectedItem as ChatRoom;
            if (chatRoomSelected != null && chatRoomSelected is ChatRoom)
            {
                MessageBox.Show("选中的聊天室:" + chatRoomSelected.Name);
            }
        }

        //文本框内容发生变化时，自动补全功能生效
        private void msg_input_TextChanged(object sender, TextChangedEventArgs e)
        {
            string textInputed = this.msg_input.Text;
            //然后调用函数
            this.Completion = new Completion(textInputed);
            //展示回传list
            this.autoCompletionDataBinding.DataContext = Completion.CompleteLastWord();
        }

        //列表双击选中后，自动补全
        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //获得选中表项的string
            string? selectedWord = this.autoCompletionDataBinding.SelectedItem as string;
            if (selectedWord != null)
            {
                this.msg_input.Text = Completion.GetNewSentence(selectedWord);
            }
        }

        //初始化窗口（传入数据）
        private void initWindow()
        {
            //事件订阅
            this.CurrentClient.EventMotdReceived += (sender, args) =>
            {
                var c = sender as Client;
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    this.CurrentChannel.ChatHistory +=
                        ($"\n\nReceived MOTD: from server {c.ServerAddress}\n" + args.motd);
                    this.ChatHistory.Text = this.CurrentChannel.ChatHistory;
                }));
            };

            this.CurrentClient.EventMessageReceived += (sender, args) =>
            {
                var c = sender as Client;
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    this.CurrentChannel.ChatHistory +=
                        "\n" + args.sender + ": " + args.message + "\n";
                    this.ChatHistory.Text = this.CurrentChannel.ChatHistory;
                    Content += args.message + "\n";
                    LastSend = args.message;

                    //更改主题词
                    // var topics = NLP.GetKeywords(Content);
                    // if (topics.Length > 0)
                    // {
                    //     this.CurrentChannel.Topic = topics[0];
                    // }
                    // this.CurrentChannel.Topic = "Test";
                }));
            };


            //热点聊天室列表数据绑定，也是给我一个list然后设置itemssource
            // List<ChatRoom> hotChatRooms = new List<ChatRoom>();
            // hotChatRooms.Add(new ChatRoom() { Activity = 20 , Topic = "足球",Name="体育聊天室" });
            // hotChatRooms.Add(new ChatRoom() { Activity = 33, Topic = "软件构造",Name="课程聊天室" });
            // HotListDataBinding.ItemsSource = hotChatRooms;
            this.CurrentChannel.Topic = "Test";
            HotListDataBinding.ItemsSource = Channels;
            //已加入聊天室列表数据绑定，也是给我一个list然后设置itemssource
            //List<ChatRoom> joinedChatRooms = new List<ChatRoom>();
            //joinedChatRooms.Add(new ChatRoom() { Name = "王者荣耀" });
            //joinedChatRooms.Add(new ChatRoom() { Name = "英雄联盟" });
            JoinedListDataBinding.ItemsSource = this.Channels;

            //传入历史聊天记录
            //ChatHistory.Text = 你传入的string;
        }
    }

    //用来测试数据绑定，后面删掉传入正常数据list就行，写在里面的都是必须存在的东西
    public class ChatRoom
    {
        public string Topic { get; set; }
        public int Activity { get; set; }
        public string Name { get; set; }

        //展示列表表项时需要用到tostring方法
        public override string ToString()
        {
            return this.Name;
        }
    }
}