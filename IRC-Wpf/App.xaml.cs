using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace IRC_Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //热点话题选中后将其添加到当前聊天室列表
        private void HotListViewItem_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MessageBox.Show("已添加该聊天室","HiFriends",MessageBoxButton.OK,MessageBoxImage.Information);
        }
        private void JoinedListViewItem_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MessageBox.Show("已进入该聊天室", "HiFriends", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
