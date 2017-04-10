<template>
<div class="panelBlade" v-if="isAnyCommitOnSatging">
    <div class="rightPanelHeader"> <label>COMMIT</label> </div>
    <form @submit.prevent="validateBeforeSubmit" class="commitPanel">
        <textarea name="commitMessage" v-model="commitMessage" v-validate="'required'"></textarea>
        <i v-show="errors.has('commitMessage')" class="fa fa-warning"></i>
        <span v-show="errors.has('commitMessage')" class="help is-danger">{{ errors.first('commitMessage') }}</span>
        <button type="submit">Commit</button>
    </form>
</div>
</template>

<script>
export default {
    data() {
        return {
            commitMessage: ''
        }
    },
    computed: {
        isAnyCommitOnSatging() {
            return this.$store.getters.stagedFiles.length > 0;
        }
    },
    methods: {
        validateBeforeSubmit() {
            this.$validator.validateAll().then(() => {
                this.$store.dispatch("COMMIT", this.commitMessage)
                            .then(() => {
                                this.commitMessage = '';
                            });
            }).catch((e) => {
                // eslint-disable-next-line
                console.log(e);
                alert('Correct them errors!');
            });
        }
    }
}
</script>


<style lang="scss">@import '../shared/variables.scss';

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
