import * as types from './types';
import * as graphBuilder from '../features/graphBuilder';

export const CHECKOUT_BRANCH_All = ({dispatch,state}, branchName) => {

    dispatch(types.CHECKOUT_BRANCH, branchName)
        .then(() =>

        dispatch(types.GET_COMMIT_TREE_FROM_HEAD).then(() => {
            console.log("ddd");
            console.log(state.commitArea.commitTree);
          var elements = graphBuilder.build(state.commitArea.commitTree);
          graphBuilder.draw(elements.nodes, elements.links);

        }));
}
