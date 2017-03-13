export const  FETCH_UNSTAGED_FILES = (state, files) => {
    state.unstagedFiles = files;
}
export const FETCH_STAGED_FILES = (state, files) => {
    state.stagedFiles = files;
}