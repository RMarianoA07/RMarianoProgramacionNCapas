$(document).ready(function () {
    $('#ddlEstado').change(function () {
        $('#ddlMunicipio').empty().append('<option value="0">' + '-Municipio-' + '</option>'); //Vaciar el dropdown y agregar la opcion por default
        $('#ddlColonia').empty().append('<option value="0">' + '-Colonia-' + '</option>');
        if ($("#ddlEstado").val() > 0) {
            $.ajax({
                type: 'GET',
                url: 'MunicipioGetByIdEstado',
                dataType: 'json',
                data: { IdEstado: $("#ddlEstado").val() },
                success: function (municipios) {
                    $.each(municipios.Objects, function (i, municipio) { // iteramos los valores con foreach
                        $("#ddlMunicipio").append('<option value="'  // agregar las opciones que tengamos
                            + municipio.IdMunicipio + '">'
                            + municipio.Nombre + '</option>');
                    });
                },
                error: function (ex) {
                    alert('Failed.' + ex);
                }
            });
        }
        
    });
    $('#ddlMunicipio').change(function () {
        $('#ddlColonia').empty().append('<option value="0">' + '-Colonia-' + '</option>');
        $.ajax({
            type: 'GET',
            url: 'ColoniaGetByIdMunicipio',
            dataType: 'json',
            data: { IdMunicipio: $("#ddlMunicipio").val() },
            success: function (municipios) {
                $.each(municipios.Objects, function (i, municipio) { // iteramos los valores con foreach
                    $("#ddlColonia").append('<option value="'  // agregar las opciones que tengamos
                        + municipio.IdColonia + '">'
                        + municipio.Nombre + '</option>');
                });
            },
            error: function (ex) {
                alert('Failed.' + ex);
            }
        });
    });
});