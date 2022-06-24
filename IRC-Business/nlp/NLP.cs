namespace IRC_Business.nlp;

using TencentCloud.Common;
using TencentCloud.Nlp.V20190408;
using TencentCloud.Nlp.V20190408.Models;

public class NLP {
	
	private static readonly HashSet<string> acceptablePos = new(new string[] {
		"a",
		"ad",
		"al",
		"an",
		"b",
		// "d",
		// "f",
		"g",
		"gb",
		"gbc",
		"gc",
		"gg",
		"gi",
		"gm",
		"gp",
		"i",
		"l",
		"v",
		"vd",
		"vf",
		"vg",
		"vi",
		"vl",
		"vn",
		"vx",
	});

	public static LexicalAnalysisResponse LexicalAnalysis(string text) {
		var c = new NlpClient(new Credential {
			SecretId = Config.SECRET_ID,
			SecretKey = Config.SECRET_KEY
		}, "ap-guangzhou");

		return c.LexicalAnalysis(new LexicalAnalysisRequest {
			Text = text
		}).Result;
	}

	public static string[] GetKeywords(string text) {
		var response = LexicalAnalysis(text);
		var keywordSet = new HashSet<string>();
		foreach (var t in response.PosTokens) {
			if (!t.Pos.StartsWith('n') && !acceptablePos.Contains(t.Pos))
				continue;
			keywordSet.Add($"{t.Word}({t.Pos})");
		}
		return keywordSet.ToArray();
	}
}