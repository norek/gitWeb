<template>
    <div class="panelBlade">
        <div class="rightPanelHeader"> <label>UNSTAGED FILES</label> </div>
        <table>
            <thead>
            </thead>
            <tbody>
                <tr v-for="file in files">
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
                files: [{ name: "./shared/variables.scss", status: "1" }, { name: "./y1/variables.scss", status: "2" }, { name: "./shared/33.scss", status: "1" }, { name: "sdsdsd", status: "1" },]
            }
        },
        methods: {
            stageFile: function(file){
                alert(file.name);
            }
        }
    }

</script>

<style lang="scss">
@import '../shared/variables.scss';

</style>