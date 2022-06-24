using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace IRC_Business.Server
{
    /*
     * 服务器管理
     */
    public class ServerUtilities
    {
        public ObservableCollection<Server> Servers;

        public ServerUtilities()
        {
            Servers = new ObservableCollection<Server>();
        }

        public ServerUtilities(ObservableCollection<Server> servers)
        {
            Servers = servers;
        }

        public void AddServer(string name, int port)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Server name cannot be empty or consist of only white-space.");
            }
            //此服务器已在列表中
            if (FindServer(name, port).Count > 0)
                return;
            Servers.Add(new Server(name, port));
        }

        public void RemoveServer(string s, int port)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                throw new ArgumentException("Server name cannot be empty or consist of only white-space.");
            }


            for (int i = 0; i < Servers.Count;)
            {
                if (Servers[i].ServerName.Equals(s) && Servers[i].ServerPort == port)
                {
                    Servers.Remove(Servers[i]);
                    break;
                }

                ++i;
            }
        }

        public ObservableCollection<Server> FindServer(string s, int port)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                throw new ArgumentException("Server name cannot be empty or consist of only white-space.");
            }
            ObservableCollection<Server> res = new ObservableCollection<Server>();
            foreach (Server server in Servers)
            {
                if (server.ServerName.Contains(s) && server.ServerPort == port)
                    res.Add(server);
            }
            return res;
        }

        public void ExportServer(string filePath = "servers.xml")
        {
            // if (Servers.Count == 0) return;
                
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ObservableCollection<Server>));
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                xmlSerializer.Serialize(fs, Servers);
            }
        }

        public ObservableCollection<Server> ImportServer(string filePath = "servers.xml")
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ObservableCollection<Server>));
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    return (ObservableCollection<Server>)xmlSerializer.Deserialize(fs);
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("File \"servers.xml\" not found.");
                return new ObservableCollection<Server>();
            }
        }
    }
}
