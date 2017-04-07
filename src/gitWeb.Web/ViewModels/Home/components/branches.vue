<template>
<div class="panelBlade">
    <div class="rightPanelHeader">
        <label>BRANCHES</label>
        <button v-on:click="showBranchInput()" v-if="!canCreateNewBranch"> + </button>
        <button v-on:click="createBranch()" v-if="canCreateNewBranch"> OK </button>
        <button v-on:click="clearCreationForm()" v-if="canCreateNewBranch"> X </button>
        <input id="newBranch" type="text" v-model="branchName" v-if="canCreateNewBranch">
    </div>
    <table>
        <thead>
        </thead>
        <tbody>
            <tr v-for="branch in branchList" v-bind:class="{currentBranch:branch.name == currentBranchName}">
                <td>{{branch.name}}</td>
                <td><button v-on:click="checkout(branch)">C</button></td>
            </tr>
        </tbody>
    </table>
</div>
</template>

<script>
import * as types from '../store/types';

export default {
    data() {
        return {
            branchName: '',
            canCreateNewBranch: false
        }
    },
    computed: {
        branchList() {
            return this.$store.state.branchArea.branchList;
        },
        currentBranchName() {
          var currBranch = this.$store.getters.currentBranch;

          if(currBranch != undefined){
            return currBranch.name;
          }

          return "";
        }
    },
    methods: {
        createBranch: function() {
            this.$store.dispatch(types.CREATE_NEW_BRANCH, this.branchName)
                .then(() => this.clearCreationForm());
        },
        clearCreationForm: function() {
            this.branchName = '';
            this.canCreateNewBranch = false;
        },
        showBranchInput: function() {
            this.canCreateNewBranch = true;
        },
        checkout: function(branch) {
            this.$store.dispatch("CHECKOUT_BRANCH_All", branch.name);
        }
    },
    beforeMount() {
        this.$store.dispatch(types.GET_ALL_BRANCHES);
    }
}
</script>

<style lang="scss" scoped>@import '../shared/variables.scss';

#newBranch {
    color: black;
},
.currentBranch{
  background-color: pink;
  color: red;
}
</style>
