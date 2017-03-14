using System;
using System.Collections.Generic;

namespace gitWeb.Core.Formatters
{
    public class ChanageContentFormatter
    {
        private HunkHeaderParser _hunkHeaderParser;

        private const string _hunkSeparator = "@@";
        private const string _orginalFilePrecededSeparaor = "---";
        private const string _newFilePrecededSeparator = "+++";
        private const string _additionalLineChange = "+";
        private const string _removeLineChange = "-";
        private const int _headerLenght = 4;

        public ChanageContentFormatter()
        {
            _hunkHeaderParser = new HunkHeaderParser();
        }

        public List<Hunk> Format(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return new List<Hunk>();
            }

            string[] contentLines = content.Replace(' ', '\u00a0').Split(Environment.NewLine.ToCharArray());

            List<Hunk> hunkList = new List<Hunk>();
            Hunk hunk = null;

            int origLineNumber = 0;
            int newLineNumber = 0;

            for (int i = _headerLenght; i < contentLines.Length; i++)
            {
                string currLine = contentLines[i];

                if (currLine.IndexOf(_hunkSeparator, StringComparison.Ordinal) == 0)
                {

                    hunk = new Hunk();
                    hunk.Header = currLine;
                    hunk.Lines = new List<Line>();
                    hunk.HeaderFormatted = _hunkHeaderParser.Parse(currLine);

                    origLineNumber = hunk.HeaderFormatted[0].StartingLineNumber;
                    newLineNumber = hunk.HeaderFormatted[1].StartingLineNumber;

                    hunkList.Add(hunk);
                }
                else
                {
                    if (hunk == null)
                    {
                        continue;
                    }

                    Line newLine = new Line();
                    if (currLine.IndexOf(_additionalLineChange, StringComparison.Ordinal) == 0)
                    {
                        newLine.Type = EDiffFileType.Add;
                        currLine = currLine.Substring(1);
                        newLine.LineNewNumber = _additionalLineChange + newLineNumber;
                        newLineNumber++;
                    }
                    else if (currLine.IndexOf(_removeLineChange, StringComparison.Ordinal) == 0)
                    {
                        newLine.Type = EDiffFileType.Remove;
                        currLine = currLine.Substring(1);
                        newLine.LineOrigNumber = _removeLineChange + origLineNumber;
                        origLineNumber++;
                    }
                    else
                    {
                        newLine.Type = EDiffFileType.Unchanged;
                        newLine.LineOrigNumber = origLineNumber.ToString();
                        newLine.LineNewNumber = newLineNumber.ToString();

                        origLineNumber++;
                        newLineNumber++;
                    }

                    newLine.Content = currLine;
                    hunk.Lines.Add(newLine);
                }
            }

            return hunkList;
        }
    }
}
