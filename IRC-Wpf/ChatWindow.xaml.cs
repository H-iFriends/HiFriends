﻿using System;
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
    /// ChatWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ChatWindow : Window
    {
        public ChatWindow()
        {
            InitializeComponent();
        }
        private void SendButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addChatRoom_Click(object sender, RoutedEventArgs e)
        {
            AddChatRoomWindow addChatRoomWindow = new AddChatRoomWindow();
            if(addChatRoomWindow.ShowDialog() == true)
            {
                //获取填入数据
            }
        }

        private void delChatRoom_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
