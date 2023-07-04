AutoNumeric.multiple('.no-numbar', autoNumericOptionsThousands); //- Se le da el formato a los inputs
$("#frm_indexacion").ajaxForm({
    beforeSubmit: function (arr, $form, options) {
        if ($("input[name=idProceso]").val() === "") {
            alert("ERROR: debe seleccionar un proceso.");
            return false;
        } else
            return true;
    },
    success: function (data) {
        $("#capital").val(data.vr);
        $("#ipc_inicial").val(data.ipc_inicial);
        $("#ipc_final").val(data.ipc_final);
    },
    dataType: "json",
    error: function (jqXHR, exception) { alert(jqXHR.responseJSON.message); }
});

$("#btn_guardar").click(function () {
    var formData = new FormData();
    formData.append('formulario', serializeToJSON($('#frm_indexacion')));
    formData.append('tipo', 96);

    return $.ajax({
        beforeSend: function () {
            if (!$('#frm_indexacion')[0].checkValidity()) {
                $('#frm_indexacion button[type=submit]').click();
                return false;
            }
        },
        contentType: false,
        url: "/Liquidador/Guardar",
        data: formData,
        processData: false,
        method: "post",
        success: function (data, textStatus, jqXHR) {
            alert(data.message);
            t_guardados.row.add(data.data).draw();
            //$('#guardados tr:last').after('<tr><td><a href="/Liquidador/Cargar?liquidacion='
            //    + data.data.id + '">'
            //    + data.data.llaveproc + '</a></td><td>'
            //    + data.data.tipo + '</td><td>'
            //    + new Date(data.data.fecha).toLocaleDateString("es-CO", { year: 'numeric', month: '2-digit', day: '2-digit' }) + '</td><td>'
            //    + data.data.usuario + '</td></tr>');
        },
        error: function (jqXHR, exception, errorThrown) {
            jqXHR.responseJSON.type ? alert(jqXHR.responseJSON.type.Message) : alert(jqXHR.responseJSON.message);
        }
    });
});

function deserializeToForm(JsonString) {
    var json = JSON.parse(JsonString);

    for (var element in json) {
        if (json.hasOwnProperty(element))
            if ($('input[name=' + element + ']').hasClass('no-numbar'))
                AutoNumeric.getAutoNumericElement('input[name=' + element + '].no-numbar').set(json[element]);
            else if ($('input[name=' + element + ']').prop('disabled')) {
                $('input[name=' + element + ']').prop('disabled', false);
                $('input[name=' + element + ']').val(json[element]);
            } else if ($('input[name=' + element + ']').attr('type') == 'checkbox')
                $('input[name=' + element + ']').prop('checked', true);
            else if ($('input[name=' + element + ']').attr('type') == 'radio')
                $('input[name=' + element + ']').val([json[element]]);
            else
                $('input[name=' + element + ']').val(json[element]);
    }
}

$('#guardados').on('click', 'a', function (e) {
    e.preventDefault();
    $.post(this.getAttribute('href'), function (data) {
        deserializeToForm(data);
        alert("Datos cargados con éxito.");
    });
});