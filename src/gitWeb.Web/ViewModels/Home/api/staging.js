import axios from 'axios';

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
        axios.post('/api/repository/stage?filePath=' + file)
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
        axios.post('/api/repository/unstage?filePath=' + file)
            .then(function(response) {
                console.log(response);
                resolve(response);
            })
            .catch(function(error) {
                console.log(error);
                reject(error);
            });
    })
}

export function getListOfHunks(filePath) {
    return new Promise((resolve, reject) => {
        axios.get('/api/filechange?path=' + filePath)
            .then(function(response) {
                resolve(response.data);
            })
            .catch(function(error) {
                reject(error);
            });
    })
}

export function discardAllChanges(){
  return new Promise((resolve, reject) => {
      axios.get('/api/filechange/discardAll')
          .then(function(response) {
              resolve();
          })
          .catch(function(error) {
              reject(error);
          });
  })
}

export function discardFileChanges(filePath){
  return new Promise((resolve, reject) => {
      axios.get('/api/filechange/discard?filePath=' + filePath)
          .then(function(response) {
              resolve();
          })
          .catch(function(error) {
              reject(error);
          });
  })
}
