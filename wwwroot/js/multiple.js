// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

$(document).ready(function () {
    var liquidaciones = [];
    var formulario = $("#sng_mult");
    var actualizar = false; //-- Flag que indica si se va a sucribir una nueva liquidación, o actualizar una existente
    var pos = 0; //-- Posición actual del array de respuesta
    var response = []; //-- Respuesta JSON del servidor con la tabla de liquidaciones y de resúmenes
    //AutoNumeric.multiple('.no-numbar', autoNumericOptionsThousands); //- Se le da el formato a los inputs
    /**
     * Convierte los datos del formulario, a un objeto JSON
     * @param {any} f
     */
    function formToJSON(f) {
        var json = {};
        f.serializeArray().map(function (item) {
            if (json[item.name]) {
                if (typeof (json[item.name]) === "string") {
                    json[item.name] = [json[item.name]];
                }
                if(item.value != "")
                    json[item.name].push(item.value);
            } else {
                json[item.name] = item.value;
            }
        });

        return serializeJSON(json);
    }
    /**
     * Compacta los datos del formulario a lo que necesita la tabla.
     * @param {JSON} json
     */
    function serializeJSON(json) {
        if (json.hasOwnProperty('i_corriente')) {
            if (json.i_corriente === "variable") {
                json.i_corriente = json.i_corriente_var;
                delete json.i_corriente_var;
                json.tasa_pacto_a = null; //-- Como no es pactado, este campo no existe, por lo tanto se crea vacío.
            } else
                delete json.i_corriente_var; //-- Como es pactado, no hace falta este campo
        } else { //-- Variable IBC es el predeterminado
            json.i_corriente = json.i_corriente_var;
            delete json.i_corriente_var;
            json.tasa_pacto_a = null; //-- Como no es pactado, este campo no existe, por lo tanto se crea vacío.
        }

        if (json.hasOwnProperty('i_mora')) {
            if (json.i_mora === "variable") {
                json.i_mora = json.i_mora_var;
                delete json.i_mora_var;
                json.tasa_pacto_b = null; //-- Como no es pactado, este campo no existe, por lo tanto se crea vacío.
            } else
                delete json.i_mora_var; //-- Como es pactado, no hace falta este campo
        } else { //-- Variable IBC es el predeterminado
            json.i_mora = json.i_mora_var;
            delete json.i_mora_var;
            json.tasa_pacto_b = null; //-- Como no es pactado, este campo no existe, por lo tanto se crea vacío.
        }

        if (!json.hasOwnProperty('aplica_sancion'))
            json.aplica_sancion = false;
        else
            json.aplica_sancion = true;

        if (!json.hasOwnProperty('base_360'))
            json.base_360 = false;
        else
            json.base_360 = true;

        if (!json.hasOwnProperty('puntos_corriente'))
            json.puntos_corriente = null;

        if (!json.hasOwnProperty('puntos_mora'))
            json.puntos_mora = null;

        if (json["f_abono[]"] === "")
            delete json["f_abono[]"];

        if (json["abono[]"] === "")
            delete json["abono[]"];

        actualizar ? liquidaciones[pos] = json : liquidaciones.push(json);

        return json;
    }

    function extractToForm(colIdx) {
        var json = liquidaciones[colIdx];

        for (var element in json) {
            if (json.hasOwnProperty(element))
                if (Array.isArray(json[element])) {
                    let fields_to_create = json[element].length;

                    if (element === "f_abono[]")
                        for (let i = 0; i < fields_to_create; i++) {
                            t_abonos.row.add(['<i class="fa fa-trash delete"></i>', '<input type="date" max="2100-01-01" min="1900-01-01" name="f_abono[]" />', '<input type="number" step=".01" class="no-numbar" name="abono[]" />']).draw();
                            //new AutoNumeric('#t_abonos table tr:last-child td:last-child input', autoNumericOptionsThousands);
                            }
                    else if (element === "f_capitales[]")
                        for (let i = 0; i < fields_to_create; i++)
                            t_capitales.row.add(['<i class="fa fa-trash delete"></i>', '<input type="date" max="2100-01-01" min="1900-01-01" name="f_capitales[]" />', '<input type="number" step=".01" class="no-numbar" name="capitales[]" />']).draw();

                    $('input[name="' + element + '"]').each(function (index, obj) {
                            $(this).val(json[element][index]);
                    });

                } else {
                    if ($('textarea[name=' + element + ']').attr('rows') == '5') {
                        $('textarea[name=' + element + ']').val(json[element]);
                    } else if ( (element === 'i_mora' || element === 'i_corriente') && json[element] !== 'pactado') {
                        $('input[name=' + element + '][value=variable]').prop('checked', true);
                        $('input[name=' + element + '_var][value=' + json[element] + ']').prop('checked', true);
                    } else if ($('input[name=' + element + ']').prop('disabled') && json[element] !== null) {
                        $('input[name=' + element + ']').prop('disabled', false);
                        $('input[name=' + element + ']').val(json[element]);
                    } else if ($('input[name=' + element + ']').attr('type') == 'checkbox' && json[element])
                        $('input[name=' + element + ']').prop('checked', true);
                    else if ($('input[name=' + element + ']').attr('type') == 'radio')
                        $('input[name=' + element + '][value=' + json[element] + ']').prop('checked', true);
                    else
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
        formData.append('formulario', JSON.stringify(liquidaciones));
        formData.append('tipo', 91);

        return $.ajax({
            beforeSend: function () {
                if (t_datos.rows.count > 0) {
                    alert("ERROR: No hay liquidaciones que guardar.");
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
            },
            error: function (jqXHR, exception, errorThrown) {
                jqXHR.responseJSON.type ? alert(jqXHR.responseJSON.type.Message) : alert(jqXHR.responseJSON.message);
            }
        });
    }

    function AutoGuardar(savePdf) {
        var formData = new FormData();
        formData.append('formulario', JSON.stringify(liquidaciones));
        formData.append('idProceso', $("input[name=idProceso]").val());
        formData.append('tipo', 91);

        return $.ajax({
            beforeSend: function () {
                if (t_datos.rows.count > 0) {
                    alert("ERROR: No hay liquidaciones que guardar.");
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
                //t_guardados.row.add(data.data).draw();
                typeof savePdf === 'function' && savePdf();
                //$('#guardados tr:last').after('<tr><td><a href="/Liquidador/Cargar?liquidacion='
                //    + data.data.id + '">'
                //    + data.data.llaveproc + '</a></td><td>'
                //    + data.data.tipo + '</td><td>'
                //    + new Date(data.data.fecha).toLocaleDateString("es-CO", { year: 'numeric', month: '2-digit', day: '2-digit' }) + '</td><td>'
                //    + data.data.usuario + '</td></tr>');
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

    var dialog3 = $("#dialog3").dialog({
        autoOpen: false,
        height: 500,
        width: screen.availWidth - 15,
        modal: true,
        buttons: {
            "Aceptar": function () {
                if (!formulario[0].checkValidity()) {
                    $('<input type="submit">').hide().appendTo(formulario).click().remove();
                    formulario.find(':submit').click();
                } else {
                    if (actualizar) {
                        t_datos.row(pos).data(formToJSON(formulario));
                    } else {
                        t_datos.row.add(formToJSON(formulario)).draw();
                    }

                    dialog3.dialog("close");
                }
            }
        }
    });

    $("#add_capital").click(function () {
        ($("input[name=completo]").val() === "") ? alert("Error: No ha seleccionado ningún proceso") : dialog3.dialog("open");
    });


    $("#add_abono").click(function () {
        t_abonos.row.add(['<i class="fa fa-trash delete"></i>', '<input type="date"  max="2100-01-01" min="1900-01-01" name="f_abono[]" />', '<input type="number" step=".01" class="no-numbar" name="abono[]" />']).draw();
        //new AutoNumeric('#t_abonos table tr:last-child td:last-child input', autoNumericOptionsThousands);

    });
    $('#t_abonos tbody').on('click', 'td i.delete', function () {
        t_abonos.row($(this).parents('tr')).remove().draw();
    });

    $("#s_liquidacion").change(function () {
        t_liquidacion.clear();
        t_liquidacion.rows.add(response[$("#s_liquidacion").val()].detalle_liquidacion).draw();
        t_resumen.clear();
        t_resumen.rows.add(response[$("#s_liquidacion").val()].resumen).draw();
    });

    $("#ver_resumen").change(function () {
        if ($("#ver_resumen").is(':checked')) {
            $("#t_liquidacion_wrapper").addClass('oculto');
            $("#t_resumen").removeClass('oculto');
        } else {
            $("#t_liquidacion_wrapper").removeClass('oculto');
            $("#t_resumen").addClass('oculto');
        }

    });

    $("#btn_liquidar").click(function () {
        $.post("/Liquidador/Singular_Multiple", { data: JSON.stringify(liquidaciones), idProceso: $("input[name=completo]").val() })
            .done(function (data) {
                $("#s_liquidacion").children().remove();
                for (var i = 0; i < data.data.length; i++)
                    $("#s_liquidacion").append(new Option(i+1, i));
                response = data.data;
                t_liquidacion.clear();
                t_liquidacion.rows.add(data.data[0].detalle_liquidacion).draw();
                t_resumen.clear();
                t_resumen.rows.add(data.data[0].resumen).draw();
                t_aresumen.clear();
                t_aresumen.rows.add(data.resumen_general).draw();
                $('a[href="#tab3"]').trigger("click");
                $('#guardar').removeAttr('disabled');
            }).fail(function (jqXHR, exception) {
                if (jqXHR.status == 401) {
                    alert('Su sesión ha expirado.!');
                    window.location.href = "/";
                }
                alert('Ocurrió un error durante el proceso de liquidar, favor verificar los datos ingresados..!');
            });
    });

    $('.dropdown-submenu a.test').click(function (e) {
        $(this).next('ul').toggle();
        e.stopPropagation();
        e.preventDefault();
    });

    //-- INICIO DATATABLES
    var t_datos = $("#datos-liq").DataTable({
        "paging": false,
        "ordering": false,
        "scrollX": true,
        "info": false,
        "searching": false,
        "language": {
            "decimal": ",",
            "thousands": ".",
            "zeroRecords": "Seleccione un proceso, y añada datos con el botón \"Agregar Capital\"",
            "infoEmpty": "Sin datos para mostrar."
        },
        "columns": [
            { "data": "capital", render: $.fn.dataTable.render.number('.', ',', 2, '$ ') },
            { "data": "f_obligacion" },
            { "data": "f_exigibilidad" },
            { "data": "f_liquidacion" },
            {
                "data": "i_corriente", render: function (data, type, row, meta) {
                    if (data === "pactado")
                        return '<input type="checkbox" checked disabled />';
                    else
                        return '<input type="checkbox" disabled />';
                }
            },
            { "data": "tasa_pacto_a" },
            { "data": "i_corriente" },
            {
                "data": "i_mora", render: function (data, type, row, meta) {
                    if (data === "pactado")
                        return '<input type="checkbox" checked disabled />';
                    else
                        return '<input type="checkbox" disabled />';
                }
            },
            { "data": "tasa_pacto_b" },
            { "data": "i_mora" },
            {
                "data": "aplica_sancion", render: function (data, type, row, meta) {
                    if (data)
                        return '<input type="checkbox" checked disabled />';
                    else
                        return '<input type="checkbox" disabled />';
                }
            },
            {
                "data": "base_360", render: function (data, type, row, meta) {
                    if (data)
                        return '<input type="checkbox" checked disabled />';
                    else
                        return '<input type="checkbox" disabled />';
                }
            },
            { "data": "puntos_mora" },
            { "data": "puntos_corriente" }
        ]
    });
    var t_liquidacion = $("#t_liquidacion").DataTable({
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
            { "data": "tasaAnual", render: $.fn.dataTable.render.number('.', ',', 8, "$ ") },
            { "data": "tasaMaxima", render: $.fn.dataTable.render.number('.', ',', 8, "$ ") },
            { "data": "intAplicado", render: $.fn.dataTable.render.number('.', ',', 8, "$ ") },
            { "data": "interesNominal", render: function (data, type, row, meta) {
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
    var t_resumen = $("#t_resumen").DataTable({
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
    var t_aresumen = $("#tab3 table").DataTable({
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

    //-- EVENTOS DATATABLES
    $("#datos-liq tbody").on('click', 'tr', function () {
        pos = t_datos.row(this).index();
        $(this).siblings('.highlight').removeClass('highlight');
        $(this).addClass('highlight');
    });

    $('#datos-liq tbody').on('dblclick', 'tr', function () {
        extractToForm(pos);
        actualizar = true;
        dialog3.dialog("open");
    });

    //-- FIN DATATABLES
    //-- INICIO EXTRAS
    $('#dialog3').on('dialogclose', function (event) {
        formulario[0].reset();
        t_abonos.clear();
        t_abonos.row.add(['<i class="fa fa-trash delete"></i>','<input type="date"  max="2100-01-01" min="1900-01-01" name="f_abono[]" />', '<input type="number" class="no-numbar" step=".01" name="abono[]" />']).draw();
        actualizar = false;
    });
    $('#guardados').on('click', 'a', function (e) {
        e.preventDefault();
        $.post(this.getAttribute('href'), function (data) {
            liquidaciones = JSON.parse(data);
            t_datos.clear();
            t_datos.rows.add(liquidaciones).draw();
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
                            '<tr role="row"><td><i class="fa fa-trash delete"></i></td><td><input type="date"  max="2100-01-01" min="1900-01-01" name="f_abono[]" value="'
                            + date.toFancyString()
                            + '" /></td><td><input type="number" step=".01" class="no-numbar" name="abono[]" value="'
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
            t_abonos.row.add(['<i class="fa fa-trash delete"></i>', '<input type="date"   max="2100-01-01" min="1900-01-01"name="f_abono[]" value="' + arreglo[i].fecha + '" />', '<input type="number" step=".01" class="no-numbar" name="abono[]"  value="' + arreglo[i].monto + '" />']).draw();
        }
        //AutoNumeric.multiple('#t_abonos .no-numbar', autoNumericOptionsThousands);
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