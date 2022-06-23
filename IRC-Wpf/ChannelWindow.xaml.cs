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
    /// ChannelWindow.xaml 初始进入时添加完服务器后添加频道的界面
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
            //然后执行搜索操作，返回一个list,备注里我自己新建了一个，写好list之后删掉1、2行注释，把第3行等号后面加你的list的名字
            //List<ChatRoom> searchChatRooms = new List<ChatRoom>();（删）
            //searchChatRooms.Add(new ChatRoom { Name = "eee" });（删）
            //SearchListDataBinding.ItemsSource= searchChatRooms;（取消注释）
        }

    }
}
