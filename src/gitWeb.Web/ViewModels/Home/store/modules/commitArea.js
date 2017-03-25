import * as commitApi from '../../api/commitApi';

const state = {
    selectedCommit : {
        Sha: "",
        message: "",
        author: "",
        date: ""
    },
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
  }
}

export default {
  state,
  mutations,
  actions
}
