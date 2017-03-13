<template>
    <div class="panelBlade">
        <div class="rightPanelHeader"> <label>STAGED FILES</label> </div>
        <table>
            <thead>
            </thead>
            <tbody>
                <tr v-for="file in stagedFiles">
                    <td>{{file.name}}</td>
                    <td>{{file.status}}</td>
                    <td><button v-on:click="unStageFile(file)">+</button></td>
                </tr>
            </tbody>
        </table>
    </div>
</template>

<script>

    import * as types from '../store/types'
    
    export default {
         computed:{
            stagedFiles() {
                return this.$store.state.stagedFiles
            }
        },
        methods: {
            unStageFile: function(file){
                this.$store.dispatch(types.UNSTAGE_FILE,{file:file});
            }
        },
        beforeMount() {
            console.log("before mount");
            this.$store.dispatch(types.FETCH_STAGED_FILES);
        }
    }

</script>

<style lang="scss" scoped>
    
</style>


