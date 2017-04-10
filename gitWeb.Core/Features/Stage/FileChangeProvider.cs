using gitWeb.Core.Formatters;
using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gitWeb.Core.Features.Stage
{
    public class FileChangeProvider : IFileChangeProvider
    {
        private readonly IRepository _repository;

        public FileChangeProvider(IRepository repository)
        {
            _repository = repository;
        }


        public FileChange GetFileDiff(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return new FileChange();
            }

            Patch patch = _repository.Diff.Compare<Patch>(new List<string>() { filePath });

            ChanageContentFormatter contentFormatter = new ChanageContentFormatter();
            List<Hunk> formattedContent = contentFormatter.Format(patch.Content);

            return new FileChange(filePath, patch.Content, patch.LinesAdded, patch.LinesDeleted, formattedContent);
        }
    }
}
