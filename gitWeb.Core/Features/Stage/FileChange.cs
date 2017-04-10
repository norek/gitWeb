using System.Collections.Generic;
using gitWeb.Core.Formatters;

namespace gitWeb.Core.Features.Stage
{
    public class FileChange
    {
        public string FilePath { get; }

        public string Content { get; }

        public int LinesAdded { get; }

        public int LinesDeleted { get; }

        public List<Hunk> Hunks { get; }

        public FileChange(string filePath, string content, int linesAdded, int linesDeleted, List<Hunk> hunks)
        {
            FilePath = filePath;
            this.Content = content;
            this.LinesAdded = linesAdded;
            this.LinesDeleted = linesDeleted;
            this.Hunks = hunks;
        }

        public FileChange()
        {
            IsEmpty = true;
        }

        public bool IsEmpty { get; private set; }
    }
}