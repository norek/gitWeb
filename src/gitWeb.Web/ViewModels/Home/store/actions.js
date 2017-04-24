import * as types from './types';
import * as graphBuilder from '../features/graphBuilder';

export const CHECKOUT_BRANCH_All = ({dispatch,state}, branchName) => {
    dispatch(types.CHECKOUT_BRANCH, branchName)
        .then(() =>

        dispatch(types.GET_COMMIT_TREE_FROM_HEAD).then(() => {
          graphBuilder.draw(state.commitArea.commitTree, []);

        }));
}

export const COMMIT = ({dispatch,state,commit}, commitMessage) => {
  return dispatch("COMMIT_BASIC", commitMessage)
            .then(() => {
              commit("APPLY_FILE_CHANGES",[]);
              dispatch(types.GET_COMMIT_TREE_FROM_HEAD).then(() => {
                    graphBuilder.draw(state.commitArea.commitTree, []);
                    graphBuilder.onCommitClick(commitClick);
              });
              dispatch(types.FETCH_REPOSITORY_STATUS);

            },
            (err) => {console.log(err)})

            function commitClick(commitInfo) {
                if (commitInfo.message == "unCommitedChanges") {
                    commit(types.CLEAR_SELECTED_COMMIT);
                } else {
                    dispatch("SET_CURRENT_COMMIT", commitInfo)
                }
            }
}
