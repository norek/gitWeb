const unStagedFiles = [{ name: "./shaxxxred/variables.scss", status: "1" }, { name: "./y1/variables.scss", status: "2" }, { name: "./shared/33.scss", status: "1" },
{ name: "sdsdsd", status: "1" },]

const stagedFiles = [];

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
            unStagedFiles.splice(0, 1);

            resolve();
        }, 1000)
    })
}
export function unStageFile(file) {
    return new Promise((resolve, reject) => {
        axios.get('/api/File')
            .then(function (response) {
                resolve(response.data);
            })
            .catch(function (error) {
                reject();
            });
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

export function getListOfHunks(fileName) {
    return new Promise((resolve, reject) => {
        axios.get('/api/FileChanges')
            .then(function (response) {
                console.log(response.data);
                resolve(response.data);
            })
            .catch(function (error) {
                reject();
            });
    })
}
