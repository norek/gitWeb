<template>
<div class="dropdown">
    <button class="btn btn-primary dropdown-toggle" type="button" data-toggle="dropdown">{{currentRepositoryName}}
        <span class="caret"></span></button>
    <ul class="dropdown-menu">
        <li v-for="repository in mappedRepositories" v-on:click="mapRepository(repository)">
            <a>
                <span v-show="repository == currentRepositoryName" class="glyphicon glyphicon-record"></span> {{repository}}
            </a>
        </li>
    </ul>
</div>
</template>

<script>
export default {
    computed: {
        mappedRepositories() {
            return this.$store.state.repositoryArea.mappedRepositories;
        },
        currentRepositoryName() {
            if (this.$store.state.repositoryArea.currentRepository == '') {
                return 'Select repository';
            }

            return this.$store.state.repositoryArea.currentRepository;
        }
    },
    methods: {
        mapRepository(repository) {
            this.$store.dispatch('MAP_REPOSITORY', repository);
        }
    },
    mounted() {
        this.$store.dispatch('FETCH_MAPPED_REPOSITORIES');
    }
}
</script>

<style lang="scss" scoped>@import '../shared/variables.scss';

.dropdown {
    display: inline-block;
    height: 35px;
    margin: 0;
    button {
        border-radius: 0;
        color: white;
        background-color: $selected-row-color;
        border-style: none;
    }
}</style>
