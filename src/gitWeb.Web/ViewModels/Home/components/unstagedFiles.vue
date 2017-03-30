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
                <td>
                  <!-- {{file.fileStatus}} -->
                    <span v-bind:class.="fileStatusIcon(file)" aria-hidden="true"></span>
                </td>

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
        },
        fileStatusIcon(file){

          if(file.fileStatus == enums.added_file){
            return "glyphicon glyphicon-align-left";
          }else if(file.fileStatus == enums.modified_file){
            return "glyphicon glyphicon-minus";
          }else if(file.fileStatus == enums.removed_file){
            return "glyphicon glyphicon-star";
          }else if(file.fileStatus == enums.conflicted_file){
            return "glyphicon glyphicon-saved";
          }else if(file.fileStatus == enums.staged_files){
            return "glyphicon glyphicon-link";
          }else{
            return "";
          }
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
