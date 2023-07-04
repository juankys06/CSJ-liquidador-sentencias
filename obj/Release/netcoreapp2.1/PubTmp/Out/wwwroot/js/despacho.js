$(document).ready(function () {
    //AutoNumeric.multiple('.no-numbar', autoNumericOptionsThousands); //- Se le da el formato a los inputs

    

    $("#departamento").on("change", function () {
        var departamento = $('#departamento').val().toString(); 
        var formData = new FormData();
        formData.append('departamentos', departamento);

        $.ajax({
            contentType: false,
            url: "/Admin/GetMunicipios",
            data: formData,
            processData: false,
            method: "post",
            success: function (data, textStatus, jqXHR) {
                if (data) {
                    $('#municipio').html('');
                    var options = '';
                    options += '<option value="">Seleccione un municipio...</option>';
                    for (var i = 0; i < data.length; i++) {
                        options += '<option value="' + data[i].codigo + '">' + data[i].nombre + '</option>';
                    }
                    $('#municipio').append(options);

                }  
            },
            error: function (jqXHR, exception, errorThrown) {
                jqXHR.responseJSON.type ? alert(jqXHR.responseJSON.type.Message) : alert(jqXHR.responseJSON.message);
            }
        });
    });

    $("#municipio,#entidad,#especialidad").on("change", function () {
        var departamento = $('#departamento').val().toString();
        var municipio = $('#municipio').val().toString();
        var entidad = $('#entidad').val().toString();
        var especialidad = $('#especialidad').val().toString();
        var formData = new FormData();
        formData.append('departamento', departamento);
        formData.append('municipio', municipio);
        formData.append('entidad', entidad);
        formData.append('especialidad', especialidad);

        $.ajax({
            contentType: false,
            url: "/Admin/GetDespachos",
            data: formData,
            processData: false,
            method: "post",
            success: function (data, textStatus, jqXHR) {
                if (data) {
                    $('#despacho').html('');
                    var options = '';
                    options += '<option value="">Seleccione un despacho...</option>';
                    for (var i = 0; i < data.length; i++) {
                        options += '<option value="' + data[i].codigo + '">' + data[i].nombre + '</option>';
                    }
                    $('#despacho').append(options);

                }
            },
            error: function (jqXHR, exception, errorThrown) {
                jqXHR.responseJSON.type ? alert(jqXHR.responseJSON.type.Message) : alert(jqXHR.responseJSON.message);
            }
        });
    });

});