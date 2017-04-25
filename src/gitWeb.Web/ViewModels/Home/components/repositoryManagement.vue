<template>
<div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title" id="myModalLabel">Map repository</h4>
            </div>
            <div class="modal-body">
                <table class="table">
                    <!-- <caption>{{ path }}</caption> -->
                    <thead>
                        <tr>
                            <th>Name</th>
                            <!-- <th class="text-right"><button class="btn btn-default btn-xs" @click="goBack()" v-if="path !== '/'">Go Back</button></th> -->
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
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
            </div>
        </div>
    </div>
</div>
</template>


<script>
import * as explorerApi from '../api/explorerProvider';

export default {
    data() {
        return {
            selectedDirectory: {
                path: 'C:/',
                mapped: false
            }
        }
    },
    computed: {
        directoryList() {
            return this.$store.state.stagingArea.directoryList
        },
    },
    methods: {
        getDirectory(parentDirectory) {
            this.$store.dispatch('GET_DIRECTORIES', parentDirectory);
        },
        selectDirectory(directory) {
            if (!directory.mapped) {
                this.selectedDirectory = directory;
                this.getDirectory(directory.path);
            }
        }
    },
    destroyed() {
        this.$store.commit('ASSIGN_DIRECTORIES', []);
    },
    mounted() {
        this.$nextTick(function() {
            this.$store.dispatch('GET_DIRECTORIES', 'C:/');
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
