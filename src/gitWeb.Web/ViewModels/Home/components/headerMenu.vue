<template>
<div>
    <div id="headerButtons">
        <button type="button" id="repomg" data-toggle="modal" data-target="#myModal">
          <span class="glyphicon glyphicon-cog"></span>
        </button>
        <dropdown id="dropdown"></dropdown>

    </div>

    <modal></modal>

</div>
</template>

<script>
import repositoryManagement from './repositoryManagement.vue';
import repositoryDropdown from './repositoryDropdown.vue';

export default {
    data() {
        return {
            showModal: false,
            visible: true
        }
    },
    methods: {},
    components: {
        'modal': repositoryManagement,
        'dropdown': repositoryDropdown
    }
}
</script>

<style lang="scss" scoped>@import '../shared/variables.scss';

#repomg {
    height: 35px;
    width: 35px;
    color: white;
    background-color: $selected-row-color;
    border-style: none;

    display: inline-block;
}

#headerButtons{
  display: inline-block;
}

</style>
