<template>
<div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title" id="myModalLabel">GITWEB SETTINGS</h4>
            </div>
            <div class="modal-body">
                <ul class="nav nav-tabs" id="tabContent">
                    <li class="active"><a href="#github-connect" data-toggle="tab">Github connect</a></li>
                    <li><a href="#local-mapping" data-toggle="tab">Local mapping</a></li>
                    <li><a href="#settings" data-toggle="tab">Settings</a></li>
                    <li><a href="#clone" data-toggle="tab">Clone</a></li>
                </ul>
                <div class="tab-content">
                    <div class="tab-pane active" id="github-connect">
                        <h2> github-connect</h2>
                    </div>
                    <div class="tab-pane" id="local-mapping">
                        <repositoryMapper></repositoryMapper>
                    </div>
                    <div class="tab-pane" id="clone">
                        <h2>Clone repository</h2>

                        <input name="repository_url" type="text" v-model="repositoryUrl" placeholder="Repository url" v-validate.initial="'required'">
                        <input name="repository_path" type="text" v-model="repositoryPath" placeholder="Repository path" v-validate.initial="'required'">
                        <button v-show="!errors.has('repository_path') && !errors.has('repository_url')" class="btn btn-primary" v-on:click="cloneRepository()"> Clone </button>

                    </div>
                    <div class="tab-pane" id="settings">
                        <h2>here are settings !</h2>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
            </div>
        </div>
    </div>
</div>
</template>


<script>
import repositoryMapper from './repositoryMapper.vue'


export default {
    data() {
        return {
            showModal: false,
            repositoryUrl: "",
            repositoryPath: "",
            loadervisible: false
        }
    },
    computed: {},
    methods: {
        cloneRepository() {
            this.loadervisible = true;
            this.$store.dispatch('CLONE_REPOSITORY', {
                'url': this.repositoryUrl,
                'path': this.repositoryPath
            }).
            then(() => this.loadervisible = false);
        }
    },
    components: {
        repositoryMapper

    }
}
</script>


<style lang="scss" scoped>@import '../shared/variables.scss';

.modal-content {
    background-color: $primary-back-color;
    border-radius: 0;

    button {
        border-radius: 0;
    }

}

input{
  color: black;
}
.active {}
</style>
