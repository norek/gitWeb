import Vue from 'vue'
import Vuex from 'vuex'
import * as types from './types';
import * as actions from './actions'
import * as mutations from './mutations'
import * as stagingAPI from '../api/staging'

Vue.use(Vuex);

export default new Vuex.Store({
    strict: process.env.NODE_ENV !== 'production',
    actions,
    state: {
        count: 0,
        unstagedFiles: [],
        stagedFiles: []
    },
    mutations
});