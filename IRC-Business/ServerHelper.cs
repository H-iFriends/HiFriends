using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace IRC_Business
{
    /*
     * 服务器管理
     */
    internal class ServerHelper
    {
        public List<string> Servers { get; } = new List<string>();

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
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("File \"servers.xml\" not found.");
                return new List<string>();
            }



        }
    }
}
