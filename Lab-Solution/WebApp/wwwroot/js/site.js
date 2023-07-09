
var appClient = () => {
    console.log(JSON.parse(atob("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c")));
    const localStorage = window.localStorage;
    let tokenRequest = {};
    let cart = [];

    const METHOD = {
        GET: 'GET',
        POST: 'POST',
        PUT: 'PUT',
        DELETE: 'DELETE',
    };

    const BASE_DOMAIN = 'https://localhost:5000/api';

    const PATH = {
        AUTH: 'auth',
        CATEGORY: 'categories',
        ORDER: 'orders',
        PRODUCT: 'products',
        USER: 'users'
    };

    const ENDPOINT = {
        REGISTER: 'register',
    };

    const request = (url, method, payload) => {
        return {
            subscribe: (success, error) => {
                $.ajax({
                    url: url,
                    method: method,
                    data: payload,
                    success: success,
                    error: error
                });
            }
        }
    };

    ; (function () {
        cart = JSON.parse(localStorage.getItem("cart")) ?? [];
        tokenRequest = JSON.parse(localStorage.getItem("token")) ?? {};
    })()

    const initRestful = (path) => {
        return {
            getAll: () => {
                return request(`${BASE_DOMAIN}/${path}`, METHOD.GET);
            },

            getById: (id) => {
                return request(`${BASE_DOMAIN}/${path}/${id}`, METHOD.GET);
            },

            insert: (id, payload) => {
                return request(`${BASE_DOMAIN}/${path}/${id}`, METHOD.POST, payload);
            },

            update: (id, payload) => {
                return request(`${BASE_DOMAIN}/${path}/${id}`, METHOD.PUT, payload);
            },

            delete: (id) => {
                return request(`${BASE_DOMAIN}/${path}/${id}`, METHOD.DELETE);
            },
        }
    }

    const { insert, ...restApiUsers } = initRestful(PATH.USER);

    return {
        cart: {
            add2Cart: (item) => {
                cart.push(item);
            },
            getCart: () => {
                return cart;
            },
            removeItem: (indexItemCart) => {
                cart.splice(indexItemCart, 1);
            },
            clear: () => {
                cart = [];
            },
            save: () => {
                localStorage.setItem("cart", cart);
            },
        },
        auth: {
            register: (payload) => {
                return request(`${BASE_DOMAIN}/${PATH.AUTH}/${ENDPOINT.REGISTER}`, METHOD.POST, payload)
            },
        },

        categories: {
            ...initRestful(PATH.CATEGORY),
        },

        orders: {
            ...initRestful(PATH.ORDER),
        },

        products: {
            ...initRestful(PATH.PRODUCT),
        },

        users: restApiUsers,
    }
}

