namespace IRC_Business.nlp;

using TencentCloud.Common;
using TencentCloud.Nlp.V20190408;
using TencentCloud.Nlp.V20190408.Models;

public class NLP {
	public static void LexicalAnalysis(string text) {
		var c = new NlpClient(new Credential {
			SecretId = Config.SECRET_ID,
			SecretKey = Config.SECRET_KEY
		}, "ap-guangzhou");

		var r = c.LexicalAnalysis(new LexicalAnalysisRequest {
			Text = text
		}).Result;
		
		if (r == null) {
			Console.WriteLine("null");
			return;
		}
		
		Console.WriteLine("PosTokens:");
		foreach (var token in r.PosTokens) {
			Console.WriteLine($"Offset: {token.BeginOffset}, Length: {token.Length}, Word: {token.Word}, Pos: {token.Pos}");
		}
		
		Console.WriteLine("\n\n");
		
		Console.WriteLine("NerTokens:");
		foreach (var token in r.NerTokens) {
			Console.WriteLine($"Offset: {token.BeginOffset}, Length: {token.Length}, Word: {token.Word}, Type: {token.Type}");
		}
		
	}
}