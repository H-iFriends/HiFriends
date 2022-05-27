using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace IRC_Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<string> Servers = new List<string>();
        public MainWindow()
        {
            InitializeComponent();

            //从文件中读取保存的server
            ImportServer();
        }
        private void SelectButton_Click(object sender, EventArgs e)
        {

        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {

        }

        private void AddServer(string s)
        {
            if (Servers.Contains(s))
                return;
            Servers.Add(s);
        }

        private void RemoveServer(string s)
        {
            if (Servers.Contains(s))
                Servers.Remove(s);
        }

        private List<string> FindServer(string s)
        {
            List<string> res = new List<string>();
            foreach (string server in Servers)
            {
                if (server.Contains(s))
                    res.Add(server);
            }
            return res;
        }

        private void ExportServer(string filePath = "servers.xml")
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<string>));
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                xmlSerializer.Serialize(fs, Servers);
            }
        }

        private List<string> ImportServer(string filePath = "servers.xml")
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<string>));
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    return (List<string>)xmlSerializer.Deserialize(fs);
                }
            }catch(FileNotFoundException e)
            {
                Console.WriteLine("File \"servers.xml\" not found.");
                return new List<string>();
            }



        }
    }
}
