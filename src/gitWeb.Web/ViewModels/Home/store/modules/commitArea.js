import * as commitApi from '../../api/commitApi';

function createEmptyCommit(){
  return {
      sha: "",
      message: "",
      author: "",
      date: ""
  }
}

const state = {
    selectedCommit : createEmptyCommit(),
    selectedCommitDetails : {},
    commitTree : []
}

const actions = {
    SET_CURRENT_COMMIT({commit} , selectedCommit){

      commit('setCurrentCommit',selectedCommit);
      commitApi.getCommitDetails(state.selectedCommit.sha)
               .then((r) =>
               commit('setCurrentCommitDetails',r));
    },
    GET_COMMIT_TREE_FROM_HEAD({commit}){
      return commitApi.getAllFromHead()
               .then((commitTree) => commit('SET_COMMIT_TREE',commitTree));
    },
    GET_COMMIT_TREE_FROM_TIP({commit},tipSha){
      return commitApi.getAllFromBranchTip(tipSha)
               .then((commitTree) => commit('SET_COMMIT_TREE',commitTree));
    }
}

const mutations = {
  setCurrentCommit(state,sc){
    state.selectedCommit = sc;
  },
  setCurrentCommitDetails(state,details){
    state.selectedCommitDetails = details;
  },
  SET_COMMIT_TREE(state,commitTree){
    state.commitTree = commitTree;
  },
  CLEAR_SELECTED_COMMIT(state){
    state.selectedCommit = createEmptyCommit();
    state.selectedCommitDetails = [];
  }
}

export default {
  state,
  mutations,
  actions
}
