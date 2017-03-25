using LibGit2Sharp;

namespace gitWeb.Core.Features.Commit
{
    public class CommitFile
    {

        public CommitFile(int status, string path)
        {
            ChangeType = status;
            Path = path;
        }

        public string Path { get; private set; }
        public int ChangeType { get; private set; }
    }
}