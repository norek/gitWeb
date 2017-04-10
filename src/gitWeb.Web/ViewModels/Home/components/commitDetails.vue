<template>
<div id="mainGraphDiv" class="panelBlade">
    <div class="rightPanelHeader"> <label>COMMIT DETAILS</label> </div>
    <div class="">
        <p>{{details.sha}}</p>
        <p>{{details.message}}</p>
        <p>{{details.author}}</p>
        <p>{{details.date}}</p>

    </div>
    <table>
        <thead>
        </thead>
        <tbody>
            <tr v-for="file in details.files" v-on:click="selectFile(file)" v-bind:class="selected(file)">
                <td class="column-icon">
                    <span class="glyphicon glyphicon-inbox" v-bind:class="fileStatusClass(file)">
                  </span>
                </td>
                <!-- <td class="column-icon">
                    <button v-on:click="stageFile(file)" class="icon-button">
                    <span class="glyphicon glyphicon-menu-down"></span>
                  </button>
                </td> -->
                <td>{{file.name}}</td>
                <td>
                    <div class="truncate">
                        {{file.path}}
                    </div>
                </td>
            </tr>
        </tbody>
    </table>
</div>
</template>

<script>
export default {
    data() {
        return {
            selectedFile: {}
        }
    },
    computed: {
        details() {
            return this.$store.state.commitArea.selectedCommitDetails;
        }
    },
    methods: {
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
        selected(file) {
            if (this.selectedFile.path == file.path) {
                return "selected";
            }
            return "";
        },
        selectFile: function(file) {
            this.selectedFile = file;
        }
    }
}
</script>

<style lang="scss" scoped>@import '../shared/variables.scss';

.truncate {
    width: 300px;
    white-space: nowrap;
    overflow: hidden;
    text-overflow: ellipsis;
}

// #listOfCommitChanges {
//     overflow-y: auto;
//
//     table {
//         width: 100%;
//         background-color: red;
//         border: none;
//         // display: block;
//         box-sizing: border-box;
//     }
//     thead {
//         background-color: #393E40;
//         padding: 2.5em;
//         text-align: left;
//     }
//     th {
//         padding: 1em;
//     }
//     td {
//         padding: 0.4em;
//         border: none;
//     }
//
// }
// #mainGraphDiv {
//     height: 50%;
// }
</style>
