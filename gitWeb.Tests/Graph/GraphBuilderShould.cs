using gitWeb.Core.GraphBuilder;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibGit2Sharp;
using Xunit;
using Commit = gitWeb.Core.Features.Commit.Commit;

namespace gitWeb.Tests
{
    public class GraphBuilderShould
    {
        public int XIndexOfMainPath = 1;

        /// <summary>
        /// Tree
        /// *----*----*
        /// </summary>
        [Fact]
        public void ForEveryCommit_FromMainPath_AssignXIndex_Equals_One()
        {
            var commit3 = CreateNewCommit();
            var commit2 = CreateNewCommit(commit3.Sha);
            var commit1 = CreateNewCommit(commit2.Sha);

            var commitCollection = new Commit[3] { commit1, commit2, commit3 };

            GraphBuilder gb = new GraphBuilder();
            gb.Build(commitCollection);

            Assert.True(commitCollection.All(d => d.HIndex == XIndexOfMainPath));

        }

        /// <summary>
        /// Tree
        ///     /-* 
        /// *----*----*
        /// </summary>
        [Fact]
        public void CreateBranch_WithNextHIndexLevel()
        {
            var branchCommit = CreateNewCommit();
            var commit3 = CreateNewCommit();
            var commit2 = CreateNewCommit(commit3.Sha, branchCommit.Sha);
            var commit1 = CreateNewCommit(commit2.Sha);

            var commitCollection = new[] { commit1, commit2, commit3, branchCommit };

            GraphBuilder gb = new GraphBuilder();
            gb.Build(commitCollection);

            Assert.Equal(1, commit1.HIndex);
            Assert.Equal(1, commit2.HIndex);
            Assert.Equal(1, commit3.HIndex);
            Assert.Equal(2, branchCommit.HIndex);

        }

        /// <summary>
        /// Tree
        ///     /-* --*
        /// *----*----*
        /// </summary>
        [Fact]
        public void CreateBranch_WithNextHIndexLevel_AndParentOfNewBranchShouldBeSameLevelAsChild()
        {
            var parentOfBranchCommit = CreateNewCommit();
            var branchCommit = CreateNewCommit(parentOfBranchCommit.Sha);
            var commit3 = CreateNewCommit();
            var commit2 = CreateNewCommit(commit3.Sha, branchCommit.Sha);
            var commit1 = CreateNewCommit(commit2.Sha);

            var commitCollection = new[] { commit1, commit2, commit3, branchCommit, parentOfBranchCommit };

            GraphBuilder gb = new GraphBuilder();
            gb.Build(commitCollection);

            Assert.Equal(1, commit1.HIndex);
            Assert.Equal(1, commit2.HIndex);
            Assert.Equal(1, commit3.HIndex);
            Assert.Equal(2, branchCommit.HIndex);
            Assert.Equal(2, parentOfBranchCommit.HIndex);

        }


        /// <summary>
        /// Tree
        ///  /-b1-\/-b2---
        /// 4--3----2----
        /// </summary>
        [Fact]
        public void CreateBranch_SetSecondColumn_AfterMerge_CreateNewBranch_AndAlsoGiveSecondColumn()
        {

            var branchCommit = CreateNewCommit();
            var branchAfterMerge = CreateNewCommit();
            var commit1 = CreateNewCommit();
            var commit2 = CreateNewCommit(commit1.Sha, branchAfterMerge.Sha);
            var commit3 = CreateNewCommit(commit2.Sha, branchCommit.Sha);
            var commit4 = CreateNewCommit(commit3.Sha);

            branchCommit.Parents = new List<string>() { commit2.Sha };
            branchCommit.ParentsIds = new List<ObjectId>() { new ObjectId(commit2.Sha) };


            var commitCollection = new[] { commit4, commit3, commit2, branchCommit, commit1, branchAfterMerge };

            GraphBuilder gb = new GraphBuilder();
            gb.Build(commitCollection);


            Assert.Equal(1, commit1.HIndex);
            Assert.Equal(1, commit2.HIndex);
            Assert.Equal(1, commit3.HIndex);
            Assert.Equal(1, commit4.HIndex);
            Assert.Equal(2, branchCommit.HIndex);
            Assert.Equal(2, branchAfterMerge.HIndex);

        }

        /// <summary>
        /// Tree
        ///   /-b2
        ///  /-b1----
        /// 4--3----2----
        /// </summary>
        [Fact]
        public void AddThirdStage_WhenAreTwobranchesFromOneNode()
        {

            var branchCommit = CreateNewCommit();
            var secondBranch = CreateNewCommit();
            var commit1 = CreateNewCommit();
            var commit3 = CreateNewCommit(commit1.Sha, branchCommit.Sha, secondBranch.Sha);
            var commit4 = CreateNewCommit(commit3.Sha);

            var commitCollection = new[] { commit4, commit3, commit1, branchCommit, secondBranch };

            GraphBuilder gb = new GraphBuilder();
            gb.Build(commitCollection);


            Assert.Equal(1, commit1.HIndex);
            Assert.Equal(1, commit3.HIndex);
            Assert.Equal(1, commit4.HIndex);
            Assert.Equal(2, branchCommit.HIndex);
            Assert.Equal(3, secondBranch.HIndex);

        }


        public void WhenInMainPathIs_CommitWithTwoParents_SescondParentShouldHave_XIndexEqualsTwo()
        {
            var commit_FromOtherBranch = CreateNewCommit();

            var InitialCommit = CreateNewCommit();
            var secondCommit = CreateNewCommit(InitialCommit.Sha);
            var third = CreateNewCommit(secondCommit.Sha, commit_FromOtherBranch.Sha);


            var commitCollection = new[] { InitialCommit, secondCommit, third };

            GraphBuilder gb = new GraphBuilder();
            gb.Build(commitCollection);

            Assert.True(commitCollection.All(d => d.HIndex == XIndexOfMainPath));

        }

        private string ShaCreator()
        {
            string sha = string.Empty;

            for (int i = 0; i < 2; i++)
            {
                sha += Guid.NewGuid().ToString();
            }

            return sha.Replace("-", "").Substring(0, 40);
        }

        private Commit CreateNewCommit(params string[] parents)
        {
            var newSha = ShaCreator();
            var commit = new Commit
            {
                Sha = newSha,
                Id = new ObjectId(newSha),
                ParentsIds = parents.Select(p => new ObjectId(p)),
                Parents = parents
            };

            return commit;
        }
    }
}
