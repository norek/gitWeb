<template>
  <div class="panelBlade">
    <div class="rightPanelHeader"> <label>COMMIT</label> </div>
    <div class="commitPanel">
      <textarea></textarea>
      <button>Commit</button>
    </div>
  </div>
</template>

<script>
  export default {

  }

</script>


<style lang="scss">
  @import '../shared/variables.scss';


  .commitPanel {
    padding: 0.3em;
    color: white;
    font-size: 1.3em;
  }

  .commitPanel textarea {
    background-color: $panel-secondary-color;
    color: inherit;
    font-size: inherit;
    border: none;
    outline: none;
    resize: none;
    overflow: auto;
    padding: 0.2em;
    height: 75px;
    width: 100%;
    -webkit-box-sizing: border-box;
    -moz-box-sizing: border-box;
    box-sizing: border-box;
  }

  .commitPanel button {
    width: 100%;
    margin-top: 0.2;
    height: 35px;
    border: none;
    font-size: inherit;
    background-color: green;
    color: inherit;
  }
</style>