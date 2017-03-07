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
@import '../shared/variables.scss';

label {
  display: inline-block;
  /*width: 3em;*/
  margin: .5em 0;
  color: #607D8B;
  font-weight: bold;
}
.detailComponnet {
    /*background-color:lightgray*/
}
input {
  height: 2em;
  font-size: 1em;
  padding-left: .4em;
}
button {
  margin-top: 20px;
  font-family: Arial;
  background-color: #eee;
  border: none;
  padding: 5px 10px;
  border-radius: 4px;
  cursor: pointer; cursor: hand;
}
button:hover {
  background-color: #cfd8dc;
}
button:disabled {
  background-color: #eee;
  color: #ccc; 
  cursor: auto;
}

</style>