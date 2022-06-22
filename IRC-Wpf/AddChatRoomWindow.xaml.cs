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
            get { return ChannelName.Text; }      
        }
    }
}
