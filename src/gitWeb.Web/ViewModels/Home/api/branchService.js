import axios from 'axios';

export function getAllbranches() {
    return new Promise((resolve, reject) => {

        axios.get('/api/branch')
            .then((r) => resolve(r.data))
            .catch((e) => reject(e));
    });
}

export function getCurrentBranch(){
  return new Promise((resolve, reject) => {
      axios.get('/api/branch')
          .then((r) => resolve(r.data))
          .catch((e) => reject(e));
  });
}

export function checkoutBranch(branchName){
  return new Promise((resolve, reject) => {
      axios.post('/api/branch/' + branchName + '/checkout')
          .then((r) =>resolve(r.data));
          // .catch((e) => reject(e));
  });
}

export function createNewBranch(branchName) {
    return new Promise((resolve, reject) => {
        axios.post('/api/branch?branchName=' + branchName)
            .then((r) => resolve())
            .catch((e) => reject(e));
    });
}
