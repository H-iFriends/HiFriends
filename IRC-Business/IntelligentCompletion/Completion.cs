using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace IRC_Business.IntelligentCompletion;

public class Completion
{
    private static Trie Trie = new Trie();
    private string Sentence = "";


    public Completion(string sentence)
    {
        Trie.AddVocabulary("student");
        Trie.AddVocabulary("stupid");
        Trie.AddVocabulary("apple");
        Trie.AddVocabulary("like");
        string s =
            "The indefinite article takes two forms. It’s the word a when it precedes a word that begins with a consonant. It’s the word an when it precedes a word that begins with a vowel. The indefinite article indicates that a noun refers to a general idea rather than a particular thing. For example, you might ask your friend, “Should I bring a gift to the party?” Your friend will understand that you are not asking about a specific type of gift or a specific item. “I am going to bring an apple pie,” your friend tells you. Again, the indefinite article indicates that she is not talking about a specific apple pie. Your friend probably doesn’t even have any pie yet. The indefinite article only appears with singular nouns. Consider the following examples of indefinite articles used in context";
        s = Regex.Replace(s, @"[^\w\s]", string.Empty);
        var words = s.Split(" ");
        foreach (var word in words)
        {
            Trie.AddVocabulary(word);
        }

        this.Sentence = sentence;
    }

    //若最后一个字符为空格，返回的是空字符串""
    public string GetLast()
    {
        if (string.IsNullOrWhiteSpace(Sentence))
            return "";

        bool flag = Sentence[^1] == ' ' || Sentence[^1] == '.' || Sentence[^1] == ',' || Sentence[^1] == '?';

        var words = Sentence.Split(' ');
        var last = words[^1];

        // 将用户输入的词添加到字典树中
         if (flag)
         {
             Trie.AddVocabulary(words[^2]); //words[^1] == ""
         }

        return last;
    }

    public List<string> CompleteLastWord()
    {
        var last = GetLast();
        if (string.IsNullOrEmpty(last))
        {
            return new List<string>();
        }

        var matchedWords = Trie.FindAllMatched(last);
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