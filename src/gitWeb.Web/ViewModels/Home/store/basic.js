import Vue from 'vue'
import Vuex from 'vuex'
import { FETCH_UNSTAGED_FILES,FETCH_STAGED_FILES } from './types';
import {stageFile,fetch_staged_files} from '../api/gitApi';

Vue.use(Vuex);

export default new Vuex.Store({
    strict: process.env.NODE_ENV !== 'production',
    state: {
        count: 0,
        unstagedFiles: [{ name: "./shaxxxred/variables.scss", status: "1" }, { name: "./y1/variables.scss", status: "2" }, { name: "./shared/33.scss", status: "1" }, { name: "sdsdsd", status: "1" },]
        fileToPreview: {}
    },
    mutations: {
        [FETCH_UNSTAGED_FILES](state) {
            console.log("fetchSomeFiles mutation");
        },
        [FETCH_STAGED_FILES](state) {
            state.unstagedFiles.push({ name: "woot", status: "1" });
        },
        
    },
    actions: {
        stageFileAsync({ commit, state,dispatch }, fileName) {
            return dispatch('stageFile_POST').then(() => {
                dispatch('fetchStagedFiles').then(() => {
                    console.log('staged file completed');
                })
            })
        },
        stageFile_POST({ commit, state }, fileName) {
            return stageFile(fileName).then(() => commit(FETCH_UNSTAGED_FILES));
        },
        fetchStagedFiles({ commit, state }, fileName) {
            return fetch_staged_files().then(() => commit(FETCH_STAGED_FILES));
        }
    }
});