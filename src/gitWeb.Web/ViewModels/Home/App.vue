// app.vue
<template>
<div id="main">

    <div class="container-fluid">
        <nav class="navbar navbar-default panelBlade">
            <div class="container-fluid">
                <div class="navbar-header">
                    <a class="navbar-brand" href="#">
                        <label>gitWeb</label>
                    </a>
                </div>
                <ul class="nav nav-tabs">
                    <li class="active"><a href="#">Home</a></li>
                    <li><a href="#">Profile</a></li>
                    <li><a href="#">Messages</a></li>
                </ul> </div>
        </nav>
        <div class="row">
            <div class="col-md-2">
                <branches></branches>
                <tags></tags>
            </div>
            <div class="col-md-6">
                <graph></graph>
            </div>
            <div class="col-md-4">
                <div v-if="showCommitDetails">
                    <commitDetails></commitDetails>
                </div>
                <div v-else>
                    <unstagedFiles></unstagedFiles>
                    <stagedFiles></stagedFiles>
                    <commitForm></commitForm>
                    <fileChanges></fileChanges>
                </div>
            </div>
        </div>
        </div>
    </div>
</template>

<script>
import unstagedFiles from './components/unstagedFiles.vue'
import stagedFiles from './components/stagedFiles.vue'
import commitForm from './components/commitForm.vue'
import commitDetails from './components/commitDetails.vue'
import fileChanges from './components/fileChanges.vue'
import graph from './components/graph.vue'
import branches from './components/branches.vue'
import tags from './components/tags.vue'
import * as types from './store/types'

function checkForReporitoryStatusUpate(repoStatusFunction) {
    setInterval(repoStatusFunction, 10000)
}

export default {
    components: {
        'unstagedFiles': unstagedFiles,
        'stagedFiles': stagedFiles,
        'commitDetails': commitDetails,
        'fileChanges': fileChanges,
        'commitForm': commitForm,
        graph,
        branches,
        tags
    },
    computed: {
        showCommitDetails() {
            var selectedCommit = this.$store.state.commitArea.selectedCommit;
            return selectedCommit.message != "unCommitedChanges";
        }
    },
    beforeMount() {
      console.log("xxx");
        var fetchFunction = () => this.$store.dispatch(types.FETCH_REPOSITORY_STATUS);

        fetchFunction().then(() => checkForReporitoryStatusUpate(fetchFunction));

        this.$store.dispatch(types.GET_ALL_BRANCHES);
    }
}
</script>

<style lang="scss">@import './shared/variables.scss';

body {
    font: 100% $font-stack;
    color: $primary-color;
    background-color: $primary-back-color;
}
</style>
