import * as types from './types';
import * as stagingAPI from '../api/staging';

export const FETCH_STAGED_FILES = ({ commit, state }) => {
    return stagingAPI
        .fetch_staged_files()
        .then((files) => commit(types.FETCH_STAGED_FILES, files));
}
export const STAGE_FILE = ({ dispatch }, file) => {
    return stagingAPI
        .stageFile(file)
        .then(() => {
            dispatch(types.FETCH_UNSTAGED_FILES);
            dispatch(types.FETCH_STAGED_FILES);
        });
}
export const FETCH_UNSTAGED_FILES = ({ commit, state }) => {
    return stagingAPI.fetch_unstaged_files().then((files) => commit(types.FETCH_UNSTAGED_FILES, files));
}
export const UNSTAGE_FILE = ({ commit, state, dispatch }, file) => {
    return stagingAPI
        .unStageFile(file)
        .then(() => {
            dispatch(types.FETCH_UNSTAGED_FILES);
            dispatch(types.FETCH_STAGED_FILES);
        });
}
