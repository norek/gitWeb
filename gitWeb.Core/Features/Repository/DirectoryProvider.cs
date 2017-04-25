using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gitWeb.Core.Features.Repository
{
    public class DirectoryProvider
    {
        public IEnumerable<DirectoryInfo> GetDirectoryList(string path)
        {
            var directiories = Directory.GetDirectories(path);

            return directiories.Select(d => new DirectoryInfo() { Path = d, Mapped = false }).ToList();
        }
    }

    public class DirectoryInfo
    {
        public string Path { get; set; }
        public bool Mapped { get; set; }
    }
}
