import axios from 'axios';

export function getAllbranches() {
    return new Promise((resolve, reject) => {

      axios.get('/api/branch')
           .then((r) => resolve(r.data))
           .catch((e) => reject(e));
    });
}
