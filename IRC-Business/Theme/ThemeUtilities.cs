using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRC_Business.Theme
{
    /*
     * 获取频道的主题
     */
    public class ThemeUtilities
    {
        public Dictionary<string, int> WordStatistics = new Dictionary<string, int>();

        public void AddWords(string[] words)
        {
            foreach (string word in words)
            {
                if (this.WordStatistics.ContainsKey(word))
                {
                    this.WordStatistics[word]++;
                }
                this.WordStatistics.Add(word, 1);
            }
        }

        public string GetTheme()
        {
            int max = 0;
            string theme = "";
            foreach (KeyValuePair<string, int> pair in WordStatistics)
            {
                if (pair.Value > max)
                {
                    max = pair.Value;
                    theme = pair.Key;
                }
            }

            return theme;
        }
    }
}