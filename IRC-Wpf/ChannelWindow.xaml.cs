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
    /// ChannelWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ChannelWindow : Window
    {
        public ChannelWindow()
        {
            InitializeComponent();
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
            ChatWindow cw = new ChatWindow();
            cw.Show();
        }
        private void searchChannel_Click(object sender, RoutedEventArgs e)
        {

            string searchWord = searchChannelName.Text;
            //然后执行搜索操作，返回一个list,备注里我自己新建了一个
            //List<ChatRoom> searchChatRooms = new List<ChatRoom>();
            //searchChatRooms.Add(new ChatRoom { Name = "eee" });
            //SearchListDataBinding.ItemsSource= searchChatRooms;
        }

        //数据
     
    }
}
