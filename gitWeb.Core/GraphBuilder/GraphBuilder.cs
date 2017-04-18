using gitWeb.Core.Features.Commit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gitWeb.Core.GraphBuilder
{
    public class GraphBuilder
    {
        List<Commit> MainPath = new List<Commit>();
        HIndexProvider hIndexProvider = new HIndexProvider();

        public GraphBuilder()
        {
        }

        public void Build(Commit[] commits)
        {
            Stopwatch diagnostic = new Stopwatch();
            diagnostic.Start();
            AssignParents(commits);
            diagnostic.Stop();
            Debug.WriteLine("     AssignParents: " + diagnostic.ElapsedMilliseconds);

            diagnostic.Restart();
            CreateMainPath(commits[0]);
            diagnostic.Stop();
            Debug.WriteLine("     CreateMainPath: " + diagnostic.ElapsedMilliseconds);

            diagnostic.Restart();
            AssignParentColumn(commits[0]);
            diagnostic.Stop();
            Debug.WriteLine("AssignParentColumn: " + diagnostic.ElapsedMilliseconds);

        }

        private void AssignParents(Commit[] commits)
        {
            int index = 0;

            for (int i = 0; i < commits.Length; i++)
            {
                index++;
                var currCommit = commits[i];
                currCommit.SetVIndex(index);

                var currCommitParents = commits.Where(c => currCommit.ParentsIds.Contains(c.Id)).ToArray();
                currCommit.AssignParents(currCommitParents);
            }
        }

        private void AssignParentColumn(Commit commit)
        {
            for (int i = 0; i < commit.CommitParents.Length; i++)
            {
                var currParent = commit.CommitParents[i];

                if (currParent.HIndex == 0)
                {
                    if (i == 0)
                    {
                        currParent.SetHIndex(commit.HIndex);
                    }
                    else
                    {
                        currParent.SetHIndex(hIndexProvider.GetFreeIndex()); ;
                    }
                }
                else
                {
                    if (currParent.HIndex < commit.HIndex && commit.CommitParents.Length == 1)
                    {
                        hIndexProvider.ReleaseIndex(commit.HIndex);
                    }
                }
            }

            for(var i = commit.CommitParents.Length; i-- > 0;)
            {
                AssignParentColumn(commit.CommitParents[i]);
            }
        }

        private void CreateMainPath(Commit currentCommit)
        {
            if (currentCommit == null) return;

            currentCommit.SetHIndex(1);

            MainPath.Add(currentCommit);

            if (currentCommit.CommitParents.Length > 0)
            {
                CreateMainPath(currentCommit.CommitParents[0]);
            }

        }
    }
}
