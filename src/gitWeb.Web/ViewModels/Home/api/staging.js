let unStagedFiles = [{ name: "./shaxxxred/variables.scss", status: "1" }, { name: "./y1/variables.scss", status: "2" }, { name: "./shared/33.scss", status: "1" }, { name: "sdsdsd", status: "1" },]
let unStagedFiles2 = [{ name: "./shaxxxred/variables.scss", status: "1" }]

let stagedFiles = [];
let x = true;
export function fetch_staged_files() {
    return new Promise((resolve, reject) => {
        console.log('start fetching');
        setTimeout(() => {
            console.log('staged file fetched');
            resolve(stagedFiles);
        }, 1300)
    })
}

export function stageFile(file) {
    return new Promise((resolve, reject) => {
        console.log('start staging file ' + file);
        console.log(file);
        setTimeout(() => {
            console.log('staged file ' + file.name);

            stagedFiles = stagedFiles.slice();
            stagedFiles.push({ name: file.name, status: file.status });
            unStagedFiles = unStagedFiles.slice();
            unStagedFiles.splice(0,1);

            resolve();
        }, 1000)
    })
}
export function unStageFile(file) {
    return new Promise((resolve, reject) => {
        console.log('start unstaging file ' + file.name);
        setTimeout(() => {
            console.log('unstaged file ' + file.name);
            // unStagedFiles2.push(file);
            // stagedFiles.splice(0, 1);
            resolve();
        }, 1000)
    })
}
export function fetch_unstaged_files() {
    return new Promise((resolve, reject) => {
        console.log('start fetching');
        setTimeout(() => {
            console.log('unstaged file fetched');
            resolve(unStagedFiles);
        }, 1000)
    }
    )
}
