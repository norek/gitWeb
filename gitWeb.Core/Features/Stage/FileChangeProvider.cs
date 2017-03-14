using gitWeb.Core.Formatters;
using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gitWeb.Core.Features.Stage
{
    public class FileChangeProvider
    {
        private IRepository _repository;

        public FileChangeProvider(IRepository repository)
        {
            _repository = repository;
        }

        public object GetFileDiff(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return new object();
            }


            Patch patch = _repository.Diff.Compare<Patch>(new List<string>() { filePath });

            ChanageContentFormatter contentFormatter = new ChanageContentFormatter();
            List<Hunk> formattedContent = contentFormatter.Format(patch.Content);

            var fileChange = new
            {
                filePath,
                content = patch.Content,
                linesAdded = patch.LinesAdded,
                linesDeleted = patch.LinesDeleted,
                hunks = formattedContent
            };
            return fileChange;
            //return Ok(fileChange);
        }
    }
}
