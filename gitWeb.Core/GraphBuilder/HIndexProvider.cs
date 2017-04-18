using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gitWeb.Core.GraphBuilder
{
    public class HIndexProvider
    {
        private readonly Dictionary<int, bool> _Hindex = new Dictionary<int, bool>();
        private int _maxIndex = 1;
        public HIndexProvider()
        {
            _Hindex.Add(_maxIndex, false);
        }


        public int GetFreeIndex()
        {
            var freeIndex = _Hindex.FirstOrDefault(d => d.Value);

            if (freeIndex.Key == 0)
            {
                _maxIndex++;
                _Hindex.Add(_maxIndex, false);
            }

            return _maxIndex;
        }

        public void ReleaseIndex(int hIndex)
        {
            _Hindex[hIndex] = true;
        }
    }
}
