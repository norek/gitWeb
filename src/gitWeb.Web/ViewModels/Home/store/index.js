import Vue from 'vue'
import Vuex from 'vuex'
import * as actions from './actions'
import * as mutations from './mutations'
import stagingArea from './modules/stagingArea'
import commitArea from './modules/commitArea'

Vue.use(Vuex);

export default new Vuex.Store({
    strict: process.env.NODE_ENV !== 'production',
    modules: {
        stagingArea,
        commitArea
    },
    actions,
    mutations
});
