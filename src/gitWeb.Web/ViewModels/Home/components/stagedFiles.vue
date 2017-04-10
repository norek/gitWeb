<template>
<div class="panelBlade">
    <div class="rightPanelHeader"> <label>STAGED FILES</label> </div>
    <table>
        <thead>
        </thead>
        <tbody>
            <tr v-for="file in stagedFiles" v-on:click="selectFile(file)" v-bind:class="selected(file)">
                <td class="column-icon">
                    <span class="glyphicon glyphicon-inbox" v-bind:class="fileStatusClass(file)">
                  </span>
                </td>
                <td class="column-icon">
                    <button v-on:click="unStageFile(file)" class="icon-button">
                    <span class="glyphicon glyphicon-menu-up"></span>
                  </button>
                </td>
                <td>{{file.filePath}}</td>
            </tr>
        </tbody>
    </table>
</div>
</template>

<script>
import * as types from '../store/types'

export default {
    data() {
        return {
            selectedFile: {}
        }
    },
    computed: {
        stagedFiles() {
            return this.$store.getters.stagedFiles;
        }
    },
    methods: {
        unStageFile: function(file) {
            this.$store.dispatch(types.UNSTAGE_FILE, file.filePath);
        },
        selected(file) {
            if (this.selectedFile.filePath == file.filePath) {
                return "selected";
            }
            return "";
        },
        fileStatusClass(file) {
            if (file.fileStatus == 1) {
                return 'file-removed';
            } else if (file.fileStatus == 2) {
                return 'file-modified';
            } else if (file.fileStatus == 3) {
                return 'file-added';
            } else {
                return '';
            }
        },
        selectFile: function(file) {
            this.selectedFile = file;
            this.$store.dispatch(types.FETCH_FILE_CHANGES, file.filePath)
        }
    }
}
</script>

<style lang="scss">@import '../shared/variables.scss';

.file-modified {
    color: $file-modified_color;
}

.file-removed {
    color: $file-removed-color;
}

.file-added {
    color: $file-added-color;
}

.file-conflicted {
    color: $file-conflicted-color;
}
</style>
