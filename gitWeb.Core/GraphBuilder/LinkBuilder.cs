using System;
using System.Collections.Generic;
using System.Linq;
using gitWeb.Core.Features.Commit;

namespace gitWeb.Core.GraphBuilder
{
    public class LinkBuilder
    {
        public List<Link> Build(Dictionary<string, Commit> commits)
        {
            List<Link> links = new List<Link>();
            List<Commit> inetrCommits = new List<Commit>();

            foreach (var currentCommit in commits.Values)
            {
                var parents = currentCommit.Parents;

                for (int j = 0; j < parents.Count; j++)
                {
                    Commit parent;
                    commits.TryGetValue(parents[j], out parent);

                    if (parent != null)
                    {
                        if (currentCommit.HIndex == parent.HIndex)
                        {
                            links.Add(new Link(currentCommit, parent));
                        }
                        else
                        {
                            Commit interCommit = new Commit();
                            interCommit.SetHIndex(parent.HIndex);
                            interCommit.Y = currentCommit.Y + 12;

                            links.Add(new Link(currentCommit, interCommit));
                            links.Add(new Link(interCommit, parent));

                            inetrCommits.Add(interCommit);
                        }

                    }
                }
            }

            return links;
        }

        public List<Link> BuildNoOptimalizedLinq(List<Commit> commits)
        {
            List<Link> links = new List<Link>();
            List<Commit> inetrCommits = new List<Commit>();

            foreach (var currentCommit in commits)
            {
                var parents = commits.Where(d => currentCommit.Parents.Contains(d.Sha));

                foreach (var parent in parents)
                {
                    CreateLinks(currentCommit, parent, links, inetrCommits);
                }
            }
            return links;
        }

        public List<Link> Build_ClassicApproach_WithToList(List<Commit> commits)
        {
            List<Link> links = new List<Link>();
            List<Commit> inetrCommits = new List<Commit>();

            foreach (var currentCommit in commits)
            {
                var parents = commits.Where(d => currentCommit.Parents.Contains(d.Sha)).ToList();

                foreach (var parent in parents)
                {
                    CreateLinks(currentCommit, parent, links, inetrCommits);
                }
            }
            return links;
        }

        public List<Link> Build_On_Array_WithLinq(Commit[] commits)
        {
            List<Link> links = new List<Link>();
            List<Commit> inetrCommits = new List<Commit>();

            foreach (var currentCommit in commits)
            {
                var parents = commits.Where(d => currentCommit.Parents.Contains(d.Sha));

                foreach (var parent in parents)
                {
                    CreateLinks(currentCommit, parent, links, inetrCommits);
                }
            }

            return links;
        }

        public List<Link> BuildOnArray_ForLoop(Commit[] commits)
        {
            List<Link> links = new List<Link>();
            List<Commit> inetrCommits = new List<Commit>();

            for (int index = 0; index < commits.Length; index++)
            {
                var currentCommit = commits[index];

                for (int i = 0; i < currentCommit.Parents.Count; i++)
                {
                    var parentSha = currentCommit.Parents[i];

                    for (int j = 0; j < commits.Length; j++)
                    {
                        var parent = commits[j];

                        if (parent.Sha == parentSha)
                        {
                            CreateLinks(currentCommit, parent, links, inetrCommits);
                            break;
                        }
                    }
                }

               
            }
            return links;
        }

        private static void CreateLinks(Commit currentCommit, Commit parent, List<Link> links, List<Commit> inetrCommits)
        {
            if (currentCommit.HIndex == parent.HIndex)
            {
                links.Add(new Link(currentCommit, parent));
            }
            else
            {
                Commit interCommit = new Commit();
                interCommit.SetHIndex(parent.HIndex);
                interCommit.Y = currentCommit.Y + 12;

                links.Add(new Link(currentCommit, interCommit));
                links.Add(new Link(interCommit, parent));

                inetrCommits.Add(interCommit);
            }
        }

        public List<Link> BuildNoOptimalizedLinqOnArrayFind(Commit[] arrayOfCommits)
        {
            List<Link> links = new List<Link>();
            List<Commit> inetrCommits = new List<Commit>();

            foreach (var currentCommit in arrayOfCommits)
            {
                var parents = Array.FindAll(arrayOfCommits, s => currentCommit.Parents.Contains(s.Sha));

                foreach (var parent in parents)
                {
                    CreateLinks(currentCommit, parent, links, inetrCommits);
                }
            }
            return links;
        }
    }
}