using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gitWeb.Core.Formatters
{
    public class Line
    {
        public EDiffFileType Type { get; set; }
        public string LineOrigNumber { get; set; }
        public string LineNewNumber { get; set; }
        public string Content { get; set; }
    }

    public enum EDiffFileType
    {
        Unchanged = 0,
        Add = 1,
        Remove = 2
    }
}
