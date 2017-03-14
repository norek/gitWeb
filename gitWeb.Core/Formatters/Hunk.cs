using System.Collections.Generic;

namespace gitWeb.Core.Formatters
{
    public class Hunk
    {
        public string Header { get; set; }
        public List<Line> Lines { get; set; }
        public HunkLinePair[] HeaderFormatted { get; set; }
    }
}
