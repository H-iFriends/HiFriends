using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRC_Business
{
    internal class Channel
    {
        private string ChannelName;
        private string ChannelId;
        private int UserCount;
        //频道简介
        private string Description;
        //最近主题
        private List<string> Themes;
    }
}
