<template>
<div class="panelBlade">
    <div class="rightPanelHeader"> <label>STAGED FILES</label> </div>
    <table>
        <thead>
        </thead>
        <tbody>
            <tr v-for="file in stagedFiles">
                <td><button v-on:click="unStageFile(file)">+</button></td>
                <td>{{file.filePath}}</td>
                <td>{{file.fileStatus}}</td>
            </tr>
        </tbody>
    </table>
</div>
</template>

<script>
import * as types from '../store/types'

export default {
    computed: {
        stagedFiles() {
            return this.$store.getters.stagedFiles;
        }
    },
    methods: {
        unStageFile: function(file) {
            this.$store.dispatch(types.UNSTAGE_FILE, file.filePath);
        }
    }
}
</script>

<style lang="scss">@import '../shared/variables.scss';</style>
