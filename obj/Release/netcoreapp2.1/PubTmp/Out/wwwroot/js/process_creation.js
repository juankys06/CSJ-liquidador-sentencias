$(document).ready(function () {
    $("#create_process").ajaxForm({
        //beforeSubmit: ,
        success: function (data) {
            var ventana = window.self;
            ventana.opener = window.self;
            ventana.close();
        },
        dataType: "json",
        error: function (jqXHR) {
            switch (jqXHR.status) {
                case 404:
                    alert(jqXHR.responseJSON.message);
                    break;
                default:
                    alert(jqXHR.responseJSON.innerException.Message);
                    break;
            }
        }
    });

    $("input[name=demandante_nombre]").focusout(function () {
        for (let i = 0; i < document.getElementById('lista_nombres').options.length; i++)
            if (document.getElementById('lista_nombres').options[i].value === this.value) {
                $("input[name=demandante_id]").val(document.getElementById('lista_identificacion').options[i].value);
                break;
            }
    });

    $("input[name=demandado_nombre]").focusout(function () {
        for (let i = 0; i < document.getElementById('lista_nombres').options.length; i++)
            if (document.getElementById('lista_nombres').options[i].value === $(this).val()) {
                $("input[name=demandado_id]").val(document.getElementById('lista_identificacion').options[i].value);
                break;
            }
    });

    $("input[name=demandante_id]").focusout(function () {
        for (let i = 0; i < document.getElementById('lista_identificacion').options.length; i++)
            if (document.getElementById('lista_identificacion').options[i].value === $(this).val()) {
                $("input[name=demandante_nombre]").val(document.getElementById('lista_nombres').options[i].value);
                break;
            }
    });

    $("input[name=demandado_id]").focusout(function () {
        for (let i = 0; i < document.getElementById('lista_identificacion').options.length; i++)
            if (document.getElementById('lista_identificacion').options[i].value === $(this).val()) {
                $("input[name=demandado_nombre]").val(document.getElementById('lista_nombres').options[i].value);
                break;
            }
    });
});