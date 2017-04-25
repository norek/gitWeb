<template>
<div>
    <button type="button" id="repomg" data-toggle="modal" data-target="#myModal">
        <span class="glyphicon glyphicon-home"></span>
  </button>
    <modal></modal>
</div>
</template>

<script>
import repositoryManagement from './repositoryManagement.vue'


export default {
    data() {
        return {
            showModal: false
        }
    },
    computed: {},
    methods: {},
    components: {
        'modal': repositoryManagement

    }
}
</script>

<style lang="scss" scoped>@import '../shared/variables.scss';

#repomg {
    height: 35px;
    width: 35px;
    position: absolute;
    margin: 0.5em;
    color: white;
    background-color: $selected-row-color;
    border-style: none;
}
</style>
