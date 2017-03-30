<template>
<div id="mainGraphDiv" class="panelBlade">
    <div class="rightPanelHeader"> <label>COMMIT DETAILS</label> </div>
    <p>{{details.sha}}</p>
    <p>{{details.message}}</p>
    <p>{{details.author}}</p>
    <p>{{details.date}}</p>
    <div id="listOfCommitChanges">
        <table>
            <tbody>
                <tr v-for="file in details.files">
                    <td>{{file.path}}</td>
                    <td>{{file.changeType}}</td>
                </tr>
            </tbody>
        </table>
    </div>
</div>
</template>

<script>
export default {
    computed: {
        details() {
            return this.$store.state.commitArea.selectedCommitDetails;
        }
    }
}
</script>

<style lang="scss" scoped>@import '../shared/variables.scss';

#listOfCommitChanges {
  overflow-y: auto;

    table {
        width: 100%;
        background-color: red;
        border: none;
        // display: block;
        box-sizing: border-box;
    }
    thead {
        background-color: #393E40;
        padding: 2.5em;
        text-align: left;
    }
    th {
        padding: 1em;
    }
    td {
        padding: 0.4em;
        border: none;
    }

}
#mainGraphDiv {
    height: 50%;
}
</style>
