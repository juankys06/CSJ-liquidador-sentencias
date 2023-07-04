Date.prototype.toFancyString = function () {
    var mm = (this.getMonth() + 1); // getMonth() is zero-based
    var dd = this.getDate();

    return [this.getFullYear(), mm > 9 ? '' + mm : '0' + mm, dd > 9 ? '' + dd : '0' + dd].join('-'); // padding
};

$(document).ready(function () {
    $.fn.dataTable.moment('D/M/YYYY'); //-- Para el formato de datatables con fechas
    var tasas = $("#t_tasas").DataTable({
        "scrollX": true,
        "info": false,
        "searching": false,
        "language": {
            "decimal": ",",
            "thousands": ".",
            "zeroRecords": "La tabla no contiene datos.",
            "infoEmpty": "Sin datos para mostrar."
        },
        "columns": [
            {
                "data": "idTasa", render: function (data, type, row, meta) {
                    return '<a href="/Admin/EditarTasa/' + data + '">' + data + '</a>';
                }
            },
            { "data": "tipoTasa" },
            {
                "data": "vigenteDesde", render: function (data, type, row, meta) {
                    return new Date(data).toLocaleDateString("es-CO", { year: 'numeric', month: '2-digit', day: '2-digit' }); 
                }
            },
            {
                "data": "vigenteHasta", render: function (data, type, row, meta) {
                    return new Date(data).toLocaleDateString("es-CO", { year: 'numeric', month: '2-digit', day: '2-digit' });
                }
            },
            { "data": "valorTasa", render: $.fn.dataTable.render.number('.', ',', 2, null) },
            { "data": "periodo" },
            { "data": "resVigencia" }
        ]
    });

    $("#f_tasas").ajaxForm({
        beforeSubmit: function (arr, $form, options) {
            var desde = new Date($("input[name=fromDate]").val());
            var hasta = new Date($("input[name=toDate]").val());

            if (desde > hasta) {
                alert("ERROR: Las fechas no son coherentes");
                return false;
            } else
                return true;
        },
        success: function (data, responseText, jqXHR) {
            if (jqXHR.status == 204)
                alert("No se encontraron registros");
            else {
                tasas.clear();
                tasas.rows.add(data).draw();
            }
        },
        error: function (jqXHR, exception, errorThrown) { alert(errorThrown); }
    });
});