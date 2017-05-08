using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Running;
using gitWeb.Core.GraphBuilder;

namespace Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //var summary = BenchmarkRunner.Run<GraphBuilderBenchmark>();
                using (GraphBuilderBenchmark s = new GraphBuilderBenchmark())
                {
                    s.Run();
                }
                //var summary = BenchmarkRunner.Run<LinkBuilderBenchmark>();
                //LinkBuilderBenchmark s = new LinkBuilderBenchmark();
                //s.Build();
            }
            finally
            {
                //Console.ReadLine();
            }
        }
    }
}
