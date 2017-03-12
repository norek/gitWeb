import Vue from 'vue';
import App from './App.vue';
import Vuex from 'vuex';
import store from './store/basic.js'


const vm = new Vue({
    store,
    el: '#app',
    render: h => h(App)
});