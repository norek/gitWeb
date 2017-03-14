// app.vue
<template>

   <div class="container-fluid">
      <nav class="navbar navbar-default">
        <div class="container-fluid">
          <div class="navbar-header">
            <a class="navbar-brand" href="#">
              <label>gitWeb</label>
            </a>
          </div>
        </div>
      </nav>
        <div class="row">
          <div class="col-md-8">
          <p> graph </P>
          </div>
          <div class="col-md-4">
            <div class="row">
              <unstagedFiles></unstagedFiles>
            </div>
            <div class="row">
              <stagedFiles></stagedFiles>
            </div>
            <div class="row">
              <commitForm></commitForm>
            </div>
            <div class="row">
              <fileChanges></fileChanges>
            </div>
          </div>
        </div>
      </div>
    </div>
</template>

<script>


import unstagedFiles from './components/unstagedFiles.vue'
import stagedFiles from './components/stagedFiles.vue'
import commitForm from './components/commitForm.vue'
import commitDetails from './components/commitDetails.vue'
import fileChanges from './components/fileChanges.vue'

export default {
  components: { 
    'unstagedFiles': unstagedFiles,
    'stagedFiles': stagedFiles, 
    'commitDetails': commitDetails, 
    'fileChanges': fileChanges, 
    'commitForm': commitForm },
  methods:{
  }}

</script>

<style lang="scss">
@import './shared/variables.scss';

$font-stack:    Helvetica, sans-serif;



body {
  font: 100% $font-stack;
  color: $primary-color;
  background-color:$primary-back-color;
}
</style>