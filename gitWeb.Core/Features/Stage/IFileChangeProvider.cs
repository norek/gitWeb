namespace gitWeb.Core.Features.Stage
{
    public interface IFileChangeProvider
    {
        FileChange GetFileDiff(string filePath);

        void DiscardAllChanges();

        void DiscardFileChanges(string filePath);
    }
}