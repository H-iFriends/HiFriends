namespace IRCTest; 

using IRC_Business.nlp;

[TestClass]
public class BusinessTest {
	[TestMethod]
	public void TestNLP() {
		NLP.LexicalAnalysis("南都此前报道，6月20日晚，香港仔饮食集团发布声明称，18日下午，珍宝海鲜舫行驶至南海西沙群岛附近水域时，遇上风浪，船身入水开始倾侧。负责航程的拖船公司经过尝试救援后未果，海鲜舫最终于19日全面入水翻转。香港仔饮食集团指出，事件中未有任何船员受伤。由于事发地点水深超过1000米，预计打捞工程非常困难。据该集团介绍，珍宝海鲜舫于6月14日启航离港，离港前根据有关规定，已聘请专业海事工程人员详细检查船身和加上围板，并得到一切所需批文进行本次航行。");
	}
}