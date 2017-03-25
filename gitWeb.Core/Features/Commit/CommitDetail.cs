using System;
using System.Collections.Generic;

namespace gitWeb.Core.Features.Commit
{
    public class CommitDetail
    {

        public CommitDetail(string sha, string message, string author, DateTime date)
        {
            Sha = sha;
            Message = message;
            Author = author;
            Date = date;

            Files = new List<CommitFile>();
        }

        public string Sha { get; set; }
        public string Message { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }

        public List<CommitFile> Files { get; private set; }
    }
}