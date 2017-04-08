import axios from 'axios';

export function getCommitDetails(sha) {
    return new Promise((resolve, reject) => {

        axios.get('/api/commit/' + sha)
            .then((r) => resolve(r.data))
            .catch((e) => reject(e));
    });
}

export function getAllFromHead() {
    return new Promise((resolve, reject) => {
        axios.get('/api/commit/')
            .then((r) => resolve(r.data))
            .catch((e) => reject(e));
    });
}

export function getAllFromBranchTip(tipSha) {
    return new Promise((resolve, reject) => {
        axios.get('/api/commit/' + tipSha + '/tree')
            .then((r) => resolve(r.data))
            .catch((e) => reject(e));
    });
}

export function commit(commitMessage) {
    console.log("XX");
  console.log(commitMessage);
  console.log("XX");
    return new Promise((resolve, reject) => {
        axios.post('/api/commit/',{
                                    "message": commitMessage
                                  })
            .then((r) => resolve())
            .catch((e) => reject(e));
    });
}
