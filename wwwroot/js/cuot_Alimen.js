Date.prototype.toFancyString = function () {
    var mm = (this.getMonth() + 1); // getMonth() is zero-based
    var dd = this.getDate();

    return [this.getFullYear(), mm > 9 ? '' + mm : '0' + mm, dd > 9 ? '' + dd : '0' + dd].join('-'); // padding
};

$(document).ready(function () {
    AutoNumeric.multiple('.no-numbar', autoNumericOptionsThousands); //- Se le da el formato a los inputs

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
            { "data": "tasaAnual", render: $.fn.dataTable.render.number('.', ',', 8, "$ ") },
            { "data": "tasaMaxima", render: $.fn.dataTable.render.number('.', ',', 8, "$ ") },
            { "data": "intAplicado", render: $.fn.dataTable.render.number('.', ',', 8, "$ ") },
            {
                "data": "interesNominal", render: function (data, type, row, meta) {
                    return parseFloat(data * 100).toLocaleString(undefined, { maximumFractionDigits: 4 }) + ' %';
                }
            },
            { "data": "capital", render: $.fn.dataTable.render.number('.', ',', 2, "$ ") },
            { "data": "capitalALiquidar", render: $.fn.dataTable.render.number('.', ',', 2, "$ ") },
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
            { "data": "item2", render: $.fn.dataTable.render.number('.', ',', 2, '$ ') }
        ]
    });

    var t_abonos = $("#t_abonos").DataTable({
        "paging": false,
        "ordering": false,
        "info": false,
        "searching": false
    });

    var t_cuotas = $("#tab-tabla1 table").DataTable({
        "paging": false,
        "ordering": false,
        "info": false,
        "searching": false
    });

    var ext_cuotas = $("#tab-tabla2 table").DataTable({
        "paging": false,
        "ordering": false,
        "info": false,
        "searching": false
    });

    var t_multas = $("#tab-tabla3 table").DataTable({
        "paging": false,
        "ordering": false,
        "info": false,
        "searching": false
    });

    function deserializeToForm(JsonString) {
        var json = JSON.parse(JsonString);

        for (var element in json) {
            if (json.hasOwnProperty(element))
                if (Array.isArray(json[element])) {
                    let fields_to_create = json[element].length;
                    let field_name = element.concat('[]');

                    if (element === "f_abono")
                        for (let i = 0; i < fields_to_create; i++) {
                            t_abonos.row.add(['<i class="fa fa-trash delete"></i>','<input type="date"  max="2100-01-01" min="1900-01-01" name="f_abono[]" />', '<input type="text" class="no-numbar" name="abono[]" />']).draw();
                            new AutoNumeric('#t_abonos tr:last-child td:last-child input', autoNumericOptionsThousands);
                        }
                    else if (element === "f_cuota")
                        for (let i = 0; i < fields_to_create; i++) {
                            t_cuotas.row.add(['<i class="fa fa-trash delete"></i>', '<input type="date"  max="2100-01-01" min="1900-01-01" name="f_cuota[]" />', '<input type="text" class="no-numbar" name="cuota[]" />']).draw();
                            new AutoNumeric('#tab-tabla1 tr:last-child td:last-child input', autoNumericOptionsThousands);
                        }
                    else if (element === "f_ext")
                        for (let i = 0; i < fields_to_create; i++) {
                            ext_cuotas.row.add(['<i class="fa fa-trash delete"></i>','<input type="date"  max="2100-01-01" min="1900-01-01" name="f_ext[]" />', '<input type="text" class="no-numbar" name="ext[]" />', '<input type="checkbox" name="calcInt[]" />', '<input type="number" step=".00005" name="aSeguros[]" />']).draw();
                            new AutoNumeric('#tab-tabla2 table tr:last-child .no-numbar', autoNumericOptionsThousands);
                        }
                    else if (element === "f_multa")
                        for (let i = 0; i < fields_to_create; i++) {
                            t_multas.row.add(['<i class="fa fa-trash delete"></i>','<input type="date"  max="2100-01-01" min="1900-01-01" name="f_multa[]" />', '<input type="text" class="no-numbar" name="multa[]" />', '<input type="checkbox" name="intereses[]" />']).draw();
                            new AutoNumeric('#tab-tabla3 table tr:last-child .no-numbar', autoNumericOptionsThousands);
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
                    else
                        $('input[name=' + element + ']').val(json[element]);

                    if ($('input[name=' + element + ']').hasClass('oculto'))
                        $('input[name=' + element + ']').removeClass('oculto');

                    let field_name = element.concat('[]');
                    $('input[name="' + field_name + '"]').hasClass('no-numbar') ?
                        AutoNumeric.getAutoNumericElement('input[name="' + field_name + '"]').set(json[element]) :
                        $('input[name="' + field_name + '"]').val(json[element]);
                }
        }
    }

    function guardar(savePdf) {
        var formData = new FormData();
        formData.append('formulario', serializeToJSON($('#cuot_form')));
        formData.append('tipo', 98);

        $.ajax({
            beforeSend: function () {
                if (!$('#cuot_form')[0].checkValidity()) {
                    $('#cuot_form button[type=submit]').click();
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
        formData.append('formulario', serializeToJSON($('#cuot_form')));
        formData.append('idProceso', $("input[name=idProceso]").val());
        formData.append('tipo', 98);

        return $.ajax({
            beforeSend: function () {
                if (!$('#cuot_form')[0].checkValidity()) {
                    $('#cuot_form button[type=submit]').click();
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

    $('#cuot_form').ajaxForm({
        beforeSubmit: function (arr, $form, options) {
            if ($('input[name=completo]').val() !== "") {
                $('input[type=checkbox]').each(function () {
                    if (!$(this).is(':checked')) {
                        $(this).prop('checked', true);
                        $(this).val('off');
                    }
                });
                return true;
            } else {
                alert("ERROR: No ha seleccionado ningun proceso.");
                return false;
            }
        },
        dataType: "json",
        success: function (data) {
            t_liquidacion.clear();
            t_liquidacion.rows.add(data.detalle_liquidacion).draw();
            t_resumen.clear();
            t_resumen.rows.add(data.resumen).draw();
            $('a[href="#tab3"]').trigger("click");
            $('input[type=checkbox]').each(function () {
                if ($(this).val() === "off")
                    $(this).prop('checked', false);
            });
            $('#guardar').removeAttr('disabled');
        },
        error: function (jqXHR, exception) { alert(jqXHR.responseJSON.message); }
    });

    $('#tab-tabla1').on('focusout', 'input[name="f_cuota[]"]', function () {
        try {
            if ($(this).val() != "") {
                if (new Date($(this).val()) < new Date($("input[name=f_exigibilidad]").val()) ||
                    new Date($(this).val()) > new Date($("input[name=f_liquidacion]").val())) {
                    if ($("#err_msg").length == 0) //-- Dont exists
                        $('<span class="alert alert-danger" id="err_msg" style="font-size:0.8em; display:block;">Las fechas de las cuotas deben estar entre las fechas de exigibilidad y de liquidación</span>').insertAfter("#tab-tabla1 table");
                } else if ($("#err_msg").length != 0)
                    $("#err_msg").remove();
            }
        } catch (exception) {
            alert("Hubo un problema convirtiendo las fechas");
        }
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

    $('#tab-tabla1').on('focusout', 'input[name="cuota[]"]:last', function () {
        t_cuotas.row.add(['<i class="fa fa-trash delete"></i>','<input type="date"  max="2100-01-01" min="1900-01-01" name="f_cuota[]" />', '<input type="text" class="no-numbar" name="cuota[]" />']).draw();
        new AutoNumeric('#tab-tabla1 tr:last-child td:last-child input', autoNumericOptionsThousands);
        $('input[name="f_cuota[]"]:last').focus();

    });

    $('#t_cuotas tbody').on('click', 'td i.delete', function () {
        t_cuotas.row($(this).parents('tr')).remove().draw();
    });

    $('#t_abonos').on('focusout', 'input[name="abono[]"]:last', function () {
        t_abonos.row.add(['<i class="fa fa-trash delete"></i>', '<input type="date"  max="2100-01-01" min="1900-01-01" name="f_abono[]" />', '<input type="text" class="no-numbar" name="abono[]" />']).draw();
        new AutoNumeric('#t_abonos tr:last-child td:last-child input', autoNumericOptionsThousands);
        $('input[name="f_abono[]"]:last').focus();
    });

    $('#t_abonos tbody').on('click', 'td i.delete', function () {
        t_abonos.row($(this).parents('tr')).remove().draw();
    });

    $("#btn_add_ext").click( function () {
        ext_cuotas.row.add(['<i class="fa fa-trash delete"></i>','<input type="date"  max="2100-01-01" min="1900-01-01" name="f_ext[]" />', '<input type="text" class="no-numbar" name="ext[]" />', '<input type="checkbox" name="calcInt[]" />', '<input type="number" step=".00005" name="aSeguros[]" />']).draw();
        new AutoNumeric('#tab-tabla2 table tr:last-child .no-numbar', autoNumericOptionsThousands);
        $('input[name="f_ext[]"]:last').focus();
    });

    $('#tab-tabla2 tbody').on('click', 'td i.delete', function () {
        ext_cuotas.row($(this).parents('tr')).remove().draw();
    });

    $("#btn_add_multa").click(function () {
        t_multas.row.add(['<i class="fa fa-trash delete"></i>','<input type="date"  max="2100-01-01" min="1900-01-01" name="f_multa[]" />', '<input type="text" class="no-numbar" name="multa[]" />', '<input type="checkbox" name="intereses[]" />']).draw();
        new AutoNumeric('#tab-tabla3 table tr:last-child .no-numbar', autoNumericOptionsThousands);
        $('input[name="f_multa[]"]:last').focus();
    });

    $('#t_multas tbody').on('click', 'td i.delete', function () {
        t_multas.row($(this).parents('tr')).remove().draw();
    });

    $('#btn_completar').click(function () {
        var jsonCuotas = '[';
        var difference = 0, indice = 0;
        $("input[name='f_cuota[]']").each(function (index, obj) {
            if ($(this).val() && $("input[name='f_cuota[]']").eq(indice + index + 1).val())
            {
                date1 = $(this).val().split('-');
                date2 = $("input[name='f_cuota[]']").eq(indice + index + 1).val().split('-');

                difference = monthDiff(new Date(date1[0], date1[1] - 1, date1[2]), new Date(date2[0], date2[1] - 1, date2[2]));
                
                if (difference > 0) {
                    var sdate = obj.value.split('-');
                    var date = new Date(sdate[0], sdate[1] - 1, sdate[2]);
                    date.setMonth(date.getMonth() + difference + 1);
                    for (var i = difference - 1; i >= 0; i--) {
                        date.setMonth(date.getMonth() - 1);
                        $(this).parent().parent().after(
                            '<tr role="row"><td><i class="fa fa-trash delete"></i></td><td><input type="date"  max="2100-01-01" min="1900-01-01" name="f_cuota[]" value="'
                            + date.toFancyString()
                            + '" /></td><td><input type="text" class="no-numbar" name="cuota[]" value="'
                            + $("input[name='cuota[]']").eq(indice + index).val() + '"/></td></tr>');
                    }
                }
                indice += difference;
            }
        });
        $("input[name='f_cuota[]']").each(function (index, obj) {
            jsonCuotas += '{ "fecha":"' + $("input[name='f_cuota[]']").eq(index).val() + '", "monto" :"' + $("input[name='cuota[]']").eq(index).val() + '"},';
        });
        jsonCuotas = jsonCuotas.substring(0, jsonCuotas.length - 1);
        jsonCuotas += ']';
        aux = JSON.parse(jsonCuotas);
        for (var i = 0; i < aux.length; i++)
        {
            if (aux[i].fecha.length==0)
                aux.splice(i,1);
        }
        jsonCuotas = JSON.stringify(aux);
        t_cuotas.rows().remove().draw();
        //if ($('input[name = "v_cuota"]:checked').val() === 'SUELDO') {
            //var proporcion = $('input[name="v_cuota_suendo"]').val();
            
        getValoresFechas(jsonCuotas);
        //}
        /*if ($('input[name = "v_cuota"]:checked').val() === 'MANUAL') {
            arreglo = JSON.parse(jsonCuotas);
            arreglo.sort(function (a, b) {
                if (a.fecha > b.fecha) {
                    return 1;
                }
                if (a.fecha < b.fecha) {
                    return -1;
                }
                // a must be equal to b
                return 0;
            });
            for (var i = 0; i < arreglo.length; i++) {
                t_cuotas.row.add(['<i class="fa fa-trash delete"></i>', '<input type="date" name="f_cuota[]" value="' + arreglo[i].fecha + '" />', '<input type="text" class="no-numbar" name="cuota[]"  value="' + arreglo[i].monto +'" />']).draw();
            }
        }*/
        
    });

    function monthDiff(date1, date2) {
        var months;
        months = (date2.getFullYear() - date1.getFullYear()) * 12;
        months -= date1.getMonth() + 1;
        months += date2.getMonth();

        return months <= 0 ? 0 : months;
    }

    /*$("input[name=i_mora]").change(function () {
        if ($("input[name=i_mora][value=PACTADO]").prop("checked")) {
            $("input[name=i_pactado]").attr("disabled", false);
            $("input[name=i_pactado]").removeClass("oculto");            
        }else {
            $("input[name=i_pactado]").attr("disabled", true);
            $("input[name=i_pactado]").addClass("oculto");            
        }
    });*/

    $("input[name=v_incremento]").change(function () {
        if ($("input[name=v_incremento][value=MANUAL]").prop("checked")) {
            $("input[name=v_incremento_pactado]").attr("disabled", false);
            $("input[name=v_incremento_pactado]").removeClass("oculto");
            $("#text_cuota").removeClass("oculto");
        } else {
            $("input[name=v_incremento_pactado]").attr("disabled", true);
            $("input[name=v_incremento_pactado]").addClass("oculto");
            $("#text_cuota").addClass("oculto");
        }
    });

    $("input[name=v_incremento_ex]").change(function () {
        if ($("input[name=v_incremento_ex][value=MANUAL]").prop("checked")) {
            $("input[name=v_incremento_pactado_ex]").attr("disabled", false);
            $("input[name=v_incremento_pactado_ex]").removeClass("oculto");
            $("#text_cuota_ex").removeClass("oculto");
        } else {
            $("input[name=v_incremento_pactado_ex]").attr("disabled", true);
            $("input[name=v_incremento_pactado_ex]").addClass("oculto");
            $("#text_cuota_ex").addClass("oculto");
        }
    });



    //-- VALIDACIONES
    $('#t_abonos').on('focusout','input[name="f_abono[]"]', function() {
        //-- Procesos.js
        validarAbonosCapitales($('input[name="f_exigibilidad"]'), $('input[name="f_liquidacion"]'), $(this));
    });

    $('input[name=f_exigibilidad]').blur(function () {
        if ($('input[name=f_exigibilidad]').length > 0) {
            if ($('input[name=fecha_incremento]').val().length == 0) {
                var sdate = $('input[name=f_exigibilidad]').val();
                sdate = sdate.split('-');
                var date = new Date(parseInt(sdate[0])+1, sdate[1] - 1, sdate[2]);
                $('input[name=fecha_incremento]').val(date.toFancyString());
                $('input[name=fecha_incremento_ex]').val(date.toFancyString());
            }
        }
        
    });

    $('#t_cuotas').on('focusout', 'tr input[name="f_cuota[]"]', function () {
        //-- Procesos.js
        validarAbonosCapitales($('input[name="f_exigibilidad"]'), $('input[name="f_liquidacion"]'), $(this));
        var fecha = $(this).val();
        var fechaIncremento = $('input[name="fecha_incremento"]').val();
        var incremento = $('input[name = "v_incremento"]:checked').val();
        if (incremento === 'MANUAL') {
            incremento = $('input[name="v_incremento_pactado"]').val();
        }
        var valorCuota = $('input[name="v_cuota"]').val();
        valorCuota = valorCuota.replace('$ ', '').replace('.', '').replace(',','.');
        var incideTabla = t_cuotas.row($(this).parents('tr')).index();
        getValorCuota(fecha ,fechaIncremento, incremento, valorCuota, incideTabla);
        
        
    });

    $('#t_extraordinarias').on('focusout', 'input[name="f_ext[]"]', function () {
        //-- Procesos.js
        validarAbonosCapitales($('input[name="f_exigibilidad"]'), $('input[name="f_liquidacion"]'), $(this));
    });

    $('#t_multas').on('focusout', 'input[name="f_multa[]"]', function () {
        //-- Procesos.js
        validarAbonosCapitales($('input[name="f_exigibilidad"]'), $('input[name="f_liquidacion"]'), $(this));
    });
    //-- FIN VALIDACIONES


    /*
    $("#t_abonos").on('dblclick', 'td', function () {
        t_abonos.row($(this).parents('tr')).remove().draw();
    });

    $("#tab-tabla1 table").on('dblclick', 'input', function () {
        t_cuotas.row($(this).parents('tr')).remove().draw();
    });
    */

    $('#t_calculadora').on('focusout', 'tr input[name="f_calculadora"]', function () {
        //-- Procesos.js
        var fecha = $(this).val();
        var fechaIncremento = $('input[name="fecha_incremento_ex"]').val();
        var incremento = $('input[name = "v_incremento_ex"]:checked').val();
        if (incremento === 'MANUAL') {
            incremento = $('input[name="v_incremento_pactado_ex"]').val();
        }
        var valorCuota = $('input[name="v_cuota_ex"]').val();
        valorCuota = valorCuota.replace('$ ', '').replace('.', '').replace(',', '.');
        getValorCuotaExtraordinaria(fecha, fechaIncremento, incremento, valorCuota);


    });

    function getValorCuotaExtraordinaria(fecha, fechaIncremento, incremento, valorCuota) {

        if (fechaIncremento.length > 0 && fecha.length > 0) {
            var formData = new FormData();
            formData.append('Fecha', fecha + " 00:00:00.000");
            formData.append('FechaIncremento', fechaIncremento + " 00:00:00.000");
            formData.append('Incremento', incremento);
            formData.append('ValorCuota', valorCuota);
            $('#progreso').show();
            $.ajax({
                contentType: false,
                url: "/Liquidador/GetValorCuota",
                data: formData,
                processData: false,
                method: "post",
                success: function (data, textStatus, jqXHR) {
                    $('#progreso').hide();

                    if (data.Error) {
                        alert(data.Error);
                    } else {
                        var num = data.valor;
                        var new_num = num.toFixed(2);
                        $('input[name="c_calculadora"]').val(new_num);
                        //var campo = '<input type="text" class="no-numbar" name="cuota[]" value="' + new_num + '" />';
                        //t_cuotas.cell({ row: incideTabla, column: 2 }).data(campo);
                    }
                    //var t = document.getElementById("t_cuotas");
                    //var tb = t.getElementsByTagName("tbody");

                    /*var td = tr[incideTabla].getElementsByTagName("td");
                    var input = td[2].getElementsByTagName("input");*/
                    //var input = tb[0]['children'][incideTabla]['children'][2]['children'][0];
                    new AutoNumeric('input[name="c_calculadora"]', autoNumericOptionsThousands);

                },
                error: function (jqXHR, exception, errorThrown) {
                    $('#progreso').hide();
                    jqXHR.responseJSON.type ? alert(jqXHR.responseJSON.type.Message) : alert(jqXHR.responseJSON.message);
                }
            });
        }

    }

    function getValorCuota(fecha ,fechaIncremento, incremento, valorCuota, incideTabla) {

        if (fechaIncremento.length > 0 && fecha.length > 0) {
            var formData = new FormData();
            formData.append('Fecha', fecha + " 00:00:00.000");
            formData.append('FechaIncremento', fechaIncremento + " 00:00:00.000");
            formData.append('Incremento', incremento);
            formData.append('ValorCuota', valorCuota);
            $('#progreso').show();
            $.ajax({
                contentType: false,
                url: "/Liquidador/GetValorCuota",
                data: formData,
                processData: false,
                method: "post",
                success: function (data, textStatus, jqXHR) {
                    $('#progreso').hide();
                    
                    if (data.Error) {
                        alert(data.Error);
                    } else {
                        var num = data.valor;
                        var new_num = num.toFixed(2);
                        var campo = '<input type="text" class="no-numbar" name="cuota[]" value="' + new_num + '" />';
                        t_cuotas.cell({ row: incideTabla, column: 2 }).data(campo);
                    }
                    var t = document.getElementById("t_cuotas");
                    var tb = t.getElementsByTagName("tbody");
                    
                    /*var td = tr[incideTabla].getElementsByTagName("td");
                    var input = td[2].getElementsByTagName("input");*/
                    var input = tb[0]['children'][incideTabla]['children'][2]['children'][0];
                    new AutoNumeric(input, autoNumericOptionsThousands);

                },
                error: function (jqXHR, exception, errorThrown) {
                    $('#progreso').hide();
                    jqXHR.responseJSON.type ? alert(jqXHR.responseJSON.type.Message) : alert(jqXHR.responseJSON.message);
                }
            });
        }
        
    }

    function getValoresFechas(jsonCuotas) {
        var fechaIncremento = $('input[name="fecha_incremento"]').val();
        var incremento = $('input[name = "v_incremento"]:checked').val();
        if (incremento === 'MANUAL') {
            incremento = $('input[name="v_incremento_pactado"]').val();
        }
        var valorCuota = $('input[name="v_cuota"]').val();
        valorCuota = valorCuota.replace('$ ', '').replace('.', '').replace(',', '.');
        
        if (fechaIncremento.length > 0) {
            var formData = new FormData();
            formData.append('jsonCuotas', jsonCuotas);
            formData.append('FechaIncremento', fechaIncremento + " 00:00:00.000");
            formData.append('Incremento', incremento);
            formData.append('ValorCuota', valorCuota);
            $('#progreso').show();
            $.ajax({
                contentType: false,
                url: "/Liquidador/GetValoresFechas",
                data: formData,
                processData: false,
                method: "post",
                success: function (data, textStatus, jqXHR) {
                    $('#progreso').hide();
                    if (data) {
                        if (data.length > 0) {
                            for (var i = 0; i < data.length; i++) {
                                t_cuotas.row.add(['<i class="fa fa-trash delete"></i>', '<input type="date"  max="2100-01-01" min="1900-01-01" name="f_cuota[]" value="' + data[i].fecha + '" />', '<input type="text" class="no-numbar" name="cuota[]"  value="' + data[i].valor + '" />']).draw();
                            }
                        }
                    }
                    AutoNumeric.multiple('#t_cuotas .no-numbar', autoNumericOptionsThousands);

                },
                error: function (jqXHR, exception, errorThrown) {
                    $('#progreso').hide();
                    jqXHR.responseJSON.type ? alert(jqXHR.responseJSON.type.Message) : alert(jqXHR.responseJSON.message);
                }
            });
        }
    }
});