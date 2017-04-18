using gitWeb.Core.Features.Commit;
using gitWeb.Core.GraphBuilder;
using LibGit2Sharp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Commit = gitWeb.Core.Features.Commit.Commit;

namespace FastProtoProject
{
    class Program
    {
        private static Stopwatch stopwatch = new Stopwatch();
        static void Main(string[] args)
        {

            //using (var repo = new Repository(@"C:\Projects\Own\git_test_repo"))
            using (var repo = new Repository(@"C:\Projects\Taksonomia"))
            {
                stopwatch.Start();
                ICommitProvider provider = new CommitProvider(repo);
                Commit[] commits = provider.GetAllFromHead().Take(400).ToArray();
                stopwatch.StopAndLog("GetCommits");

                stopwatch.Start();
                GraphBuilder sd = new GraphBuilder();
                sd.Build(commits);
                stopwatch.StopAndLog("Build");


                //foreach (var commit in commits)
                //{
                //    Console.WriteLine(commit);
                //}
            }

            Console.ReadLine();
        }
    }

    public static class Extensions
    {
        public static void StopAndLog(this Stopwatch watch,string text)
        {
            watch.Stop();
            Console.WriteLine(text + " -- " + watch.ElapsedMilliseconds);
            watch.Reset();
        }
    }
}
