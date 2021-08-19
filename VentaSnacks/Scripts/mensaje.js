mensajeRedirect = function (titulo, texto, tipo, url) {
    Swal.fire({
        title: `${titulo}`,
        text: `${texto}`,
        type: `${tipo}`,
        confirmButtonText: 'Aceptar',
        allowEscapeKey: true,
        allowOutsideClick: false
    }).then(function () {
        window.location.href = `${url}`;
    });
}
mensaje = function (titulo, texto, tipo) {
    Swal.fire({
        title: `${titulo}`,
        text: `${texto}`,
        type: `${tipo}`,
        confirmButtonText: 'Aceptar',
        allowEscapeKey: true,
        allowOutsideClick: false
    });
}
