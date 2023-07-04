$(document).ready(function () {
    AutoNumeric.multiple('.no-numbar', autoNumericOptionsThousands); //- Se le da el formato a los inputs
    var tbl_indemnizacion = $("#tbl_indemnizacion").DataTable({
        "paging": false,
        "ordering": false,
        "info": false,
        "searching": false,
        "language": {
            "decimal": ",",
            "thousands": ".",
            "zeroRecords": "La tabla no contiene datos",
            "infoEmpty": "Sin datos para mostrar."
        },
        "columns": [
            {
                "data": "fechaInicial", render: function (data, type, row, meta) {
                    return new Date(data).toLocaleDateString("es-CO"); 
                }
            },
            {
                "data": "fechaFinal", render: function (data, type, row, meta) {
                    return new Date(data).toLocaleDateString("es-CO");
                }
            },
            { "data": "dias" },
            { "data": "capital" },
            { "data": "vrDia", render: $.fn.dataTable.render.number('.', ',', 2, null, ' $') },
            { "data": "totalMora", render: $.fn.dataTable.render.number('.', ',', 2, null, ' $') },
            { "data": "moraAcumulado", render: $.fn.dataTable.render.number('.', ',', 2, null, ' $') }
        ]
    });

    var tbl_indexacion = $("#tbl_indexacion").DataTable({
        "paging": false,
        "ordering": false,
        "info": false,
        "searching": false,
        "language": {
            "decimal": ",",
            "thousands": ".",
            "zeroRecords": "La tabla no contiene datos",
            "infoEmpty": "Sin datos para mostrar."
        },
        "columns": [
            {
                "data": "fechaInicial", render: function (data, type, row, meta) {
                    return new Date(data).toLocaleDateString("es-CO");
                }
            },
            {
                "data": "fechaFinal", render: function (data, type, row, meta) {
                    return new Date(data).toLocaleDateString("es-CO");
                }
            },
            { "data": "ipcInicial", render: $.fn.dataTable.render.number('.', ',', 2, null, ' $') },
            { "data": "ipcFinal", render: $.fn.dataTable.render.number('.', ',', 2, null, ' $') },
            { "data": "indexado", render: $.fn.dataTable.render.number('.', ',', 2, null, ' $') },
            { "data": "correccion", render: $.fn.dataTable.render.number('.', ',', 2, null, ' $') },
        ]
    });

    $("#frm_moratorio").ajaxForm({
        type: "post",
        success: function (data) {
            if (data != undefined) {
                if ($("select[name=liquidar] option:selected").val() === "indemnizacion") {
                    tbl_indemnizacion.clear();
                    tbl_indemnizacion.rows.add(data).draw();
                } else {
                    tbl_indexacion.clear();
                    tbl_indexacion.rows.add(data).draw();
                }
            } else
                alert("Error: error en la petición");
        },
        dataType: "json"
    });

    $("select[name=liquidar]").change(function () {
        if ($(this).val() === "indemnizacion") {
            $("#tbl_indexacion").addClass("oculto");
            $("#tbl_indemnizacion").removeClass("oculto");
        } else if ($(this).val() === "indexacion") {
            $("#tbl_indexacion").removeClass("oculto");
            $("#tbl_indemnizacion").addClass("oculto");
        }
    });

    $("input[name=fechaInicial]").focusout(function () {
        $.post("/Laboral/ObtenerTasaIPC", { fecha: $(this).val(), moneda: "IPC" }, function (data) {
            $("input[name=ipc_inicial").val(data);
        }, "json").fail(function (jqXHR, exception) {
            alert(jqXHR.responseText);
        });
    });

    $("input[name=fechaFinal]").focusout(function () {
        $.post("/Laboral/ObtenerTasaIPC", { fecha: $(this).val(), moneda: "IPC" }, function (data) {
            $("input[name=ipc_final").val(data);
        }, "json").fail(function (jqXHR, exception) {
            alert(jqXHR.responseText);
        });
    });
});