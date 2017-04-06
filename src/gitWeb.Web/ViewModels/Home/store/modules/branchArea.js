import * as branchService from '../../api/branchService';

const branch = {
    name: "",
    isRemote: false
}

const state = {
    branchList: [],
    currentBranch: {}
}

const mutations = {
    SET_ALL_BRANCHES(state, branchList) {
        state.branchList = branchList;
    },
    SET_CURRENT_BRANCH(state, selectedBranch) {
        state.currentBranch = selectedBranch;
    }

}
const actions = {
        GET_ALL_BRANCHES({
            commit
        }) {
            branchService.getAllbranches()
                .then((b) => commit('SET_ALL_BRANCHES', b))
        },
        CHECKOUT_BRANCH({commit},branchName){
          branchService.checkoutBranch(branchName)
              .then((b) => commit('SET_CURRENT_BRANCH', b))
        },
        CREATE_NEW_BRANCH({dispatch,commit}, branchName) {
            return new Promise((resolve, reject) => {
                    branchService.createNewBranch(branchName)
                        .then((b) => {
                          dispatch('GET_ALL_BRANCHES')
                          dispatch('CHECKOUT_BRANCH', branchName)
                        })
                        .then(resolve());

                })
            }
        }

export default {
    state,
    mutations,
    actions
}
