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

export function load_mapped_repository(){
  return new Promise((resolve, reject) => {
      axios.get('/api/repository/')
          .then(function(response) {
              resolve(response.data);
          })
          .catch(function(error) {
              reject(error);
          });
  });
}
