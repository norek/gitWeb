using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibGit2Sharp;

namespace gitWeb.Core.GraphBuilder
{
    public class HIndexProvider
    {
        private readonly List<ColumnsReservation> _Hindex = new List<ColumnsReservation>();
        private int index = 1;
        public HIndexProvider()
        {
            _Hindex.Add(new ColumnsReservation(index, 1, DateTime.MaxValue, DateTime.MinValue));
        }


        public ColumnsReservation GetFreeIndex(DateTime commitDate)
        {
            ColumnsReservation reservedColumn = null;
            for (int i = 0; i < _Hindex.Count; i++)
            {
                var d = _Hindex[i];
                if ((d.StartDate >= commitDate && !d.EndDate.HasValue) || (d.StartDate >= commitDate && d.EndDate.Value < commitDate))
                {
                    reservedColumn = d;
                    break;
                }
            }


            index++;
            var newReservedColumn = new ColumnsReservation(index, reservedColumn.Id + 1, commitDate, null);
            _Hindex.Add(newReservedColumn);

            return newReservedColumn;
        }

        public void ReleaseIndex(int hIndex, DateTime commitDate)
        {
            for (int i = 0; i < _Hindex.Count; i++)
            {
                if (_Hindex[i].Index == hIndex)
                {
                    _Hindex[i].EndDate = commitDate;
                    break;
                }
            }
        }
    }

    public class ColumnsReservation
    {
        public ColumnsReservation(int index, int id, DateTime startDate, DateTime? endDate)
        {
            Index = index;
            Id = id;
            StartDate = startDate;
            EndDate = endDate;
        }

        public int Index { get; set; }
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
