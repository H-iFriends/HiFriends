using Microsoft.VisualBasic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace IRC_Business.IntelligentCompletion.Tests;

[TestClass]
public class TrieTest
{
    private Trie trie = new Trie();
    
    [TestInitialize]
    public void Init()
    {
        //hellos, hi, cat
        trie.beginNode.isEnd = false;
        TrieNode node1 = new TrieNode(new List<TrieNode>(), false, 'h');
        TrieNode node2 = new TrieNode(new List<TrieNode>(), false, 'e');
        TrieNode node3 = new TrieNode(new List<TrieNode>(), false, 'l');
        TrieNode node4 = new TrieNode(new List<TrieNode>(), false, 'l');
        TrieNode node5 = new TrieNode(new List<TrieNode>(), false, 'o');
        TrieNode node6 = new TrieNode(new List<TrieNode>(), true, 's');

        TrieNode node7 = new TrieNode(new List<TrieNode>(), true, 'i');
        
        TrieNode node8 = new TrieNode(new List<TrieNode>(), false, 'c');
        TrieNode node9 = new TrieNode(new List<TrieNode>(), false, 'a');
        TrieNode node10 = new TrieNode(new List<TrieNode>(), true, 't');

        //hellos
        trie.beginNode.next.Add(node1);
        node1.next.Add(node2);        
        node2.next.Add(node3);
        node3.next.Add(node4);
        node4.next.Add(node5);
        node5.next.Add(node6);
        
        //hi
        node1.next.Add(node7);
        
        //cat
        trie.beginNode.next.Add(node8);
        node8.next.Add(node9);
        node9.next.Add(node10);

    }
    
    [TestMethod]
    public void AddVocabularyTest()
    {
        trie.AddVocabulary("你好");
        trie.AddVocabulary("你好呀");
        trie.AddVocabulary("谢谢你");
        bool result1 = trie.FindVocabulary("你好");
        bool result2 = trie.FindVocabulary("你好呀");
        bool result3 = trie.FindVocabulary("谢谢你");
        
        Assert.IsTrue(result1 && result2 && result3);
        
        try
        {
            trie.AddVocabulary("");
        }
        catch (ArgumentException e)
        {
            Assert.AreEqual("Input vocabulary is null or empty.", e.Message);
        }
        try
        {
            trie.AddVocabulary(null);
        }
        catch (ArgumentException e)
        {
            Assert.AreEqual("Input vocabulary is null or empty.", e.Message);
        }
    }

    [TestMethod]
    public void FindVocabularyTest()
    {
        bool result1 = trie.FindVocabulary("hello");
        bool result2 = trie.FindVocabulary("hellos");
        bool result3 = trie.FindVocabulary("hi");
        bool result4 = trie.FindVocabulary("cat");
        bool result5 = trie.FindVocabulary("NotExisted");
        Assert.IsTrue(result1 && result2 && result3 && result4);
        Assert.IsFalse(result5);
        
        try
        {
            trie.FindVocabulary("");
        }
        catch (ArgumentException e)
        {
            Assert.AreEqual("Input vocabulary is null or empty.", e.Message);
        }
        
        try
        {
            trie.FindVocabulary(null);
        }
        catch (ArgumentException e)
        {
            Assert.AreEqual("Input vocabulary is null or empty.", e.Message);
        }
    }

    [TestMethod]
    public void FindLastNodeTest()
    {
        var node1 = trie.FindLastNode("cat");
        var node2 = trie.FindLastNode("hello");
        var node3 = trie.FindLastNode("hellos");
        var node4 = trie.FindLastNode("NotExist");
        
        Assert.IsTrue(node1.character == 't');
        Assert.IsTrue(node2.character == 'o');
        Assert.IsTrue(node3.character == 's');
        Assert.IsNull(node4);
    }

    [TestMethod]
    public void FindAllMatchedTest()
    {
        CollectionAssert.AreEquivalent(new List<string>(){"hellos", "hi"}, trie.FindAllMatched("h"));
        CollectionAssert.AreEquivalent(new List<string>(){"cat"}, trie.FindAllMatched("c"));
        CollectionAssert.AreEquivalent(new List<string>(), trie.FindAllMatched("NotExist"));

    }
}