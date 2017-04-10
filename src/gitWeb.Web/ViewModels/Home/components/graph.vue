<template>
<div class="panelBlade">
    <div class="rightPanelHeader"> <label>GRAPH</label> </div>
    <svg class="graphgit">

    </svg>
</div>
</template>

<script>
import * as types from '../store/types'
import * as commitAPI from '../api/commitApi'
import * as graphBuilder from '../features/graphBuilder'
var store;
export default {
    mounted() {
        store = this.$store;
        this.$nextTick(function() {
            store.dispatch("GET_COMMIT_TREE_FROM_HEAD")
                .then(() => {
                    var elements = graphBuilder.build(this.$store.state.commitArea.commitTree);
                    graphBuilder.draw(elements.nodes, elements.links);
                    graphBuilder.onCommitClick(commitClick);
                });
        })
    }
}

function commitClick(commitInfo) {
    if (commitInfo.openCommit) {
        store.commit(types.CLEAR_SELECTED_COMMIT);
    } else {
        store.dispatch("SET_CURRENT_COMMIT", commitInfo)
    }
}
</script>

<style lang="scss">@import '../shared/variables.scss';
@import '../shared/graph.scss';</style>
