// app.vue
<template>
<div class="container-fluid">
    <nav class="navbar navbar-default panelBlade">
        <div class="container-fluid">
            <div class="navbar-header">
                <a class="navbar-brand" href="#">
                    <label>gitWeb</label>
                </a>
            </div>
        </div>
    </nav>
    <div class="row no-gutter">
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
            return selectedCommit !== undefined && selectedCommit.Sha != "";;
        }
    }
}
</script>

<style lang="scss">@import './shared/variables.scss';

$font-stack: Helvetica, sans-serif;

body {
    font: 100% $font-stack;
    color: $primary-color;
    background-color: $primary-back-color;
}
</style>
