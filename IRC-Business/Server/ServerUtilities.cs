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
        public List<string> Servers { get; }

        public ServerUtilities()
        {
            Servers = new List<string>();
        }

        public ServerUtilities(List<string> servers)
        {
            Servers = servers;
        }

        public void AddServer(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                throw new ArgumentException("Server name cannot be empty or consist of only white-space.");
            }
            if (Servers.Contains(s))
                return;
            Servers.Add(s);
        }

        public void RemoveServer(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                throw new ArgumentException("Server name cannot be empty or consist of only white-space.");
            }
            if (Servers.Contains(s))
                Servers.Remove(s);
        }

        public List<string> FindServer(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                throw new ArgumentException("Server name cannot be empty or consist of only white-space.");
            }
            List<string> res = new List<string>();
            foreach (string server in Servers)
            {
                if (server.Contains(s))
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

        public List<string> ImportServer(string filePath = "servers.xml")
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<string>));
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    return (List<string>)xmlSerializer.Deserialize(fs);
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("File \"servers.xml\" not found.");
                return new List<string>();
            }
        }
    }
}
