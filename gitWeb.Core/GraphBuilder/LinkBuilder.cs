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
    }
}