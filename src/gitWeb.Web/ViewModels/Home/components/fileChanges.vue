<template>
<div class="panelBlade">
    <div class="rightPanelHeader">
        <label>CHANGES DETAILS</label>
    </div>
    <div class="hunk" v-for="change in changes.hunks">
        <div class="hunkHeader">
            <label>{{change.header}}</label>
        </div>
        <table>
            <tbody>
                <tr v-for="line in change.lines" v-bind:class="{added:line.type == 1,removed:line.type == 2}">
                    <td>
                        <div>{{line.lineOrigNumber}}</div>
                    </td>
                    <td>
                        <div>{{line.lineNewNumber}}</div>
                    </td>
                    <td class="colContent">
                        <div>{{line.content}}</div>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</div>
</template>

<script>
import * as types from '../store/types'

export default {
    data() {
        return {
            isActive: true
        }
    },
    computed: {
        changes() {
            return this.$store.state.stagingArea.fileChanges;
        }
    }
}
</script>

<style lang="scss">@import '../shared/variables.scss';

.added {
    background-color: green;
}
.removed {
    background-color: darkred;
}

.hunk {
    margin: 0.4em;
    overflow: auto;
    overflow-y: hidden;
    background-color: #393E3F;
}

.hunkHeader {
    background-color: #323637;
    padding: 0.8em;
}

.colContent {
    width: 100%;
}

table,
td,
th {
    padding: 0.1em;
    border-collapse: collapse;
}

th {
    display: none;
}

td,
th {
    border-bottom: 0 solid;
    padding-right: 1.3em;

}
</style>
