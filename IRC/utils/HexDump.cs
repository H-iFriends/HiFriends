namespace IRC.utils;

using System.Text;

public class HexDump {
	public static string Get(byte[] data, int offset, int length) {
		var sb = new StringBuilder();
		int i;
		for (i = 0; i < length; i += 16) {
			int t = length - i > 16 ? 16 : length - i;
			sb.AppendFormat("{0:X4} | ", i + offset);
			for (int j = i; j < i + t; j++)
				sb.AppendFormat("{0:X2} ", data[j + offset]);
			for (int j = t; j < 16; j++)
				sb.Append("   ");
			sb.Append(" | ");
			for (int j = i; j < i + t; j++) {
				char c = (char)data[j + offset];
				if (c < ' ' || c > '~')
					c = '.';
				sb.Append(c);
			}
			sb.AppendLine();
		}
		return sb.ToString();
	}
}
