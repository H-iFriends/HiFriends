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
            if(string.IsNullOrWhiteSpace(s))
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
            
        }

        public bool FindVocabulary(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                throw new ArgumentException("Input vocabulary is null or empty.");
            }
            
            TrieNode current = this.beginNode;
            foreach (var c in s)
            {
                current = current.FindNextNode(c);
                if (current == null)
                {
                    return false;
                }
            }
            return true;
        }
        
        public TrieNode FindLastNode(string s){
            if (string.IsNullOrWhiteSpace(s))
            {
                throw new ArgumentException("Input vocabulary is null or empty.");
            }
            
            TrieNode current = this.beginNode;
            foreach (var c in s)
            {
                current = current.FindNextNode(c);
                if (current == null)
                {
                    return null;
                }
            }
            return current;
        }

        public List<string> FindAllMatched(string s)
        {
            List<string> result = new List<string>();
            //字典树中不存在s前缀 或 s为空
            if (!FindVocabulary(s) || string.IsNullOrWhiteSpace(s)) return result;
            

            Queue<string> queue = new Queue<string>();
            queue.Enqueue(s);
            while (queue.Count > 0)
            {
                string current = queue.Dequeue();
                var lastNode = FindLastNode(current);
                
                //此节点为最后一个节点
                if (lastNode.next.Count == 0)
                {
                    result.Add(current);
                    continue;
                }
                //此节点不为最后一个节点
                foreach (var node in lastNode.next)
                {
                    queue.Enqueue(current + node.character);
                }
            }

            return result;
        }
    }
}
