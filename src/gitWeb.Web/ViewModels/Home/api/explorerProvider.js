
export function get_directory_from_path(rootPth) {
  return new Promise((resolve, reject) => {
      axios.get('/api/repository/directory?path=' + rootPth)
          .then(function(response) {
              resolve(response.data);
          })
          .catch(function(error) {
              console.log(error);
              reject();
          });
  });
}
