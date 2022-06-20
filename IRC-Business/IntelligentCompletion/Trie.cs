using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IRC_Business.IntelligentCompletion
{
    internal class Trie
    {
        public TrieNode beginNode;
        
        public Trie()
        {
            beginNode = new TrieNode();
        }

        public void AddVocabulary(string s)
        {
            if(string.IsNullOrEmpty(s))
            {
                throw new ArgumentException("Input vocabulary is null or empty.");
            }
            
            TrieNode current = beginNode;
            for (int i = 0; i < s.Length; i++)
            {
                TrieNode node = current.FindNextNode(s[i]);
                if (node == null)
                {
                    TrieNode newNode = new TrieNode(s[i]);
                    newNode.isEnd = i == s.Length - 1;
                    current.next.Add(newNode);
                    current = newNode;
                }
                else
                {
                    current = node;
                }
            }
            
            // //将词语加入字典树
            // for(int i = 0; i < s.Length; i++)
            // {
            //     if(i != s.Length - 1)
            //     {
            //         current.isEnd = false;
            //     }
            //     //找到下一个节点
            //     bool flag = true;
            //     foreach(TrieNode node in current.next)
            //     {
            //         if(node.character == s[i])
            //         {
            //             current = node;
            //             flag = false;
            //             break;
            //         }
            //     }
            //     //若在后继节点数组中未找到想找的节点，则新增进去
            //     if (flag)
            //     {
            //         current = new TrieNode(s[i]);
            //         current.next.Add(current);
            //     }
            // }
        }

        public bool FindVocabulary(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                throw new ArgumentException("Input vocabulary is null or empty.");
            }

            //     for (int i = 0; i < s.Length; i++)
            //     {
            //         if (current.isEnd && i != s.Length - 1)
            //         {
            //             return false;
            //         }
            //
            //         bool isFound = true;
            //         foreach (var node in current.next)
            //         {
            //             if (node.character == s[i])
            //             {
            //                 current = node;
            //                 isFound = true;
            //                 break;
            //             }
            //         }
            //
            //         if (!isFound)
            //         {
            //             return false;
            //         }
            //     }
            //
            //     return true;
            // }
            TrieNode current = this.beginNode;
            for (int i = 0; i < s.Length; i++)
            {
                current = current.FindNextNode(s[i]);
                if (current == null)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
