function Eliminar(IdUsuario, NombreUsuario) {
    $('#NombreUsuario')
        .text(NombreUsuario)
    $('#btnEliminarUsuario')
        .attr('href', 'Delete' + '?IdUsuario=' + IdUsuario)
}