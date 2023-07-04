$(document).ready(function () {
    var dialog = $("#dialog").dialog({
        autoOpen: false,
        //height: 400,
        width: 360,
        modal: true,
        buttons: {
            "Aceptar": function () {
                dialog.dialog("close");
            }
        }
    });

    var tbl_mov = $("#tbl_mov").DataTable({
        "paging": false,
        "ordering": false,
        "scrollCollapse": true,
        "info": false,
        "searching": false
    });

    var tbl_liquidacion = $("#tab-liquidacion table").DataTable({
        "paging": false,
        "scrollX": true,
        "scrollY": "40vh",
        "scrollCollapse": true,
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
                "data": "fechaPago", render: function (data, type, row, meta) {
                    return new Date(data).toLocaleDateString("es-CO", { year: 'numeric', month: '2-digit', day: '2-digit' });
                }
            },
            { "data": "numeroDias" },
            { "data": "pagosPesos", render: $.fn.dataTable.render.number('.', ',', 2, "$ ") },
            { "data": "segurosPesos", render: $.fn.dataTable.render.number('.', ',', 2, "$ ") },
            { "data": "moraPesos", render: $.fn.dataTable.render.number('.', ',', 2, "$ ") },           
            //{ "data": "interesPeriodoPesos", render: $.fn.dataTable.render.number('.', ',', 2) },
            { "data": null, "defaultContent": "0" },
            { "data": "tasaInteres", render: $.fn.dataTable.render.number('.', ',', 2) },
            { "data": "correccionMonetaria", render: $.fn.dataTable.render.number('.', ',', 2) },
            { "data": "abonoEfectivoPesos", render: $.fn.dataTable.render.number('.', ',', 2, "$ ") },
            { "data": "pagoUVR", render: $.fn.dataTable.render.number('.', ',', 2) },
            { "data": "tasaInteresUVR", render: $.fn.dataTable.render.number('.', ',', 4) },
            { "data": "interesPeriodoUVR", render: $.fn.dataTable.render.number('.', ',', 2) },
            { "data": "amortizacionUVR", render: $.fn.dataTable.render.number('.', ',', 2, null) },
            { "data": "saldoUVR", render: $.fn.dataTable.render.number('.', ',', 2, null) },
            { "data": "interesPeriodoPesos", render: $.fn.dataTable.render.number('.', ',', 2, "$ ") },
            { "data": "amortizacionPesos", render: $.fn.dataTable.render.number('.', ',', 2, "$ ") },
            { "data": "saldoPesos", render: $.fn.dataTable.render.number('.', ',', 2, "$ ") },
            { "data": "cotizacionUVR", render: $.fn.dataTable.render.number('.', ',', 2, "$ ") },
            { "data": "factor" }
        ]
    });

    var tbl_resumen = $("#tab-resumen table").DataTable({
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
            { "data": "item1" },
            { "data": "item2", render: $.fn.dataTable.render.number('.', ',', 2, "$ ") }
        ]
    });

    $("#btn_dialog").click(function () {
        var date_saldo = new Date($("input[name=f_sinicial]").val());

        if (eval(date_saldo) == "Invalid Date" || date_saldo > Date.parse("12/31/1999")) {
            alert("La fecha debe ser menor o igual al 31/12/1999.");
            $("input[name=f_sinicial]").focus();
            return false;
        }

        if (Number($("input[name=i_plazo]").val()) <= 0) {
            alert("Escriba la tasa con la cual inicio el crédito y luego introduzca las demás tasas");
            $("input[name=i_plazo]").focus();
            return false;
        }

        $('input[name="f_tasas[]"]:first').val($('input[name=f_sinicial]').val());
        $('input[name="tasas[]"]:first').val($('input[name=i_plazo]').val());

        dialog.dialog("open");
    });

    $("#frm_reliquidacion").ajaxForm({
        beforeSubmit: function (arr, $form, options) {
            var date_saldo = new Date($("input[name=f_sinicial]").val());
            if (date_saldo > Date.parse("12/31/1999") || date_saldo < Date.parse("1993")) {
                alert("La fecha debe ser menor o igual al 31/12/1999 y desde el 1993.");
                return false;
            }
        },
        success: function (data) {
            tbl_liquidacion.clear();
            tbl_liquidacion.rows.add(data.detalle).draw();
            tbl_resumen.clear();
            tbl_resumen.rows.add(data.resumen).draw();
            $('a[href="#tab-resumen"]').trigger("click");
        },
        dataType: "json",
        error: function (jqXHR) { alert(jqXHR.responseText); }
    });

    $('#guardar').click(function () {
        var formData = new FormData();
        formData.append('tipo', 95);

        $.ajax({
            beforeSend: function () {
                if (!$('#frm_reliquidacion')[0].checkValidity()) {
                    $('#frm_reliquidacion button[type=submit]').click();
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
            },
            error: function (jqXHR, exception, errorThrown) { alert(jqXHR.responseJSON.message); }
        });
    });


    $("#tbl_mov").on('focusout', 'input[name="ficticio[]"]:last', function () {
        tbl_mov.row.add(['<i class="fa fa-trash delete"></i>','<input type="date" name="f_movimiento[]" />', '<input type="number" class="no-numbar" step=".01" name="pago_movimiento[]" />', '<input type="number" class="no-numbar" step=".01" name="seguro_movimiento[]" />', '<input type="number" class="no-numbar" step=".01" name="mora_movimiento[]" />', '<input type="number" class="no-numbar" step=".01" name="otros_movimiento[]" />', '<input type="checkbox" name="ficticio[]" />']).draw();
        $('input[name="f_movimiento[]"]:last').focus();

    });
    $('#tbl_mov tbody').on('click', 'td i.delete', function () {
        tbl_mov.row($(this).parents('tr')).remove().draw();
    });

    $("#dialog table").on('focusout', 'input[name="tasas[]"]:last', function () {
        $("#dialog table tr:last").after('<tr><td><input type="date" form="frm_reliquidacion" name="f_tasas[]" /></td><td><input type="number" form="frm_reliquidacion" class="no-numbar" step=".01" name="tasas[]" /></td></tr>');
        $('input[name="f_tasas[]"]:last').focus();
    });
    //-- VALIDACIONES
    $('#tbl_mov').on('focusout','input[name="f_movimiento[]"]', function() {
        var inicio = new Date($('input[name=f_sinicial]').val());
        var fecha  = new Date(oDate.val());

        if(isNaN(inicio)) {
            $('input[name=f_sinicial]').focus();
            alert('Debe asignar la fecha de inicio.');
            return;
        }

        if(fecha < inicio || fecha > Date.parse("12/31/1999")) {
            oDate.focus();
            alert("LA FECHA DEBE ESTAR ENTRE\nLA FECHA DE SALDO Y EL 31/12/1999");
        }
    });
    //-- FIN VALIDACIONES
});