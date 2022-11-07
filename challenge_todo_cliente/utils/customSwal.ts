import Swal from 'sweetalert2';

export const toast = Swal.mixin({
    toast: true,
    position: 'top-end',
    timer: 3000,
    showConfirmButton: false,
})

export const swal = Swal