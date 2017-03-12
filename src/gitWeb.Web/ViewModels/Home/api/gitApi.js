export function stageFile(fileName) {
    return new Promise((resolve, reejct) => {
        console.log('start staging file ' + fileName);
        setTimeout(() => {
            console.log('staged file ' + fileName);
            resolve();
        }, 1000)
    }
    )
};

export function fetch_staged_files() {
    return new Promise((resolve, reejct) => {
        console.log('start fetching');
        setTimeout(() => {
            console.log('staged file fetched');
            resolve();
        }, 4000)
    }
    )
};