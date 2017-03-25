import axios from 'axios';

export function getCommitDetails(sha) {
    return new Promise((resolve, reject) => {

      axios.get('/api/commit/' + sha)
           .then((r) => resolve(r.data))
           .catch((e) => reject(e));
    });
}
