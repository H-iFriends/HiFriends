using Microsoft.VisualStudio.TestTools.UnitTesting;
using IRC_Business.nlp;

namespace IRC_Business.Theme.Tests;

[TestClass]
public class ThemeUtilitiesTest
{
    public ThemeUtilities ThemeUtilities = new ThemeUtilities();

    [TestMethod]
    public void GetThemeTest()
    {
        const string s = "我喜欢打篮球。打篮球是我最大的爱好。";
        var words = NLP.GetKeywords(s);
        this.ThemeUtilities.AddWords(words);
        var theme = this.ThemeUtilities.GetTheme();
        
        Assert.IsTrue(theme.Equals("打篮球(n)"));
    }
}