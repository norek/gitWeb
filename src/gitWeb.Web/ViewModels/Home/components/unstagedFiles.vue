<template>
    <div class="panelBlade">
        <div class="rightPanelHeader"> <label>UNSTAGED FILES</label> </div>
        <table>
            <thead>
            </thead>
            <tbody>
                <tr v-for="file in unstagedFiles">
                    <td>{{file.name}}</td>
                    <td>{{file.status}}</td>
                    <td><button v-on:click="stageFile(file)">+</button></td>
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
                loading : true
            }
        },
        computed:{
            unstagedFiles() {
                console.log('sdxsd');
                return this.$store.state.stagingArea.unstagedFiles;
            }
        },
        methods: {
            stageFile: function(file){
                // console.log(file.name);
                // this.$store.dispatch(types.STAGE_FILE, file);
            }
        },
        beforeMount() {
            this.$store.dispatch(types.FETCH_UNSTAGED_FILES);
        }
    }

</script>

<style lang="scss">
@import '../shared/variables.scss';

</style>