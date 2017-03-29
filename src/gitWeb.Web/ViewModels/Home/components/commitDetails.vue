<template>
<div class="panelBlade">
    <div class="rightPanelHeader"> <label>COMMIT DETAILS</label> </div>
    <p>{{details.sha}}</p>
    <p>{{details.message}}</p>
    <p>{{details.author}}</p>
    <p>{{details.date}}</p>
    <table>
        <thead>
        </thead>
        <tbody>
            <tr v-for="file in details.files">
                <td>{{file.path}}</td>
                <td>{{file.changeType}}</td>
            </tr>
        </tbody>
    </table>
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

<style lang="scss" scoped>
  @import '../shared/variables.scss';
</style>
