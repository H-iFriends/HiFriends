using System;
using System.Collections.Generic;
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
        public List<Server> Servers { get; }

        public ServerUtilities()
        {
            Servers = new List<Server>();
        }

        public ServerUtilities(List<Server> servers)
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
            if (FindServer(name).Count > 0)
                return;
            Servers.Add(new Server(name, port));
        }

        public void RemoveServer(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                throw new ArgumentException("Server name cannot be empty or consist of only white-space.");
            }


            for (int i = 0; i < Servers.Count;)
            {
                if (Servers[i].ServerName.Equals(s))
                {
                    Servers.Remove(Servers[i]);
                    break;
                }

                ++i;
            }
        }

        public List<Server> FindServer(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                throw new ArgumentException("Server name cannot be empty or consist of only white-space.");
            }
            List<Server> res = new List<Server>();
            foreach (Server server in Servers)
            {
                if (server.ServerName.Contains(s))
                    res.Add(server);
            }
            return res;
        }

        public void ExportServer(string filePath = "servers.xml")
        {
            if (Servers.Count == 0) return;
                
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<string>));
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                xmlSerializer.Serialize(fs, Servers);
            }
        }

        public List<Server> ImportServer(string filePath = "servers.xml")
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<string>));
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    return (List<Server>)xmlSerializer.Deserialize(fs);
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("File \"servers.xml\" not found.");
                return new List<Server>();
            }
        }
    }
}
