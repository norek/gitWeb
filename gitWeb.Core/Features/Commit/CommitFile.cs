using LibGit2Sharp;

namespace gitWeb.Core.Features.Commit
{
    public class CommitFile
    {

        public CommitFile(int status, string path,string name)
        {
            ChangeType = status;
            Path = path;
            Name = name;
        }

        public string Path { get; private set; }
        public int ChangeType { get; private set; }
        public string Name { get; private set; }
    }
}