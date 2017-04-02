<template>
<div class="panelBlade">
    <div class="rightPanelHeader"> <label>BRANCHES</label> </div>
    <table>
        <thead>
        </thead>
        <tbody>
            <tr v-for="branch in branchList">
                <td>{{branch.name}}</td>
            </tr>
        </tbody>
    </table>
</div>
</template>

<script>
import * as types from '../store/types';

export default {
    computed: {
        branchList() {
            return this.$store.state.branchArea.branchList;
        }
    },
    beforeMount() {
        this.$store.dispatch(types.GET_ALL_BRANCHES);
    }
}
</script>

<style lang="scss">@import '../shared/variables.scss';</style>
