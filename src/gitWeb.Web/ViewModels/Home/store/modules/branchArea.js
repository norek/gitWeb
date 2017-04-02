import * as branchService from '../../api/branchService';

const branch = {
  name : "",
  isRemote : false
}

const state = {
  branchList : [],
  currentBranch : {}
}

const mutations = {
  SET_ALL_BRANCHES(state,branchList){
    state.branchList = branchList;
  },
  SET_CURRENT_BRANCH(state,selectedBranch){
    state.currentBranch = selectedBranch;
  }

}
const actions = {
  GET_ALL_BRANCHES({commit}){
    branchService.getAllbranches()
                 .then((b) => commit('SET_ALL_BRANCHES',b))
  }
}

export default{
  state,
  mutations,
  actions
}
