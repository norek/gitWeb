import * as stagingAPI from '../../api/staging';
import * as types from '../types';
import * as enums from '../../Enums/repositoryEnum';

const state = {
    unstagedFiles: [],
    stagedFiles: [],
    fileChanges: [],
    repositoryStatus: []
}

const getters = {
  unstagedFiles: state => {
    return state.repositoryStatus.filter(rs => !enums.IsStaged(rs.fileStatus));
  },
  stagedFiles: state => {
    return state.repositoryStatus.filter(rs => enums.IsStaged(rs.fileStatus));
  }
}

const actions = {
    UNSTAGE_FILE({ dispatch }, filePath) {
        stagingAPI
            .unStageFile(filePath)
            .then(() => {
                dispatch(types.FETCH_REPOSITORY_STATUS);
            });
    },

    FETCH_FILE_CHANGES({ dispatch,commit },filePath) {
        return stagingAPI
            .getListOfHunks(filePath)
            .then((changes) => {
                commit('APPLY_FILE_CHANGES',changes);
            });
    },
    STAGE_FILE({ dispatch }, filePath) {
        stagingAPI
            .stageFile(filePath)
            .then(() => {
                dispatch(types.FETCH_REPOSITORY_STATUS);
            });
    },
    FETCH_REPOSITORY_STATUS({ commit, state }) {
        stagingAPI.fetch_repository_status()
            .then((files) => commit(types.APPLY_REPOSITORY_STATUS, files));
    },
    DISCARD_ALL_CHANGES({dispatch}){
      stagingAPI.discardAllChanges()
                .then(() => dispatch('FETCH_REPOSITORY_STATUS'));
    },
    DISCARD_FILE_CHANGES({dispatch},filePath){
      stagingAPI.discardFileChanges(filePath)
                .then(() => dispatch('FETCH_REPOSITORY_STATUS'));
    }
}

const mutations = {
    APPLY_REPOSITORY_STATUS(state, files) {
        state.repositoryStatus = files;
    },
    APPLY_FILE_CHANGES(state,changes){
        state.fileChanges = changes;
    }
}

export default {
    state,
    actions,
    mutations,
    getters
}
