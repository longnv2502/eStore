var appClient = () => {
    const localStorage = window.localStorage;
    let tokenRequest = {};
    let cart = [];

    const METHOD = {
        GET: 'GET',
        POST: 'POST',
        PUT: 'PUT',
        DELETE: 'DELETE',
    };

    const API_BASE_DOMAIN = 'https://localhost:5000/api';
    const WEB_BASE_DOMAIN = 'https://localhost:9000';

    const PATH = {
        AUTH: 'auth',
        CATEGORY: 'categories',
        ORDER: 'orders',
        PRODUCT: 'products',
        USER: 'users'
    };

    const ENDPOINT = {
        REGISTER: 'register',
        LOGIN: 'login',
    };

    const groupItemCarts = () => {
        const _cart = cart.reduce((acc, cur) => {
            console.log(acc);
            const findIndex = acc.findIndex(item => item.product.productId === cur.product.productId);
            if (findIndex !== -1) {
                acc[findIndex].quantity += cur.quantity;
                return acc;
            }
            acc.push(cur);
            return acc
        }, [])
        cart = _cart;
    }

    const request = (url, method, payload) => {
        if (method === METHOD.POST || method === METHOD.PUT) {
            payload = JSON.stringify(payload);
        }
        return {
            subscribe: (success, error) => {
                $.ajax({
                    url: url,
                    method: method,
                    headers: {
                        "Authorization": `Bearer ${tokenRequest}`
                    },
                    data: payload,
                    success: success,
                    contentType: "application/json",
                    error: error
                });
            }
        }
    };

    const parseJwt = () => {
        return JSON.parse(Buffer.from(tokenRequest.split('.')[1], 'base64').toString());
    };


    ; (function () {
        cart = JSON.parse(localStorage.getItem("cart")) ?? [];
        tokenRequest = localStorage.getItem("token") ?? '';
    })()

    const initRestful = (path) => {
        return {
            getAll: () => {
                return request(`${API_BASE_DOMAIN}/${path}`, METHOD.GET);
            },

            getById: (id) => {
                return request(`${API_BASE_DOMAIN}/${path}/${id}`, METHOD.GET);
            },

            insert: (payload) => {
                return request(`${API_BASE_DOMAIN}/${path}`, METHOD.POST, payload);
            },

            update: (id, payload) => {
                return request(`${API_BASE_DOMAIN}/${path}/${id}`, METHOD.PUT, payload);
            },

            delete: (id) => {
                return request(`${API_BASE_DOMAIN}/${path}/${id}`, METHOD.DELETE);
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
                localStorage.setItem("cart", JSON.stringify(cart));
            },
            groupItemCarts: () => {
                groupItemCarts();
                localStorage.setItem("cart", JSON.stringify(cart));
            },
        },
        auth: {
            login: (payload) => {
                return request(`${WEB_BASE_DOMAIN}/${PATH.AUTH}/${ENDPOINT.LOGIN}`, METHOD.POST, payload)
            },
            register: (payload) => {
                return request(`${API_BASE_DOMAIN}/${PATH.AUTH}/${ENDPOINT.REGISTER}`, METHOD.POST, payload)
            },
            saveToken: (token) => {
                localStorage.setItem("token", token);
                tokenRequest = token;
            },
            clearToken: () => {
                localStorage.removeItem("token");
                tokenRequest = '';
            },
            isAuthenticated: () => {
                return tokenRequest !== '';
            }
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

