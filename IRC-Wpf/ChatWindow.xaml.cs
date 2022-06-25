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
using IRC_Business.IntelligentCompletion;

namespace IRC_Wpf
{
    /// <summary>
    /// ChatWindow.xaml 聊天窗口
    /// 这个里面chatroom的定义是我自己随便定的，chatroom就是channel，后面记得改掉
    /// </summary>
    public partial class ChatWindow : Window
    {
        public Completion Completion;
        
        public ChatWindow()
        {
            InitializeComponent();
            initWindow();
        }
        //按钮点击事件
        private void SendButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addChatRoom_Click(object sender, RoutedEventArgs e)
        {
            AddChatRoomWindow addChatRoomWindow = new AddChatRoomWindow();
            if(addChatRoomWindow.ShowDialog() == true)
            {
                //这目前看来不需要填什么
            }
        }

        private void delChatRoom_Click(object sender, RoutedEventArgs e)
        {

        }
        private void plusOne_Click(object sender, RoutedEventArgs e)
        {

        }
        private void quickSend_Click(object sender, RoutedEventArgs e)
        {

        }

        //列表表项点击事件
        private void HotListDataBinding_SelectionChanged(object sender, RoutedEventArgs e)
        {
            ChatRoom? chatRoomSelected = HotListDataBinding.SelectedItem as ChatRoom;
            if(chatRoomSelected !=null && chatRoomSelected is ChatRoom)
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

            //热点聊天室列表数据绑定，也是给我一个list然后设置itemssource
            //List<ChatRoom> hotChatRooms = new List<ChatRoom>();
            //hotChatRooms.Add(new ChatRoom() { Activity = 20 , Topic = "足球",Name="体育聊天室" });
            //hotChatRooms.Add(new ChatRoom() { Activity = 33, Topic = "软件构造",Name="课程聊天室" });
            //HotListDataBinding.ItemsSource = hotChatRooms;

            //已加入聊天室列表数据绑定，也是给我一个list然后设置itemssource
            //List<ChatRoom> joinedChatRooms = new List<ChatRoom>();
            //joinedChatRooms.Add(new ChatRoom() { Name = "王者荣耀" });
            //joinedChatRooms.Add(new ChatRoom() { Name = "英雄联盟" });
            //JoinedListDataBinding.ItemsSource = joinedChatRooms;

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
