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

    export default {
        data() {
            return {
                loading : true
            }
        },
        computed:{
            unstagedFiles() {
                return this.$store.state.unstagedFiles
            }
        },
        methods: {
            stageFile: function(file){
                this.$store.dispatch("stageFileAsync",{file:file.name});
            }
        },
        beforeMount() {
            console.log("before mount");
        }
    }

</script>

<style lang="scss">
@import '../shared/variables.scss';

</style>