var sweetalertService = () => {
    const Toast = Swal.mixin({
        toast: true,
        position: 'top-end',
        showConfirmButton: false,
        timer: 3000,
        timerProgressBar: true,
        didOpen: (toast) => {
            toast.addEventListener('mouseenter', Swal.stopTimer)
            toast.addEventListener('mouseleave', Swal.resumeTimer)
        }
    });

    return {
        alert: {
            confirm(params) {
                const config = Object.assign({
                    icon: 'question',
                    showCloseButton: true,
                    showCancelButton: true,
                    confirmButtonText: 'Yes',
                    cancelButtonText: 'No',
                    customClass: {
                        confirmButton: 'btn btn-primary mx-2',
                        cancelButton: 'btn btn-secondary mx-2'
                    },
                    buttonsStyling: false,
                    reverseButtons: true,
                    width: 400
                }, params);
                return Swal.fire(config);
            },

            warning(params) {
                const config = Object.assign({
                    icon: 'warning',
                    showConfirmButton: true,
                    showCloseButton: true,
                    showCancelButton: false,
                    confirmButtonText: 'OK',
                    customClass: {
                        confirmButton: 'btn btn-primary',
                    },
                    buttonsStyling: false,
                    reverseButtons: true,
                    width: 400
                }, params);
                return Swal.fire(config);
            }
        },

        toast: {
            success: (title) => {
                return Toast.fire({
                    icon: 'success',
                    title: title
                });
            },

            error: (title) => {
                return Toast.fire({
                    icon: 'error',
                    title: title
                });
            },

            warning: (title) => {
                return Toast.fire({
                    icon: 'warning',
                    title: title
                });
            },

            info: (title) => {
                return Toast.fire({
                    icon: 'info',
                    title: title
                });
            },

            question: (title) => {
                return Toast.fire({
                    icon: 'question',
                    title: title
                });
            },
        }
    }
}