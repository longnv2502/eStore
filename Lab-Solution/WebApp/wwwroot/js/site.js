var appClient = () => {
    const METHOD = {
        GET = 'GET',
        POST = 'POST',
        PUT = 'PUT',
        DELETE = 'DELETE',
    };

    const BASE_DOMAIN = 'https://localhost:7013/api';

    const PATH = {
        PRODUCTS: 'products',
        ORDERS: 'orders',
    };

    const ENDPOINT = {
        GET_BY_ID: '',
        INSERT: 'Insert',
        UPDATE: 'Update',
        DELETE: 'Delete',
    };

    const request = (url, method, body) => {
        return {
            subscribe: (success, error) => {
                $.ajax({
                    url: url,
                    method: method,
                    data: body,
                    success: success,
                    error: error
                });
            }
        }
    };

    const initRestful = (path) => {
        return {
            getAll: () => {
                return request(`${BASE_DOMAIN}/${path}`, METHOD.GET);
            },

            getById: (id) => {
                return request(`${BASE_DOMAIN}/${path}/${ENDPOINT.GET_BY_ID}/${id}`, METHOD.GET);
            },

            insert: (id, body) => {
                return request(`${BASE_DOMAIN}/${path}/${ENDPOINT.INSERT}/${id}`, METHOD.POST, body);
            },

            update: (id, body) => {
                return request(`${BASE_DOMAIN}/${path}/${ENDPOINT.UPDATE}/${id}`, METHOD.PUT, body);
            },

            delete: (id) => {
                return request(`${BASE_DOMAIN}/${path}/${ENDPOINT.DELETE}/${id}`, METHOD.DELETE);
            },
        }
    }

    return {
        products: {
            ...initRestful(PATH.PRODUCTS),
        },

        orders: {
            ...initRestful(PATH.ORDERS),
        },
    }


}
