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
    return state.repositoryStatus.filter(rs => rs.fileStatus != enums.staged_files);
  },
  stagedFiles: state => {
    return state.repositoryStatus.filter(rs => rs.fileStatus == enums.staged_files);
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
    }
}

const mutations = {
    APPLY_REPOSITORY_STATUS(state, files) {
        console.log(files);
        state.repositoryStatus = files;
    },
    APPLY_FILE_CHANGES(state,changes){
        console.log(changes);
        state.fileChanges = changes;
    }
}

export default {
    state,
    actions,
    mutations,
    getters
}
