<template>
    <div class="panelBlade">
        <div class="rightPanelHeader"> <label>FILE CHANGES</label> </div>
        <table>
            <thead>
            </thead>
            <tbody>
                <!--<tr v-for="file in files">
                    <td>{{file.name}}</td>
                    <td>{{file.status}}</td>
                    <td><button v-on:click="stageFile(file)">+</button></td>
                </tr>-->
            </tbody>
        </table>
    </div>
</template>

<script>
import { mapState } from 'vuex'

    export default {

    }

</script>

<style lang="scss">
    @import '../shared/variables.scss';

</style>


