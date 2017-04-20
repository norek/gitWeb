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
        private readonly CommitCreator _commitCreator = new CommitCreator();

        /// <summary>
        /// Tree
        /// *----*----*
        /// </summary>
        [Fact]
        public void ForEveryCommit_FromMainPath_AssignXIndex_Equals_One()
        {
            var commit3 = _commitCreator.CreateNewCommit();
            var commit2 = _commitCreator.CreateNewCommit(commit3.Sha);
            var commit1 = _commitCreator.CreateNewCommit(commit2.Sha);

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
            var branchCommit = _commitCreator.CreateNewCommit();
            var commit3 = _commitCreator.CreateNewCommit();
            var commit2 = _commitCreator.CreateNewCommit(commit3.Sha, branchCommit.Sha);
            var commit1 = _commitCreator.CreateNewCommit(commit2.Sha);

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
            var parentOfBranchCommit = _commitCreator.CreateNewCommit();
            var branchCommit = _commitCreator.CreateNewCommit(parentOfBranchCommit.Sha);
            var commit3 = _commitCreator.CreateNewCommit();
            var commit2 = _commitCreator.CreateNewCommit(commit3.Sha, branchCommit.Sha);
            var commit1 = _commitCreator.CreateNewCommit(commit2.Sha);

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

            var branchCommit = _commitCreator.CreateNewCommit();
            var branchAfterMerge = _commitCreator.CreateNewCommit();
            var commit1 = _commitCreator.CreateNewCommit();
            var commit2 = _commitCreator.CreateNewCommit(commit1.Sha, branchAfterMerge.Sha);
            var commit3 = _commitCreator.CreateNewCommit(commit2.Sha, branchCommit.Sha);
            var commit4 = _commitCreator.CreateNewCommit(commit3.Sha);

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

            var branchCommit = _commitCreator.CreateNewCommit();
            var secondBranch = _commitCreator.CreateNewCommit();
            var commit1 = _commitCreator.CreateNewCommit();
            var commit3 = _commitCreator.CreateNewCommit(commit1.Sha, branchCommit.Sha, secondBranch.Sha);
            var commit4 = _commitCreator.CreateNewCommit(commit3.Sha);

            var commitCollection = new[] { commit4, commit3, commit1, branchCommit, secondBranch };

            GraphBuilder gb = new GraphBuilder();
            gb.Build(commitCollection);


            Assert.Equal(1, commit1.HIndex);
            Assert.Equal(1, commit3.HIndex);
            Assert.Equal(1, commit4.HIndex);
            Assert.Equal(2, branchCommit.HIndex);
            Assert.Equal(3, secondBranch.HIndex);

        }

        /// <summary>
        ///  /*3--/*4---
        /// *1---*2--
        /// </summary>
        [Fact]
        public void
            WhenCommitHasTwoChilder_andFirstChildIsOnMainBranch_AndSeconIsOnSecond_BuilderShould_AssignSecondIndexOfCommitOnSecondBranchToCommit()
        {
            var mainCommit1_1 = _commitCreator.CreateNewCommit();
            var mainCommit2_2 = _commitCreator.CreateNewCommit();
            var mainCommit2_3 = _commitCreator.CreateNewCommit();

            var branchFromMainCommit1_3 = _commitCreator.CreateNewCommit();
            var branchFromMainCommit2AndParentOf_4 = _commitCreator.CreateNewCommit();

            mainCommit1_1.Parents.Add(mainCommit2_2.Sha);
            mainCommit1_1.Parents.Add(branchFromMainCommit1_3.Sha);


            branchFromMainCommit1_3.Parents.Add(branchFromMainCommit2AndParentOf_4.Sha);
            mainCommit2_2.Parents.Add(mainCommit2_3.Sha);
            mainCommit2_2.Parents.Add(branchFromMainCommit2AndParentOf_4.Sha);

            Commit[] commits = new Commit[] { mainCommit1_1 , branchFromMainCommit1_3, mainCommit2_2, branchFromMainCommit2AndParentOf_4, mainCommit2_3 };

            GraphBuilder vrlBuilder = new GraphBuilder();
            vrlBuilder.Build(commits);

            Assert.Equal(1, mainCommit1_1.HIndex);
            Assert.Equal(1, mainCommit2_2.HIndex);
            Assert.Equal(1, mainCommit2_3.HIndex);
            Assert.Equal(2, branchFromMainCommit1_3.HIndex);
            Assert.Equal(2,branchFromMainCommit2AndParentOf_4.HIndex);
        }


        public void WhenInMainPathIs_CommitWithTwoParents_SescondParentShouldHave_XIndexEqualsTwo()
        {
            var commit_FromOtherBranch = _commitCreator.CreateNewCommit();

            var InitialCommit = _commitCreator.CreateNewCommit();
            var secondCommit = _commitCreator.CreateNewCommit(InitialCommit.Sha);
            var third = _commitCreator.CreateNewCommit(secondCommit.Sha, commit_FromOtherBranch.Sha);


            var commitCollection = new[] { InitialCommit, secondCommit, third };

            GraphBuilder gb = new GraphBuilder();
            gb.Build(commitCollection);

            Assert.True(commitCollection.All(d => d.HIndex == XIndexOfMainPath));

        }
    }
}
