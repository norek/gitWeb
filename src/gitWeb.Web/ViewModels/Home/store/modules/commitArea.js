import * as commitApi from '../../api/commitApi';

function createEmptyCommit(){
  return {
      Sha: "",
      message: "",
      author: "",
      date: ""
  }
}

const state = {
    selectedCommit : createEmptyCommit(),
    selectedCommitDetails : {}
}

const actions = {
    SET_CURRENT_COMMIT({commit} , selectedCommit){

      console.log(selectedCommit);
      commit('setCurrentCommit',selectedCommit);


      commitApi.getCommitDetails(state.selectedCommit.Sha)
               .then((r) =>
               commit('setCurrentCommitDetails',r));
    }
}

const mutations = {
  setCurrentCommit(state,sc){
    state.selectedCommit = sc;
  },
  setCurrentCommitDetails(state,details){
    state.selectedCommitDetails = details;
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
