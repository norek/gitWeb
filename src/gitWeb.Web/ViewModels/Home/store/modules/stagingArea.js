import * as stagingAPI from '../../api/staging';
import * as types from '../types';

const state = {
    unstagedFiles: [],
    stagedFiles: [],
    fileChanges: []
}

const actions = {
    FETCH_UNSTAGED_FILES({ commit, state }) {
        stagingAPI.fetch_unstaged_files()
            .then((files) => commit(types.APPLY_UNSTAGED_FILES, files));
    },
    UNSTAGE_FILE({ dispatch }, file) {
        stagingAPI
            .unStageFile(file)
            .then(() => {
                dispatch(types.FETCH_UNSTAGED_FILES);
                dispatch(types.FETCH_STAGED_FILES);
            });
    },
    FETCH_STAGED_FILES({ commit, state }) {
        stagingAPI.fetch_staged_files()
            .then((files) => commit(types.APPLY_STAGED_FILES, files));
    },
    UNSTAGE_FILE({ dispatch }, file) {
        stagingAPI
            .unStageFile(file)
            .then(() => {
                dispatch(types.FETCH_UNSTAGED_FILES);
                dispatch(types.FETCH_STAGED_FILES);
            });
    },
    FETCH_FILE_CHANGES({ dispatch,commit }) {
        stagingAPI
            .getListOfHunks('file')
            .then((changes) => {
                commit('APPLY_FILE_CHANGES',changes);
            });
    }
}

const mutations = {
    APPLY_UNSTAGED_FILES(state, files) {
        console.log(files);
        state.unstagedFiles = files;
    },
    APPLY_STAGED_FILES(state, files) {
        console.log(files);
        state.stagedFiles = files;
    },
    APPLY_FILE_CHANGES(state,changes){
        console.log(changes);
        state.fileChanges = changes;
    }
}

export default {
    state,
    actions,
    mutations
}