import Vue from 'vue';
import App from './App.vue';
import Vuex from 'vuex';
import store from './store/index.js'
import VeeValidate from 'vee-validate';
import VueNotifications from 'vue-notifications'

import miniToastr from 'mini-toastr'

const toastTypes = {
    success: 'success',
    error: 'error',
    info: 'info',
    warn: 'warn'
}

function toast({
    title,
    message,
    type,
    timeout,
    cb
}) {
    return miniToastr[type](message, title, timeout, cb)
}

const options = {
    success: toast,
    error: toast,
    info: toast,
    warn: toast
}
Vue.use(VueNotifications, options)

Vue.use(VeeValidate);

new Vue({
    store,
    el: '#app',
    render: h => h(App),
    mounted() {
        miniToastr.init();
    }
});
