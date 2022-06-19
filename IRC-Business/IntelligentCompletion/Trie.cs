using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRC_Business.IntelligentCompletion
{
    internal class Trie
    {
        private TrieNode trie;
        
        public Trie()
        {
            trie = new TrieNode();
        }

        public void AddVocabulary(string s)
        {
            if(s == null || s.Length == 0)
            {
                throw new ArgumentException("Input vocabulary is null or empty.");
            }
            
            TrieNode current = trie;
            
            
            //将词语加入字典树
            for(int i = 0; i < s.Length; i++)
            {
                if(i != s.Length - 1)
                {
                    current.isEnd = false;
                }
                //找到下一个节点
                bool flag = true;
                foreach(TrieNode node in current.next)
                {
                    if(node.character == s[i])
                    {
                        current = node;
                        flag = false;
                        break;
                    }
                }
                //若在后继节点数组中未找到想找的节点，则新增进去
                if (flag)
                {
                    current = new TrieNode(s[i]);
                    current.next.Add(current);
                }
            }
        }
    }
}
