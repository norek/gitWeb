import Vue from 'vue';
import App from './App.vue';
import Vuex from 'vuex';
import store from './store/index.js'


new Vue({
    store,
    el: '#app',
    render: h => h(App)
});