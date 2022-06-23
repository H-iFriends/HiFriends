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
    /// AddChatRoomWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AddChatRoomWindow : Window
    {
        public AddChatRoomWindow()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();   
        }
        private void searchChannel_Click(object sender, RoutedEventArgs e)
        {

            string searchWord = searchChannelName.Text;
            //然后执行搜索操作，返回一个list,备注里我自己新建了一个
            //List<ChatRoom> searchChatRooms = new List<ChatRoom>();
            //searchChatRooms.Add(new ChatRoom { Name = "eee" });
            //SearchListDataBinding.ItemsSource= searchChatRooms;
        }
        //选中表项操作
        private void SearchListDataBinding_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ChatRoom? chatRoomSelected = SearchListDataBinding.SelectedItem as ChatRoom;
            if (chatRoomSelected != null && chatRoomSelected is ChatRoom)
            {
                MessageBox.Show("选中的聊天室:" + chatRoomSelected.Name);
            }
        }

        public string getHosetName
        {
            get { return HostName.Text; }
        }
        public string getPort
        {
            get { return Port.Text; }
        }
        public string getChannelName
        {
            get { return searchChannelName.Text; }      
        }

    }
}
