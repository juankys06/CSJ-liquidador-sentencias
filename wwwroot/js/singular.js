// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {

    AutoNumeric.multiple('.no-numbar', autoNumericOptionsThousands); //- Se le da el formato a los inputs

    function deserializeToForm(JsonString){
        var json = JSON.parse(JsonString);
        t_abonos.clear().draw();
        for (var element in json) {
            if (json.hasOwnProperty(element))
                if (Array.isArray(json[element])) {
                    let fields_to_create = json[element].length;
                    let field_name = element.concat('[]');

                    if (element === "f_abono")
                        for (let i = 0; i < fields_to_create; i++) {
                            t_abonos.row.add(['<i class="fa fa-trash delete"></i>','<input type="date" max="2100-01-01" min="1900-01-01" name="f_abono[]" />', '<input type="text" class="no-numbar" name="abono[]" />']).draw();
                            new AutoNumeric('#t_abonos table tr:last-child td:last-child input', autoNumericOptionsThousands);
                        }
                    else if (element === "f_capitales")
                        for (let i = 0; i < fields_to_create; i++) {
                            t_capitales.row.add(['<i class="fa fa-trash delete"></i>','<input type="date" max="2100-01-01" min="1900-01-01" name="f_capitales[]" />', '<input type="text" class="no-numbar" name="capitales[]" />']).draw();
                            new AutoNumeric('#t_capitales table tr:last-child td:last-child input', autoNumericOptionsThousands);
                        }

                    $('input[name="' + field_name + '"]').each(function (index, obj) {
                        $(this).hasClass('no-numbar') ?
                            AutoNumeric.getAutoNumericElement(obj).set(json[element][index]) :
                            $(this).val(json[element][index]);
                    });

                } else {
                    if ($('textarea[name=' + element + ']').attr('rows') == '5') {
                        $('textarea[name=' + element + ']').val(json[element]);
                    } else if ($('input[name=' + element + ']').hasClass('no-numbar'))
                        AutoNumeric.getAutoNumericElement('input[name=' + element + '].no-numbar').set(json[element]);
                    else if ($('input[name=' + element + ']').prop('disabled')) {
                        $('input[name=' + element + ']').prop('disabled', false);
                        $('input[name=' + element + ']').val(json[element]);
                    } else if ($('input[name=' + element + ']').attr('type') == 'checkbox')
                        $('input[name=' + element + ']').prop('checked', true);
                    else if ($('input[name=' + element + ']').attr('type') == 'radio')
                        $('input[name=' + element + ']').val([json[element]]);
                    else if (element === "f_abono") {
                        t_abonos.row.add(['<i class="fa fa-trash delete"></i>', '<input type="date" max="2100-01-01" min="1900-01-01" name="f_abono[]" />', '<input type="text" class="no-numbar" name="abono[]" />']).draw();
                        new AutoNumeric('#t_abonos table tr:last-child td:last-child input', autoNumericOptionsThousands);
                    } else
                        $('input[name=' + element + ']').val(json[element]);

                    let field_name = element.concat('[]');
                    $('input[name="' + field_name + '"]').hasClass('no-numbar') ?
                        AutoNumeric.getAutoNumericElement('input[name="' + field_name + '"]').set(json[element]) :
                        $('input[name="' + field_name + '"]').val(json[element]);
                    
                }
        }
    }

    function guardar(savePdf) {
        var formData = new FormData();
        formData.append('formulario', serializeToJSON($('#liq_singular')));
        formData.append('tipo', 90);

        return $.ajax({
            beforeSend: function () {
                if (!$('#liq_singular')[0].checkValidity()) {
                    $('#liq_singular button[type=submit]').click();
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
                typeof savePdf === 'function' && savePdf();
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
    }

    function AutoGuardar(savePdf) {
        var formData = new FormData();
        formData.append('formulario', serializeToJSON($('#liq_singular')));
        formData.append('idProceso', $("input[name=idProceso]").val()); 
        formData.append('tipo', 90);

        return $.ajax({
            beforeSend: function () {
                if (!$('#liq_singular')[0].checkValidity()) {
                    $('#liq_singular button[type=submit]').click();
                    return false;
                }
            },
            contentType: false,
            url: "/Liquidador/AutoGuardar",
            data: formData,
            processData: false,
            method: "post",
            success: function (data, textStatus, jqXHR) {
                $("#AlertSuccess").show();
                $("#AlertSuccess").delay(5000).fadeOut();
                $("#AlertFailed").hide();
               // t_guardados.row.add(data.data).draw();
                typeof savePdf === 'function' && savePdf();
             
            },
            error: function (jqXHR, exception, errorThrown) {
                if (jqXHR.responseJSON.message == "No se encontró la llave del proceso.") {
                    $("#AlertFailed").show();
                    $("#AlertSuccess").hide();
                } else {
                    alert(jqXHR.responseJSON.type.Message);
                }
                
            }
        });
    }

    var dialog = $("#dialog").dialog({
        autoOpen: false,
        height: 400,
        width: 350,
        modal: true,
        buttons: {
            "Aceptar": function () {
                dialog.dialog("close");
            }
        }
    });

    var dialog2 = $("#dialog2").dialog({
        autoOpen: false,
        height: 400,
        width: 350,
        modal: true,
        buttons: {
            "Aceptar": function () {
                dialog2.dialog("close");
            }
        }
    });

    const formatter = new Intl.NumberFormat('es-CO', {
        style: 'currency',
        currency: 'COP',
        minimumFractionDigits: 2
    });

    $('.dropdown-submenu a.test').click(function (e) {
        $(this).next('ul').toggle();
        e.stopPropagation();
        e.preventDefault();
    });

    //-- INICIO LIQUIDACIÓN SINGULAR
    $("#liq_singular").ajaxForm({
        beforeSubmit: function (arr, $form, options) {
            if ($("input[name=completo]").val() === "") {
                alert("ERROR: No ha seleccionado ningun proceso.");
                return false;
            }else
                return true;
        },
        success: function (data) {
            t_liquidacion.clear();
            t_liquidacion.rows.add(data.detalle_liquidacion).draw();
            t_resumen.clear();
            t_resumen.rows.add(data.resumen).draw();
            $('a[href="#tab4"]').trigger("click");
            $('#guardar').removeAttr('disabled');
            if (data.resumen[data.resumen.length - 1].item1 == 'Saldo devolver al deudor') {
                $('#tab4 table tbody tr:last-child').css("color", "red");
                $('#texto').text('* Tener en cuenta que los abonos de fecha posterior a la fecha de liquidacion no seran tomados en cuenta')
            }
            else{
                $('#texto').text('')
            }
    
        },
        dataType: "json",
        error: function (jqXHR) {
            if (jqXHR.status == 401) {
                alert('Su sesión ha expirado.!');
                window.location.href = "/";
            }
            alert(jqXHR.responseJSON.message);
        }
    });
    //-- FIN LIQUIDACIÓN SINGULAR
    //-- INICIO DATATABLES
    var t_liquidacion = $("#tab3 table").DataTable({
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
                "data": "desde", render: function (data, type, row, meta) {
                    return new Date(data).toLocaleDateString("es-CO", { year: 'numeric', month: '2-digit', day: '2-digit' });
                }
            },
            {
                "data": "hasta", render: function (data, type, row, meta) {
                    return new Date(data).toLocaleDateString("es-CO", { year: 'numeric', month: '2-digit', day: '2-digit' });
                }
            },
            { "data": "noDias" },
            { "data": "tasaAnual", render: $.fn.dataTable.render.number('.', ',', 8) },
            { "data": "tasaMaxima", render: $.fn.dataTable.render.number('.', ',', 8) },
            { "data": "intAplicado", render: $.fn.dataTable.render.number('.', ',', 8) },
            {
                "data": "interesNominal", render: function (data, type, row, meta) {
                    return parseFloat(data * 100).toLocaleString(undefined, { maximumFractionDigits: 8 }) + ' %';
                }
            },
            { "data": "capital", render: $.fn.dataTable.render.number('.', ',', 2, "$ ") },
            { "data": "capitalALiquidar", render: $.fn.dataTable.render.number('.', ',', 2, "$ ") },
            { "data": "intPlazoPeriodo", render: $.fn.dataTable.render.number('.', ',', 2, "$ ") },
            { "data": "saldoInteresPlazoAcum", render: $.fn.dataTable.render.number('.', ',', 2, "$ ") },
            { "data": "interesMoraPeriodo", render: $.fn.dataTable.render.number('.', ',', 2, "$ ") },
            { "data": "interesAdeudadoMoraAcum", render: $.fn.dataTable.render.number('.', ',', 2, "$ ") },
            { "data": "abonos", render: $.fn.dataTable.render.number('.', ',', 2, "$ ") },
            { "data": "subTotal", render: $.fn.dataTable.render.number('.', ',', 2, "$ ") }
        ]
    });
    var t_resumen = $("#tab4 table").DataTable({
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
    var t_abonos = $("#t_abonos table").DataTable({
        "paging": false,
        "ordering": false,
        "info": false,
        "searching": false
    });
    var t_capitales = $("#t_capitales table").DataTable({
        "paging": false,
        "ordering": false,
        "info": false,
        "searching": false
    });
    //-- FIN DATATABLES
    //-- INICIO EXTRAS
    $("input[name=i_corriente]").change(function () {
        if ($("input[name=i_corriente][value=pactado]").prop("checked"))
            $("input[name=tasa_pacto_a]").attr("disabled", false);
        else {
            $("input[name=tasa_pacto_a]").attr("disabled", true);
            dialog.dialog("open");
        }
    });

    $("input[name=i_mora]").change(function () {
        if ($("input[name=i_mora][value=pactado]").prop("checked"))
            $("input[name=tasa_pacto_b]").attr("disabled", false);
        else {
            $("input[name=tasa_pacto_b]").attr("disabled", true);
            dialog2.dialog("open");
        }
    });

    $("input[name=i_corriente_var]").change(function () {
        if ($("input[name=i_corriente_var][value=DTF]").prop("checked"))
            $("#corriente_dtf").attr("disabled", false);
        else
            $("#corriente_dtf").attr("disabled", true);

        if ($("input[name=i_corriente_var][value=IPC]").prop("checked"))
            $("#corriente_ipc").attr("disabled", false);
        else
            $("#corriente_ipc").attr("disabled", true);
    });

    $("input[name=i_mora_var]").change(function () {
        if ($("input[name=i_mora_var][value=DTF]").prop("checked"))
            $("#mora_dtf").attr("disabled", false);
        else
            $("#mora_dtf").attr("disabled", true);

        if ($("input[name=i_mora_var][value=IPC]").prop("checked"))
            $("#mora_ipc").attr("disabled", false);
        else
            $("#mora_ipc").attr("disabled", true);
    });

    /* *
     * Hace una petición POST al link asignado para cada proceso traído de guardados.
     * 
     * Retorna el objeto JSON con el cual se debe llenar el formulario para realizar una liquidación.
     * */
    $('#guardados').on('click', 'a', function (e) {
        e.preventDefault();
        $.post(this.getAttribute('href'), function (data) {
            deserializeToForm(data);
            alert("Datos cargados con éxito.");
        });
    });
    $('#guardar').click(function () {
        guardar();
    });
    window.setInterval(function () {
        AutoGuardar();
    }, 60000);
    $('#descargarPdf').click(function (event) {
        event.preventDefault();

        guardar(function () {
            window.location.href = 'DownloadPdf';
        });
    });

    $("#add_abono").click(function () {
        t_abonos.row.add(['<i class="fa fa-trash delete"></i>','<input type="date" max="2100-01-01" min="1900-01-01" name="f_abono[]" />', '<input type="text" class="no-numbar" name="abono[]" />']).draw();
        new AutoNumeric('#t_abonos table tr:last-child td:last-child input', autoNumericOptionsThousands);
    });
    
    
    $('#t_abonos tbody').on('click', 'td i.delete', function () {
        t_abonos.row($(this).parents('tr')).remove().draw();
    });


    $("#add_capitales").click(function () {
        t_capitales.row.add(['<i class="fa fa-trash delete"></i>','<input type="date" max="2100-01-01" min="1900-01-01" name="f_capitales[]" />', '<input type="text" class="no-numbar" name="capitales[]" />']).draw();
        new AutoNumeric('#t_capitales table tr:last-child td:last-child input', autoNumericOptionsThousands);
    });

    $('#t_capitales tbody').on('click', 'td i.delete', function () {
        t_capitales.row($(this).parents('tr')).remove().draw();
    });
    //-- FIN EXTRAS

    //-- VALIDACIONES
    $('#t_abonos').on('focusout','input[name="f_abono[]"]', function() {
        //-- Procesos.js
        validarAbonosCapitales($('input[name="f_obligacion"]'), $('input[name="f_liquidacion"]'), $(this));
    });

    $('#t_capitales').on('focusout','input[name="f_capitales[]"]', function() {
        //-- Procesos.js
        validarAbonosCapitales($('input[name="f_obligacion"]'), $('input[name="f_liquidacion"]'), $(this));
    });
    //-- FIN VALIDACIONES

    $('#btn_completar_abono').click(function () {
        var jsonCuotas = '[';
        var difference = 0, indice = 0;
        $("input[name='f_abono[]']").each(function (index, obj) {
            
            if ($(this).val() && $("input[name='f_abono[]']").eq(indice + index + 1).val()) {
                console.log($(this).val());
                console.log($("input[name='f_abono[]']").eq(indice + index + 1).val());
                date1 = $(this).val().split('-');
                date2 = $("input[name='f_abono[]']").eq(indice + index + 1).val().split('-');

                difference = monthDiff(new Date(date1[0], date1[1] - 1, date1[2]), new Date(date2[0], date2[1] - 1, date2[2]));

                if (difference > 0) {
                    var sdate = obj.value.split('-');
                    var date = new Date(sdate[0], sdate[1] - 1, sdate[2]);
                    date.setMonth(date.getMonth() + difference + 1);
                    for (var i = difference - 1; i >= 0; i--) {
                        date.setMonth(date.getMonth() - 1);
                        $(this).parent().parent().after(
                            '<tr role="row"><td><i class="fa fa-trash delete"></i></td><td><input type="date" max="2100-01-01" min="1900-01-01" name="f_abono[]" value="'
                            + date.toFancyString()
                            + '" /></td><td><input type="text" class="no-numbar" name="abono[]" value="'
                            + $("input[name='abono[]']").eq(indice + index).val() + '"/></td></tr>');
                    }
                }
                indice += difference;
            }
        });
        $("input[name='f_abono[]']").each(function (index, obj) {
            jsonCuotas += '{ "fecha":"' + $("input[name='f_abono[]']").eq(index).val() + '", "monto" :"' + $("input[name='abono[]']").eq(index).val() + '"},';
        });
        jsonCuotas = jsonCuotas.substring(0, jsonCuotas.length - 1);
        jsonCuotas += ']';
        aux = JSON.parse(jsonCuotas);
        for (var i = 0; i < aux.length; i++) {
            if (aux[i].fecha.length == 0)
                aux.splice(i, 1);
        }
        jsonCuotas = JSON.stringify(aux);
        arreglo = JSON.parse(jsonCuotas);
        t_abonos.rows().remove().draw();
        for (var i = 0; i < arreglo.length; i++) {
            t_abonos.row.add(['<i class="fa fa-trash delete"></i>', '<input type="date" max="2100-01-01" min="1900-01-01" name="f_abono[]" value="' + arreglo[i].fecha + '" />', '<input type="text" class="no-numbar" name="abono[]"  value="' + arreglo[i].monto + '" />']).draw();
        }
        AutoNumeric.multiple('#t_abonos .no-numbar', autoNumericOptionsThousands);
    });

    $('#btn_completar_capital').click(function () {
        var jsonCuotas = '[';
        var difference = 0, indice = 0;
        $("input[name='f_capitales[]']").each(function (index, obj) {

            if ($(this).val() && $("input[name='f_capitales[]']").eq(indice + index + 1).val()) {
                date1 = $(this).val().split('-');
                date2 = $("input[name='f_capitales[]']").eq(indice + index + 1).val().split('-');

                difference = monthDiff(new Date(date1[0], date1[1] - 1, date1[2]), new Date(date2[0], date2[1] - 1, date2[2]));

                if (difference > 0) {
                    var sdate = obj.value.split('-');
                    var date = new Date(sdate[0], sdate[1] - 1, sdate[2]);
                    date.setMonth(date.getMonth() + difference + 1);
                    for (var i = difference - 1; i >= 0; i--) {
                        date.setMonth(date.getMonth() - 1);
                        $(this).parent().parent().after(
                            '<tr role="row"><td><i class="fa fa-trash delete"></i></td><td><input type="date" max="2100-01-01" min="1900-01-01" name="f_capitales[]" value="'
                            + date.toFancyString()
                            + '" /></td><td><input type="text" class="no-numbar" name="capitales[]" value="'
                            + $("input[name='capitales[]']").eq(indice + index).val() + '"/></td></tr>');
                    }
                }
                indice += difference;
            }
        });
        $("input[name='f_capitales[]']").each(function (index, obj) {
            jsonCuotas += '{ "fecha":"' + $("input[name='f_capitales[]']").eq(index).val() + '", "monto" :"' + $("input[name='capitales[]']").eq(index).val() + '"},';
        });
        jsonCuotas = jsonCuotas.substring(0, jsonCuotas.length - 1);
        jsonCuotas += ']';
        aux = JSON.parse(jsonCuotas);
        for (var i = 0; i < aux.length; i++) {
            if (aux[i].fecha.length == 0)
                aux.splice(i, 1);
        }
        jsonCuotas = JSON.stringify(aux);
        arreglo = JSON.parse(jsonCuotas);
        t_capitales.rows().remove().draw();
        for (var i = 0; i < arreglo.length; i++) {
            t_capitales.row.add(['<i class="fa fa-trash delete"></i>', '<input type="date" max="2100-01-01" min="1900-01-01" name="f_capitales[]" value="' + arreglo[i].fecha + '" />', '<input type="text" class="no-numbar" name="capitales[]"  value="' + arreglo[i].monto + '" />']).draw();
        }
        AutoNumeric.multiple('#t_capitales .no-numbar', autoNumericOptionsThousands);
    });

    function monthDiff(date1, date2) {
        var months;
        months = (date2.getFullYear() - date1.getFullYear()) * 12;
        months -= date1.getMonth() + 1;
        months += date2.getMonth();

        return months <= 0 ? 0 : months;
    }

    Date.prototype.toFancyString = function () {
        var mm = (this.getMonth() + 1); // getMonth() is zero-based
        var dd = this.getDate();

        return [this.getFullYear(), mm > 9 ? '' + mm : '0' + mm, dd > 9 ? '' + dd : '0' + dd].join('-'); // padding
    };
});