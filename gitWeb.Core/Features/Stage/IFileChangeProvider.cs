namespace gitWeb.Core.Features.Stage
{
    public interface IFileChangeProvider
    {
        FileChange GetFileDiff(string filePath);

        void DiscardFileChanges(string filePath);
    }
}