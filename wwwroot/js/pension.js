$(document).ready(function () {
    AutoNumeric.multiple('.no-numbar', autoNumericOptionsThousands); //- Se le da el formato a los inputs
    var tbl_cotizacion = $("#tbl_cotizacion").DataTable({
        "paging": false,
        "scrollY": "40vh",
        "ordering": false,
        //"order": [[0, "desc"]],
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
            { "data": "salario", render: $.fn.dataTable.render.number('.', ',', 2, '$ ') },
            { "data": "dias" },
            { "data": "valorDia", render: $.fn.dataTable.render.number('.', ',', 2, '$ ') },
            { "data": "basePeriodo", render: $.fn.dataTable.render.number('.', ',', 2, '$ ') }
        ]
    });

    $("#frm_cotizacion").ajaxForm({
        url: "/Laboral/Cotizaciones",
        type: "post",
        beforeSubmit: function () {
            var total = tbl_cotizacion.data().count();
            var evaluar = new Date($("input[name=fechaInicio]").val());
            var f_inicio = new Date(tbl_cotizacion.column(0).data()[0]);
            var f_final = new Date(tbl_cotizacion.column(0).data()[total - 1]);

            evaluar.setDate(evaluar.getDate() + 1);

            if (evaluar >= f_inicio &&
                evaluar <= f_final) {
                alert("ERROR: ya se registró un ingreso en ese mes, verifique.");
                return false;
            }
        },
        success: function (data) {
            tbl_cotizacion.rows.add(data).draw();
        },
        dataType: "json",
        error: function () { alert("ERROR en la comunicación"); }
    });

    $("#frm_pension").ajaxForm({
        beforeSubmit: function () {
            if (!tbl_cotizacion.data().any()) {
                alert("ERROR: Debe ingresar por lo menos un período de cotización, verifique.");
                $("input[name=fechaInicio]").focus();
                return false;
            }
            //-- Agregar label PROCESANDO
        },
        success: function (data) {

        },
        dataType: "json",
        error: function () { alert("ERROR en la comunicación."); }
    });

    $("input[name=f_nacimiento]").change(function () {
        var f_nacimiento = new Date($("input[name=f_nacimiento]").val());
        var f_liquidacion = new Date($("input[name=f_liquidacion]").val());

        var years_diff = f_liquidacion.getFullYear() - f_nacimiento.getFullYear();
        var months_diff = f_liquidacion.getMonth() - f_nacimiento.getMonth();

        if (!isNaN(f_liquidacion)) {
            $("#txt_años").text(years_diff === 1 ? "año" : "años");
            $("#txt_meses").text(months_diff === 1 ? "mes" : "meses");
            $("#año").text(years_diff);
            $("#meses").text(months_diff);
        }
    });

    $("input[name=f_liquidacion]").change(function () {
        var f_nacimiento = new Date($("input[name=f_nacimiento]").val());
        var f_liquidacion = new Date($("input[name=f_liquidacion]").val());

        var years_diff = f_liquidacion.getFullYear() - f_nacimiento.getFullYear();
        var months_diff = f_liquidacion.getMonth() - f_nacimiento.getMonth();

        if (!isNaN(f_nacimiento)) {
            $("#txt_años").text(years_diff === 1 ? "año" : "años");
            $("#txt_meses").text(months_diff === 1 ? "mes" : "meses");
            $("#año").text(years_diff);
            $("#meses").text(months_diff);
        }
    });
});