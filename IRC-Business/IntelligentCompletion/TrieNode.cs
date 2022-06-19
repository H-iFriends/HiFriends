using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRC_Business.IntelligentCompletion
{
    public class TrieNode
    {
        public List<TrieNode> next;
        public bool isEnd;
        public char character;

        public TrieNode()
        {
            next = new List<TrieNode>();
            isEnd = true;
            character = '0';
        }

        public TrieNode(char character)
        {
            next = new List<TrieNode>();
            isEnd = true;
            this.character = character;
        }

        public TrieNode(List<TrieNode> next, bool isEnd, char character)
        {
            this.next = next;
            this.isEnd = isEnd;
            this.character = character;
        }
    }
}
