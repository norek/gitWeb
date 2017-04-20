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
        //private readonly Dictionary<int, bool> _Hindex = new Dictionary<int, bool>();
        private readonly List<ColumnsReservation> _Hindex = new List<ColumnsReservation>();
        private int index = 1;
        public HIndexProvider()
        {
            _Hindex.Add(new ColumnsReservation(index, 1, DateTime.MaxValue, DateTime.MinValue));
        }


        public dynamic GetFreeIndex(DateTime commitDate)
        {
            var reservedColumn = _Hindex.Where(d => (d.StartDate >= commitDate && !d.EndDate.HasValue) ||
            (d.StartDate >= commitDate && d.EndDate.Value < commitDate)).OrderByDescending(s => s.Id).First();

            index++;
            var newReservedColumn = new ColumnsReservation(index, reservedColumn.Id + 1, commitDate, null);
            _Hindex.Add(newReservedColumn);

            return new { newReservedColumn.Id, newReservedColumn.Index };
        }

        public void ReleaseIndex(int hIndex, DateTime commitDate)
        {
            var columnToRelease = _Hindex.Single(d => d.Index == hIndex);
            columnToRelease.EndDate = commitDate;
        }
    }

    internal class ColumnsReservation
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
