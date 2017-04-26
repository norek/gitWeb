<template>
<div>
    <button v-show="showBackButton" v-on:click="back()">back</button>
    <table class="table">
        <thead>
            <tr>
                <th>Name</th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="directory in directoryList" @click="selectDirectory(directory)">
                <td>
                    <div>
                        <i class="glyphicon glyphicon-folder-open"></i>
                        <label>{{ directory.path }}</label>
                    </div>
                </td>
                <td class="text-right" v-if="directory.mapped == false">
                    <span class="glyphicon glyphicon-bookmark"></span>
                </td>
            </tr>
        </tbody>
    </table>
    <div id="mapParams">
        <label>{{selectedDirectory.path}}</label>
        <button type="button" class="btn btn-success">MAP REPOSITORY</button>
    </div>
</div>
</template>

<script>
import * as explorerApi from '../api/explorerProvider';

const rootPath = 'C:/';

export default {
    data() {
        return {
            selectedDirectory: {
                path: rootPath,
                mapped: false
            },
            rootDirectory: rootPath
        }
    },
    computed: {
        directoryList() {
            return this.$store.state.stagingArea.directoryList;
        },
        showBackButton() {
            return this.rootDirectory != rootPath;
        }
    },
    methods: {
        getDirectory(parentDirectory) {
            this.$store.dispatch('GET_DIRECTORIES', parentDirectory);
            this.rootDirectory = parentDirectory;
        },
        selectDirectory(directory) {
            this.getDirectory(directory.path);
            this.selectedDirectory = directory;
        },
        back() {
            let lastSelectorIndex = this.rootDirectory.lastIndexOf('\\');
            let parentDirectory = '';

            if (lastSelectorIndex > 0) {
                parentDirectory = this.rootDirectory.substring(0, lastSelectorIndex);
            } else {
                parentDirectory = rootPath;
            }

            this.getDirectory(parentDirectory);
        }
    },
    destroyed() {
        this.$store.commit('ASSIGN_DIRECTORIES', []);
    },
    mounted() {
        this.$nextTick(function() {
            this.$store.dispatch('GET_DIRECTORIES', rootPath);
        }.bind(this));
    }
}
</script>

<style lang="scss" scoped>@import '../shared/variables.scss';

.table {
    color: $panel-header-color;
}
.glyphicon-folder-open {
    margin-right: 0.2em;
}
#mapParams button {
    float: right;
}
,
label {
    color: $panel-header-color;
}
</style>
