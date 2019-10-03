import axios from 'axios'

const api = {
    get: async function (url, query) {
        var _res = null;
        await axios.get(url, { headers: {}, params: query })
            .then(async function (res) {
                console.log(res);
                _res = res.data;
            });
        return _res;
    },
    post: async function (url, data, query) {
        var _res = null;
        await axios.post(url, data, { headers: {}, params: query })
            .then(async function (res) {
                _res = res.data;
            });
        return _res;
    },
}
export default api;
