<template>
<div class="panelBlade">
	<div class="rightPanelHeader">
		<label>actions.js</label>
	</div>
	<div class="hunk" v-for="change in changes.hunks">
		<div class="hunkHeader">
			<label>{{change.Header}}</label>
		</div>
		<table >
			<tbody>
				<tr v-for="line in change.Lines" v-bind:class="{added:line.Type == 1,removed:line.Type == 2}">
					<td>
						<div>{{line.LineOrigNumber}}</div>
					</td>
					<td>
						<div>{{line.LineNewNumber}}</div>
					</td>
					<td class="colContent">
						<div>{{line.Content}}</div>
					</td>
				</tr>
			</tbody>
		</table>
	</div>
</div>

</template>

<script>
    import * as types from '../store/types'

    export default {
        data(){
            return {isActive:true}
        },
        computed:{
            changes(){
                return this.$store.state.stagingArea.fileChanges;
            }
        },
        beforeMount() {
            this.$store.dispatch(types.FETCH_FILE_CHANGES);
        }
    }

</script>

<style lang="scss">
    @import '../shared/variables.scss';

.added{
    background-color: green;
}
.removed{
    background-color: darkred;
}

.hunk{
    margin: 0.4em;
    overflow: auto;
    overflow-y: hidden;
    background-color: #393E3F;
}

.hunkHeader{
        background-color: #323637;
        padding: 0.8em;
}

.colContent{
    width:100%;
}

table, th, td {
   padding: 0.1em;
    border-collapse: collapse;
}

th{
     display:none;
}

th, td {
     border-bottom: 0px solid;
     padding-right: 1.3em;
    
}

</style>


