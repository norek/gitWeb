using LibGit2Sharp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastProtoProject
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var repo = new Repository(@"C:\inPOS"))
            {
                var RFC2822Format = "ddd dd MMM HH:mm:ss yyyy K";

                int identity = 0;

                var labels  = repo.Commits.Take(50).Select(d => new Label(d.Sha, d.Message, d.Author.When.Date, d.Parents.Select(dd => dd.Sha).ToArray())).ToList();

                var jsonLabels = JsonConvert.SerializeObject(labels);

                //foreach (var label in labels)
                //{
                //    foreach (var pLabel in label.Parents)
                //    {
                //        var parent = labels.SingleOrDefault(d => d.Sha == pLabel);

                //        label.SetParents(parent);

                //        //if (parent != null)
                //        //{
                //        //    parent.AddChild(label);
                //        //}
                //    }
                //}


                var s = JsonConvert.SerializeObject(labels);


                foreach (var comm in labels)
                {
                    Console.WriteLine(comm);
                }


                //var w = JsonConvert.SerializeObject(links);
            }



            Console.ReadLine();
        }

       
        class Label
        {
            public override string ToString()
            {
                return $"{Sha}";
            }


            public Label(string id, string name, DateTime date,string[] shaParens)
            {
                Sha = id;
                Name = name;
                Date = date;
                Parents = shaParens;
                Childrens = new List<Label>();
                ParentsX = new List<Label>();
            }

            [JsonIgnore]
            public List<Label> Childrens { get; set; }
            [JsonIgnore]

            public List<Label> ParentsX { get; set; }

            public void AddChild(Label d)
            {
                Childrens.Add(d);
            }

            internal void SetParents(Label parent)
            {
                ParentsX.Add(parent);
            }

            public string[] Parents { get; set; }

            public string Sha { get; set; }
            public string Name { get; private set; }
            public DateTime Date { get; private set; }
        }
    }
}
