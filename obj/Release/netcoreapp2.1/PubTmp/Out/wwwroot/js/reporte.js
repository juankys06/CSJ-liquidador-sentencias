$(document).ready(function () {
    $.fn.dataTable.moment('D/M/YYYY'); //-- Para el formato de datatables con fechas
    var reportesL = $("#t_reporteL").DataTable({
        "scrollX": true,
        "info": false,
        "searching": true,
        "info": true,
        "language": {
            "decimal": ",",
            "thousands": ".",
            "zeroRecords": "La tabla no contiene datos.",
            "infoEmpty": "Sin datos para mostrar.",
            "oPaginate": {
                "sFirst": "Primero",
                "sLast": "Último",
                "sNext": "Siguiente",
                "sPrevious": "Anterior"
            },
            "lengthMenu": "Mostar _MENU_ Por paginas",
            "search": "Buscar:",
            "info": "Mostrando _TOTAL_ Liquidaciones",
        },
        "columns": [
            { "data": "id" },
            {
                "data": "fecha", render: function (data, type, row, meta) {
                    return new Date(data).toLocaleDateString("es-CO", { year: 'numeric', month: '2-digit', day: '2-digit' });
                }
            },
            { "data": "tipo.nombre", render: $.fn.dataTable.render.number('.', ',', 2, null) },
            { "data": "autor.email" }
        ]
    });
    var reportesDL = $("#t_reporteDL").DataTable({
        "scrollX": true,
        "info": false,
        "searching": true,
        "info": true,
        "language": {
            "decimal": ",",
            "thousands": ".",
            "zeroRecords": "La tabla no contiene datos.",
            "infoEmpty": "Sin datos para mostrar.",
            "oPaginate": {
                "sFirst": "Primero",
                "sLast": "Último",
                "sNext": "Siguiente",
                "sPrevious": "Anterior"
            },
            "lengthMenu": "Mostar _MENU_ Por paginas",
            "search": "Buscar:",
            "info": "Mostrando _TOTAL_ Liquidaciones",
        },
        "columns": [
            { "data": "id" },
            {
                "data": "fecha", render: function (data, type, row, meta) {
                    return new Date(data).toLocaleDateString("es-CO", { year: 'numeric', month: '2-digit', day: '2-digit' });
                }
            },
            { "data": "creador.email" }
        ]
    });

    var reportesU = $("#t_reporteU").DataTable({
        "scrollX": true,
        "info": false,
        "searching": true,
        "info": true,
        "language": {
            "decimal": ",",
            "thousands": ".",
            "zeroRecords": "La tabla no contiene datos.",
            "infoEmpty": "Sin datos para mostrar.",
            "oPaginate": {
                "sFirst": "Primero",
                "sLast": "Último",
                "sNext": "Siguiente",
                "sPrevious": "Anterior"
            },
            "lengthMenu": "Mostar _MENU_ Por paginas",
            "search": "Buscar:",
            "info": "Mostrando _TOTAL_ Usuarios",
        },
        "columns": [
            { "data": "id" },
            { "data": "email" },
            { "data": "fullName" },
            
        ]
    });

    var tipo;



    $("#f_reporte").ajaxForm({
        beforeSubmit: function (arr, $form, options) {
            
        },
        success: function (data, responseText, jqXHR) {
            if (jqXHR.status == 204)
                alert("No se encontraron registros");
            else {
               
                if (tipo == "Liquidaciones Realizadas") {
                    reportesL.clear();
                    reportesU.clear();
                    reportesL.rows.add(data).draw();
                    $("#reporteL").css("display","block");
                    $("#reporteDL").css("display", "none");
                    $("#reporteU").css("display", "none");
                }
                if (tipo == "Liquidaciones Guardadas") {
                    reportesDL.clear();
                    reportesDL.rows.add(data).draw();
                    $("#reporteL").css("display", "none");
                    $("#reporteDL").css("display", "block");
                    $("#reporteU").css("display","none");
                }
                if (tipo == "Usuarios registrados") {
                    reportesU.clear();
                    reportesU.rows.add(data).draw();
                    $("#reporteL").css("display", "none");
                    $("#reporteDL").css("display", "none");
                    $("#reporteU").css("display", "block");
                }

                
            }
        },
        error: function (jqXHR, exception, errorThrown) { alert(errorThrown); }
    });

    $('select#tipo').on('change', function () {
        var valor = $(this).val();
        tipo = valor;
        if (valor == "Usuarios registrados")
            $("#roles").show();
        else
            $("#roles").hide();
    });


});