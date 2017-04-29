using System;
using System.Collections.Generic;
using System.Linq;
using LibGit2Sharp;
using Newtonsoft.Json;

namespace gitWeb.Core.Features.Commit
{
    public class Commit
    {
        public bool handled;
        public bool skipped;

        public Commit()
        {
            ReachableBranches = new List<string>();
            Children = new Dictionary<string, Commit>();
        }

        public int HIndex { get; private set; }

        public string Sha { get; set; }

        [JsonIgnore]
        public List<string> Parents { get; set; }

        public string Name { get; set; }
        public DateTime Date { get; set; }

        public string Message { get; set; }

        public List<string> ReachableBranches { get; private set; }

        public void AddReachableBranch(string branchName)
        {
            if (!ReachableBranches.Contains(branchName))
            {
                ReachableBranches.Add(branchName);
            }
        }

        [JsonIgnore]
        public Commit[] CommitParents { get; set; }
        [JsonIgnore]
        public IEnumerable<ObjectId> ParentsIds { get; set; }
        
        public override string ToString() => Sha + "Index: " + HIndex;

        public void SetVIndex(int index)
        {
            VIndex = index;
            Y = 40 + index * 22;
        }

        public void SetHIndex(int index)
        {
            HIndex = index;
            X = HIndex * 20;
        }

        public int VIndex { get; private set; }

        public int X { get; private set; }
        public int Y { get; set; }

        public void SetHIndex(int id, int index)
        {
            SetHIndex(id);
            ColumnInternalIndex = index;
        }

        public int ColumnInternalIndex { get; private set; }

        public void AddChild(Commit child)
        {
            if (!Children.ContainsKey(child.Sha))
            {
                Children.Add(child.Sha, child);
            }

        }

        [JsonIgnore]
        public Dictionary<string, Commit> Children { get; private set; }

        internal void AssignParents(Commit[] currCommitParents)
        {
            List<Commit> orderedCommits = new List<Commit>();

            if (currCommitParents.Length > 1)
            {
                foreach (var parentSha in Parents)
                {
                    var parent = currCommitParents.First(s => s.Sha == parentSha);
                    orderedCommits.Add(parent);
                }

                CommitParents = orderedCommits.ToArray();

            }
            else
            {
                CommitParents = currCommitParents;

            }

        }

    }
}