<template>
<div class="panelBlade">
    <div class="rightPanelHeader">
        <label>BRANCHES</label>
        <button v-on:click="showBranchInput()" v-if="!canCreateNewBranch"
        data-toggle="tooltip" data-placement="left" title="Add new branch" class="icon-button">
          <span class="glyphicon glyphicon-plus"></span>
        </button>
        <button v-on:click="createBranch()" v-if="canCreateNewBranch" class="icon-button">
          <span class="glyphicon glyphicon-ok"></span>
        </button>
        <button v-on:click="clearCreationForm()" v-if="canCreateNewBranch" class="icon-button">
          <span class="glyphicon glyphicon-remove"></span>
        </button>
        <input id="newBranch" type="text" v-model="branchName" v-if="canCreateNewBranch">
    </div>
    <table>
        <thead>
        </thead>
        <tbody>
            <tr v-for="branch in branchList" v-bind:class="{selected:branch.name == currentBranchName}">
                <td>{{branch.name}}</td>
                <td class="column-icon">
                    <button class="icon-button" v-on:click="checkout(branch)" v-if="thisIsCurrentBranch(branch)"
                    data-toggle="tooltip" data-placement="left" title="Checkout">
                    <span class="glyphicon glyphicon-copyright-mark"></span>
                  </button>
                </td>
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

            if (currBranch != undefined) {
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
        thisIsCurrentBranch: function(branch){
          return branch.name != this.currentBranchName;
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
    background-color: $panel-secondary-color;
    color: inherit;
    font-size: inherit;
    border: none;
    outline: none;
    resize: none;
    overflow: auto;
    padding: 0.2em;
    width: 100%;
    height: 20px;
    -webkit-box-sizing: border-box;
    -moz-box-sizing: border-box;
    box-sizing: border-box;

}
#newBranch:focus {
    border-color: #094771;
    border-style: solid;
    border-width: thin;
}
button:focus {
    outline: 0;
}

.glyphicon-ok,
.glyphicon-plus {
    color: green;
}
.glyphicon-remove {
    color: red;
}

.icon-button {
    border: none;
    background-color: inherit;
}
</style>
