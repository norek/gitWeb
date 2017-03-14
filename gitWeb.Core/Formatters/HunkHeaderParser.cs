using System;
using System.Globalization;

namespace gitWeb.Core.Formatters
{
    public class HunkHeaderParser
    {
        private const string hunkSeparator = "@@";

        public HunkLinePair[] Parse(string hunkHeader)
        {
            int indexOfFirst = hunkHeader.IndexOf(hunkSeparator, StringComparison.Ordinal);
            var indexOfLast = hunkHeader.LastIndexOf(hunkSeparator, StringComparison.Ordinal);
            if (indexOfFirst == 0
                && indexOfLast != 0)
            {
                var startIndex = indexOfFirst + hunkSeparator.Length;
                string hunkLineNoData = hunkHeader.Substring(startIndex, indexOfLast - startIndex).Trim();
                string[] splitedNoData = hunkLineNoData.Split(new char[] { (char)160, (char)32 });

                HunkLinePair[] hunkHeaderArr = new HunkLinePair[2];
                for (int i = 0; i < splitedNoData.Length; i++)
                {
                    string[] splitedPair = splitedNoData[i].Split(',');

                    HunkLinePair pair = new HunkLinePair();
                    pair.StartingLineNumber = int.Parse(splitedPair[0], NumberStyles.AllowLeadingSign);
                    pair.NumberOfLines = int.Parse(splitedPair[1]);

                    if (pair.StartingLineNumber < 0)
                    {
                        pair.StartingLineNumber = pair.StartingLineNumber * (-1);
                    }

                    hunkHeaderArr[i] = pair;
                }

                return hunkHeaderArr;
            }
            else
            {
                throw new FormatException();
            }
        }
    }
}
