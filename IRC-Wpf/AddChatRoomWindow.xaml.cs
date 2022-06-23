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

namespace IRC_Wpf
{
    /// <summary>
    /// AddChatRoomWindow.xaml 是进入聊天室以后，如果想继续添加聊天室的窗口
    /// </summary>
    public partial class AddChatRoomWindow : Window
    {
        private static string channelName="";
        public AddChatRoomWindow()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            //主机名
            string hostName = getHosetName;
            //端口名
            string port = getPort;
            //频道名
            string channelName;
            /*在下面写添加频道的代码*/
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
            //SearchListDataBinding.ItemsSource= searchChatRooms;（取消注释）
        }
        //选中表项操作
        private void SearchListDataBinding_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ChatRoom? chatRoomSelected = SearchListDataBinding.SelectedItem as ChatRoom;
            if (chatRoomSelected != null && chatRoomSelected is ChatRoom)
            {
                MessageBox.Show("选中的聊天室:" + chatRoomSelected.Name);
            }
            channelName = chatRoomSelected.Name;
        }

        public string getHosetName
        {
            get { return HostName.Text; }
        }
        public string getPort
        {
            get { return Port.Text; }
        }

    }
}
