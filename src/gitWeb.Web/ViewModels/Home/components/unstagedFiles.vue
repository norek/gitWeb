<template>
<div class="panelBlade">
    <div class="rightPanelHeader"> <label>UNSTAGED FILES</label> </div>
    <table>
        <thead>
        </thead>
        <tbody>
            <tr v-for="file in unstagedFiles">
                <td><button v-on:click="stageFile(file)">+</button></td>
                <td>{{file.filePath}}</td>
            </tr>
        </tbody>
    </table>
</div>
</template>

<script>
import * as types from '../store/types'
import * as enums from '../Enums/repositoryEnum'

export default {
    computed: {
        unstagedFiles() {
            return this.$store.getters.unstagedFiles;
        }
    },
    methods: {
        stageFile: function(file) {
            this.$store.dispatch(types.STAGE_FILE, file.filePath);
        }
    },
    beforeMount() {
        this.$store.dispatch(types.FETCH_REPOSITORY_STATUS);
    }
}
</script>

<style lang="scss">
  @import '../shared/variables.scss';


</style>
