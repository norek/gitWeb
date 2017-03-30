export function fetch_repository_status() {
    return new Promise((resolve, reject) => {
        axios.get('/api/repository/status')
            .then(function(response) {
                console.log(response.data);
                resolve(response.data);
            })
            .catch(function(error) {
                console.log(error);
                reject(error);
            });
    });
}

export function stageFile(file) {
    return new Promise((resolve, reject) => {
       axios.post('/api/repository/stage',{file})
            .then(function(response) {
                console.log(response);
                resolve(response);
            })
            .catch(function(error) {
              console.log(error);

                reject();
            });
    });
}
export function unStageFile(file) {
    return new Promise((resolve, reject) => {
       axios.post('/api/repository/unstage',{file})
            .then(function(response) {
                console.log(response);
                resolve(response);
            })
            .catch(function(error) {
                console.log(error);
                reject();
            });
    })
}

export function getListOfHunks(fileName) {
    return new Promise((resolve, reject) => {
        axios.get('/api/FileChanges')
            .then(function(response) {
                console.log(response.data);
                resolve(response.data);
            })
            .catch(function(error) {
                reject();
            });
    })
}
