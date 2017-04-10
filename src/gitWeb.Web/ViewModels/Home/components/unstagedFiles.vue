<template>
<div class="panelBlade">
    <div class="rightPanelHeader"> <label>UNSTAGED FILES</label> </div>
    <table>
        <thead>
        </thead>
        <tbody>
            <tr v-for="file in unstagedFiles" v-bind:class="fileStatusClass(file)">
                <td  class="column-icon">
                  <button v-on:click="stageFile(file)" class="icon-button">
                    <span class="glyphicon glyphicon-menu-down"></span>
                  </button>
                </td>
                <td>
                  <div>
                    {{file.filePath}}
                  </div>
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
        }
    },
    methods: {
        stageFile: function(file) {
            this.$store.dispatch(types.STAGE_FILE, file.filePath);
        },
        fileStatusClass(file){
          if(file.fileStatus == 1){
            return 'file-removed';
          }else if(file.fileStatus == 2){
            return 'file-modified';
          }else if(file.fileStatus == 3){
            return 'file-added';
          }else{
            return '';
          }
        }
    },
    beforeMount() {
        this.$store.dispatch(types.FETCH_REPOSITORY_STATUS);
    }
}
</script>

<style lang="scss">
  @import '../shared/variables.scss';

.file-modified{
    div{
      color: yellow;
    }

    td{
      background-color: green;
    }
}

.file-removed{
    div{
      color: yellow;
    }

    td{
      background-color: green;
    }
}

.file-added{
    div{
      color: yellow;
    }

    td{
      background-color: green;
    }
}

.file-conflicted{
    div{
      color: yellow;
    }

    td{
      background-color: green;
    }
}

</style>
