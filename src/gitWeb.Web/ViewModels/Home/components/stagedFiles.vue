<template>
    <div class="panelBlade">
        <div class="rightPanelHeader"> <label>STAGED FILES</label> </div>
        <table>
            <thead>
            </thead>
            <tbody>
                <tr v-for="file in files">
                    <td>{{file.name}}</td>
                    <td>{{file.status}}</td>
                    <td><button>+</button></td>
                </tr>
            </tbody>
        </table>
    </div>
</template>

<script>
    export default {
        data() {

            return {
                files: []
            }
        }
    }

</script>

<style lang="scss" scoped>
    
</style>


