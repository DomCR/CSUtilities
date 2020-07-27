using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace CSUtilities.IO
{
	public static class Csv
	{
		private const string QUOTE = "\"";
		private const string ESCAPED_QUOTE = "\"\"";
		private static char[] CHARACTERS_THAT_MUST_BE_QUOTED = { ',', '"', '\n' };
		public static string Escape(string s)
		{
			if (s.Contains(QUOTE))
				s = s.Replace(QUOTE, ESCAPED_QUOTE);

			if (s.IndexOfAny(CHARACTERS_THAT_MUST_BE_QUOTED) > -1)
				s = QUOTE + s + QUOTE;

			return s;
		}
		public static string Unescape(string s)
		{
			if (s.StartsWith(QUOTE) && s.EndsWith(QUOTE))
			{
				s = s.Substring(1, s.Length - 2);

				if (s.Contains(ESCAPED_QUOTE))
					s = s.Replace(ESCAPED_QUOTE, QUOTE);
			}

			return s;
		}
	}

	public sealed class CsvReader : System.IDisposable
	{
		public long RowIndex { get { return m_rownumber; } }
		private long m_rownumber = 0;
		private TextReader m_reader;
		private static Regex rexCsvSplitter = new Regex(@",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))");
		private static Regex rexRunOnLine = new Regex(@"^[^""]*(?:""[^""]*""[^""]*)*""[^""]*$");

		public CsvReader(string path) : this(new FileStream(path, FileMode.Open, FileAccess.Read)) { }
		public CsvReader(Stream stream)
		{
			m_reader = new StreamReader(stream);
		}

		public System.Collections.IEnumerable RowEnumerator
		{
			get
			{
				if (null == m_reader)
					throw new System.ApplicationException("I can't start reading without CSV input.");

				m_rownumber = 0;
				string sLine;
				string sNextLine;

				while (null != (sLine = m_reader.ReadLine()))
				{
					while (rexRunOnLine.IsMatch(sLine) && null != (sNextLine = m_reader.ReadLine()))
						sLine += "\n" + sNextLine;

					m_rownumber++;
					string[] values = rexCsvSplitter.Split(sLine);

					for (int i = 0; i < values.Length; i++)
						values[i] = Csv.Unescape(values[i]);

					yield return values;
				}

				m_reader.Close();
			}

		}
		public void Dispose()
		{
			if (null != m_reader) m_reader.Dispose();
		}
	}
}
