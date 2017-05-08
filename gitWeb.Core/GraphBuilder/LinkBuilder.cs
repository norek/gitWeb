using System;
using System.Collections.Generic;
using System.Linq;
using gitWeb.Core.Features.Commit;

namespace gitWeb.Core.GraphBuilder
{
    public class LinkBuilder
    {
        public List<Link> Build(List<Commit> commits)
        {
            List<Link> links = new List<Link>(commits.Count);
            List<Commit> inetrCommits = new List<Commit>();

            for (int index = 0; index < commits.Count; index++)
            {
                var currentCommit = commits[index];
                var parents = currentCommit.Parents;

                for (int j = 0; j < parents.Count; j++)
                {
                    var currParent = parents[j];

                    for (int i = index; i < commits.Count; i++)
                    {
                        Commit parent = commits[i];

                        if (parent.Sha == currParent)
                        {
                            CreateLink(currentCommit, parent, links, inetrCommits);
                            break;
                        }
                    }
                }
            }

            return links;
        }

        private static void CreateLink(Commit currentCommit, Commit parent, List<Link> links, List<Commit> inetrCommits)
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