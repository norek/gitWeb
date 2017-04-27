<template>
<div>
    <div>
        <button type="button" id="repomg" data-toggle="modal" data-target="#myModal">
          <span class="glyphicon glyphicon-home"></span>
        </button>
        <modal></modal>
    </div>

    <dropdown></dropdown>
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
    // position: absolute;
    // margin: 0.5em;
    // color: white;
    // background-color: $selected-row-color;
    border-style: none;
}

.dropdown {
    height: 35px;
    // float: right;
    // color: red;
    // position: absolute;
    // background: $selected-row-color;
}
</style>
