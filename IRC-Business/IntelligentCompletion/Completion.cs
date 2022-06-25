using System.Runtime.CompilerServices;

namespace IRC_Business.IntelligentCompletion;

public class Completion
{
    private Trie Trie = new Trie();
    private string Sentence = "";
    

    public Completion(string sentence)
    {
        this.Trie.AddVocabulary("student");
        this.Trie.AddVocabulary("stupid");
        this.Trie.AddVocabulary("apple");
        this.Trie.AddVocabulary("like");

        this.Sentence = sentence;
    }
    
    //若最后一个字符为空格，返回的是空字符串""
    public string GetLast()
    {
        if (string.IsNullOrWhiteSpace(Sentence))
            return "";
        
        var words = Sentence.Split(' ');
        var last = words[^1];
        return last;
    }

    public List<string> CompleteLastWord()
    {
        
        var last = GetLast();
        if (string.IsNullOrEmpty(last))
        {
            return new List<string>();
        }
        var matchedWords = this.Trie.FindAllMatched(last);
        return matchedWords;
    }

    public string GetNewSentence(string lastWord)
    {
        if (Sentence[^1] == ' ')
            return Sentence;
        var words = Sentence.Split(" ").ToList();
        //去除最后一个未/已完成的单词
        words.Remove(words[^1]);

        string sentence = "";
        //重新形成句子
        foreach (var word in words)
        {
            sentence += word + " ";
        }
        //加上用户选择的单词
        sentence += lastWord;
        return sentence;
    }
}