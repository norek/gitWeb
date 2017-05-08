export function map_repository(repositoryPath) {
    return new Promise((resolve, reject) => {
        axios.post('/api/repository/map?path=' + repositoryPath)
            .then(function(response) {
                resolve(response);
            })
            .catch(function(error) {
                reject(error);
            });
    });
}

export function fetch_mapped_repository() {
    return new Promise((resolve, reject) => {
        axios.get('/api/repository/mapp')
            .then(function(response) {
                resolve(response.data);
            })
            .catch(function(error) {
                reject(error);
            });
    });
}

export function clone_repository(cloneParams) {
    return new Promise((resolve, reject) => {
        axios.post('/api/remote/clone', cloneParams)
            .then(function(response) {
                resolve();
            })
            .catch(function(error) {
                reject(error);
            });
    });
}
