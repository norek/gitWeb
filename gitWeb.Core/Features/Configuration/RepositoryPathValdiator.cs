using System.IO;
using System.Linq;

namespace gitWeb.Core.Features.Configuration
{
    public class RepositoryPathValdiator
    {
        public bool IsInvalid(string path)
        {
            if (Directory.Exists(path) && Directory.EnumerateDirectories(path).Any(s => s == Path.Combine(path, ".git")))
            {
                return false;
            }

            return true;
        }
    }
}