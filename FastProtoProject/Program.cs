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
using BenchmarkDotNet.Running;
using gitWeb.Core.Features.Stage;
using Commit = gitWeb.Core.Features.Commit.Commit;

namespace FastProtoProject
{
    class Program
    {
        private static Stopwatch stopwatch = new Stopwatch();
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<LinkCollectionBenchmark>();
            //using (var repo = new Repository(@"C:\Projects\Own\git_test_repo"))
            //using (var repo = new Repository(@"C:\Projects\Taksonomia"))
            //{
            //    ////stopwatch.Start();
            //ICommitProvider provider = new CommitProvider(repo);
            //Commit[] commits = provider.GetAllFromHead().Take(200).ToArray();
            ////stopwatch.StopAndLog("GetCommits");

            //stopwatch.Start();
            //GraphBuilder sd = new GraphBuilder();
            //sd.Build(commits);
            //stopwatch.StopAndLog("Builder");


            //FileChangeProvider provider = new FileChangeProvider(repo);


            //LinkBuilder builder = new LinkBuilder();
            //var s = builder.Build(commits.ToDictionary((k) => k.Sha,v => v));
            //stopwatch.StopAndLog("links");
            //stopwatch.Start();

            //var s1 = builder.Build(commits);
            //stopwatch.StopAndLog("s1");


            //provider.DiscardAllChanges();

            //foreach (var commit in s)
            //{
            //    Console.WriteLine(commit);
            //}
            //}
            //Console.WriteLine("end");
            Console.ReadLine();
        }
    }

    public static class Extensions
    {
        public static void StopAndLog(this Stopwatch watch, string text)
        {
            watch.Stop();
            Console.WriteLine(text + " -- " + watch.ElapsedMilliseconds);
            watch.Reset();
        }
    }
}
