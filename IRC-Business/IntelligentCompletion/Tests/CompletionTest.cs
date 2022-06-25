using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IRC_Business.IntelligentCompletion.Tests;

[TestClass]
public class CompletionTest
{
    public Completion Completion1 = new Completion("I am a s");
    public Completion Completion2 = new Completion("I am a ");
    public Completion Completion3 = new Completion("I am a student");
    public Completion Completion4 = new Completion("I am a student. I like eating ap");


    
    [TestMethod]
    public void GetLastTest()
    {
        var last1 = Completion1.GetLast();
        Assert.IsTrue(last1.Equals("s"));
        var last2 = Completion2.GetLast();
        Assert.IsTrue(last2.Equals(""));
    }

    [TestMethod]
    public void CompletionLastWordTest()
    {
        List<string> lastWords1 = Completion1.CompleteLastWord();
        CollectionAssert.AreEquivalent(new List<string>(){"student", "stupid"}, lastWords1);
        List<string> lastWords2 = Completion2.CompleteLastWord();
        CollectionAssert.AreEquivalent(new List<string>(){}, lastWords2);
        List<string> lastWords3 = Completion3.CompleteLastWord();
        CollectionAssert.AreEquivalent(new List<string>(){"student"}, lastWords3);

        
    }

    [TestMethod]
    public void GetNewSentenceTest()
    {
        var newSentence1 = Completion1.GetNewSentence("student");
        Assert.IsTrue(newSentence1.Equals("I am a student"));

        var newSentence4 = Completion4.GetNewSentence("apple");
        Assert.IsTrue(newSentence4.Equals("I am a student. I like eating apple"));
    }
}