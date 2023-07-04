Date.prototype.toFancyString = function () {
    var mm = (this.getMonth() + 1); // getMonth() is zero-based
    var dd = this.getDate();

    return [this.getFullYear(), mm > 9 ? '' + mm : '0' + mm, dd > 9 ? '' + dd : '0' + dd].join('-'); // padding
};

$(document).ready(function () {
    AutoNumeric.multiple('.no-numbar', autoNumericOptionsThousands); //- Se le da el formato a los inputs
    /**
     * Calcula la diferencia entre dos fechas.
     * @param {Date} date1
     * @param {Date} date2
     */
    function monthDiff(date1, date2) {
        var months;
        months = (date2.getFullYear() - date1.getFullYear()) * 12;
        months -= date1.getMonth() + 1;
        months += date2.getMonth();

        return months <= 0 ? 0 : months;
    }
    function guardar(savePdf) {
        var formData = new FormData();
        formData.append('formulario', serializeToJSON($('#frm_hipotecario')));
        formData.append('tipo', 94);

        return $.ajax({
            beforeSend: function () {
                if (!$('#frm_hipotecario')[0].checkValidity()) {
                    $('#frm_hipotecario button[type=submit]').click();
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
        formData.append('formulario', serializeToJSON($('#frm_hipotecario')));
        formData.append('idProceso', $("input[name=idProceso]").val());
        formData.append('tipo', 94);

        return $.ajax({
            beforeSend: function () {
                if (!$('#frm_hipotecario')[0].checkValidity()) {
                    $('#frm_hipotecario button[type=submit]').click();
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
    /**********************************
     ********* COMMON FUNCTIONS *******
     **********************************
     ***/
    var consulta_ivs = false;
    var maxi_plazo = 0;

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
    var tabla_ivs = $("#dialog table").DataTable({
        "paging": false,
        "scrollY": "40vh",
        "ordering": true,
        "order": [[ 0, "desc" ]],
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
                "data": "vigenteDesde", render: function (data, type, row, meta) {
                    return new Date(data).toLocaleDateString("es-CO", { year: 'numeric', month: '2-digit', day: '2-digit' });
                }
            },
            {
                "data": "vigenteHasta", render: function (data, type, row, meta) {
                    return new Date(data).toLocaleDateString("es-CO", { year: 'numeric', month: '2-digit', day: '2-digit' });
                }
            },
            {
                "data": "valorTasa", render: function (data, type, row, meta) {
                    return "$ "+parseFloat(data*135).toLocaleString(undefined, { maximumFractionDigits: 2 });
                }
            }
        ]
    });
    var tabla_abonos = $("#t_abonos table").DataTable({
        "paging": false,
        "ordering": false,
        "info": false,
        "searching": false
    });
    var tabla_capitales = $("#t_capitales table").DataTable({
        "paging": false,
        "ordering": false,
        "info": false,
        "searching": false
    });

    $("#btn_ivs").click(function () {
        if (!consulta_ivs) {
            $.post("/Liquidador/ListaVIS", function (data) {
                tabla_ivs.rows.add(data).draw();
                consulta_ivs = true;
            }, "json").fail(function () {
                alert("ERROR en la llamada.");
            });
        }

        dialog.dialog("open");
    });

    $("input[name=vis]").change(function () {
        $.post("/Liquidador/ObtenerTasas", { fecha: $("input[name=f_contrato]").val(), VIS: $(this).val(), moneda: $("input[name=r_tipo]").val() }, function (data) {
            if ($("input[name=r_tipo]").val() === "UVR" && data !== undefined) {
                maxi_plazo = parseFloat(data.valorTasa).toLocaleString(undefined, { maximumFractionDigits: 2 });
                $("#maxi_plazo").text(" = " + maxi_plazo);
                $("#maxi_mora").text(" = " + parseFloat(data.valorTasa * 1.5).toLocaleString(undefined, { maximumFractionDigits: 2 }));
            } else if ($("input[name=r_tipo]").val() === "PESOS" && data !== undefined) {
                maxi_plazo = parseFloat(data.maxIntRemunatorio + data.variacionUVR);
                $("#maxi_plazo").text(" = " + parseFloat(data.maxIntRemunatorio).toLocaleString(undefined, { maximumFractionDigits: 2 }) + " + " + parseFloat(data.variacionUVR).toLocaleString(undefined, { maximumFractionDigits: 2 }) + " = " + maxi_plazo.toLocaleString(undefined, { maximumFractionDigits: 2 }));
                $("#maxi_mora").text(" = " + parseFloat(maxi_plazo * 1.5).toLocaleString(undefined, { maximumFractionDigits: 2 }));
            }else {
                $("#maxi_plazo").text("");
                $("#maxi_mora").text("");
            }

        }, "json").fail(function (jqXHR, exception) {
            console.log(jqXHR.responseText);
        });
    });

    $("input[name=i_plazo]").focusout(function () {
        $("input[name=i_mora]").val($(this).val() * 1.5);
    });

    $("#btn_add_capitales").click(function () {
        var difference = 0, indice = 0;
        $("input[name='f_capitales[]']").each(function (index, obj) {
            difference = monthDiff(new Date($(this).val()), new Date($("input[name='f_capitales[]']").eq(indice + index + 1).val()));
            if (difference > 0) {
                var date = new Date(obj.value);
                date.setMonth(date.getMonth() + difference + 1);
                for (var i = difference - 1; i >= 0; i--) {
                    date.setMonth(date.getMonth() - 1);
                    $(this).parent().parent().after(
                        '<tr role="row"><td><i class="fa fa-trash delete" ></i></td><td><input type="date" max="2100-01-01" min="1900-01-01" name="f_capitales[]" value="'
                        + date.toFancyString()
                        + '" /></td><td><input type="number" class="no-numbar" step=".01" name="capitales[]" value="'
                        + $("input[name='capitales[]']").eq(indice + index).val() + '"/></td></tr>');
                }
            }
            indice += difference;
        });
    });

    $('#t_abonos table').on('focusout', 'input[name="seguro_abono[]"]:last', function () {
        tabla_abonos.row.add(['<i class="fa fa-trash delete"></i>','<input type="date" max="2100-01-01" min="1900-01-01" name="f_abono[]" />', '<input type="text" class="nuevo" name="pago_abono[]" />', '<input type="text" class="nuevo" name="seguro_abono[]" />']).draw();
        AutoNumeric.multiple('#t_abonos table tr:last-child .nuevo', autoNumericOptionsThousands);
        $('input[name="f_abono[]"]:last').focus();

    });
    $('#t_abonos tbody').on('click', 'td i.delete', function () {
        tabla_abonos.row($(this).parents('tr')).remove().draw();
    });
;

    $('#t_capitales table').on('focusout', 'input[name="capitales[]"]:last', function () {
        tabla_capitales.row.add(['<i class="fa fa-trash delete"></i>','<input type="date" max="2100-01-01" min="1900-01-01" name="f_capitales[]" />', '<input type="text" class="no-numbar" name="capitales[]" />']).draw();
        new AutoNumeric('#t_capitales table tr:last-child td:last-child input', autoNumericOptionsThousands);
        $('input[name="f_capitales[]"]:last').focus();

    });
    $('#t_capitales tbody').on('click', 'td i.delete', function () {
        tabla_capitales.row($(this).parents('tr')).remove().draw();
    });
    /**********************************
     ******* END COMMON FUNCTIONS *****
     **********************************
     * */

    var tabla_liquidacion = $("#tab_liquidacion table").DataTable({
        "paging": false,
        "scrollX": true,
        "scrollY": "60vh",
        "scrollCollapse": true,
        "ordering": true,
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
            { "data": "tasaAnual" },
            { "data": "tasaMaxima" },
            { "data": "intAplicado", render: $.fn.dataTable.render.number('.', ',', 2) },
            {
                "data": "interesNominal", render: function (data, type, row, meta) {
                    return parseFloat(data * 100).toLocaleString(undefined, { maximumFractionDigits: 5 }) + ' %';
                }
            },
            { "data": "capital", render: $.fn.dataTable.render.number('.', ',', 6, '$ ') },
            { "data": "capitalALiquidar", render: $.fn.dataTable.render.number('.', ',', 6, '$ ') },
            { "data": "abonos", render: $.fn.dataTable.render.number('.', ',', 2, '$ ') },
            { "data": "abonoCapitalUVR" },
            { "data": "interesAdeudadoMoraAcum" },
            { "data": "abonoIntCtePesos", render: $.fn.dataTable.render.number('.', ',', 2, '$ ') },
            { "data": "intPlazoPeriodo", render: $.fn.dataTable.render.number('.', ',', 2, '$ ') },
            { "data": "interesMoraPeriodo", render: $.fn.dataTable.render.number('.', ',', 2, '$ ') },
            { "data": "saldoInteresPlazoAcum", render: $.fn.dataTable.render.number('.', ',', 2, '$ ') },
            { "data": "saldoInteresMoraPesos", render: $.fn.dataTable.render.number('.', ',', 2, '$ ') },
            { "data": "subTotal", render: $.fn.dataTable.render.number('.', ',', 2, '$ ') },
        ]
    });

    var tabla_resumen = $("#tab_resumen table").DataTable({
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

    $("#frm_hipotecario").ajaxForm({
        beforeSubmit: function (arr, $form, options) {
            var date_contrato = new Date($("input[name=f_contrato]").val());
            var date_capital = new Date($("input[name=f_capital]").val());
            var date_exigencia = new Date($("input[name=f_exigibilidad]").val());
            var date_liquidacion = new Date($("input[name=f_liquidacion]").val());
            if ($("input[name=i_plazo]").val() >= maxi_plazo) {
                alert("El intéres remuneratorio excede el interés remuneratorio maximo");
                return false;
            }
            if ($("input[name=i_mora]").val() >= maxi_plazo * 1.5) {
                alert("El intéres remuneratorio excede el interés remuneratorio maximo");
                return false;
            }
        },
        success: function (data) {
            if (data !== undefined) {
                tabla_liquidacion.clear();
                tabla_liquidacion.rows.add(data.detalle_liquidacion).draw();
                tabla_resumen.clear();
                tabla_resumen.rows.add(data.resumen).draw();
                $('a[href="#tab_resumen"]').trigger("click");
                $('#guardar').removeAttr('disabled');
            }
        },
        dataType: "json",
        error: function (jqXHR, exception) { alert(jqXHR.responseJSON.message); }
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
                            tabla_abonos.row.add(['<i class="fa fa-trash delete"></i>','<input type="date" max="2100-01-01" min="1900-01-01" name="f_abono[]" />', '<input type="text" class="nuevo" name="pago_abono[]" />', '<input type="text" class="nuevo" name="seguro_abono[]" />']).draw();
                            AutoNumeric.multiple('#t_abonos table tr:last-child .nuevo', autoNumericOptionsThousands);
                            new AutoNumeric('#t_abonos table tr:last-child td:last-child input', autoNumericOptionsThousands);
                        }
                    else if (element === "f_capitales")
                        for (let i = 0; i < fields_to_create; i++) {
                            tabla_capitales.row.add(['<i class="fa fa-trash delete"></i>','<input type="date" max="2100-01-01" min="1900-01-01" name="f_capitales[]" />', '<input type="text" class="no-numbar" name="capitales[]" />']).draw();
                            new AutoNumeric('#t_capitales table tr:last-child td:last-child input', autoNumericOptionsThousands);
                        }

                    $('input[name="' + field_name + '"]').each(function (index, obj) {
                        $(this).hasClass('no-numbar') || $(this).hasClass('nuevo') ?
                            AutoNumeric.getAutoNumericElement(obj).set(json[element][index]) :
                            $(this).val(json[element][index]);
                    });

                } else {
                    if ($('textarea[name=' + element + ']').attr('rows') == '5') {
                        $('textarea[name=' + element + ']').val(json[element]);
                    }if ($('input[name=' + element + ']').hasClass('no-numbar'))
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

                    let field_name = element.concat('[]');
                    $('input[name="' + field_name + '"]').hasClass('no-numbar') ?
                        AutoNumeric.getAutoNumericElement('input[name="' + field_name + '"]').set(json[element]) :
                        $('input[name="' + field_name + '"]').val(json[element]);
                }
        }
        maxi();
    }

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
    //-- VALIDACIONES
    $('#t_abonos').on('focusout','input[name="f_abono[]"]', function() {
        //-- Procesos.js
        validarAbonosCapitales($('input[name="f_contrato"]'), $('input[name="f_liquidacion"]'), $(this));
    });
    $('#t_capitales').on('focusout','input[name="f_capitales[]"]', function() {
        //-- Procesos.js
        validarAbonosCapitales($('input[name="f_contrato"]'), $('input[name="f_liquidacion"]'), $(this));
    });
    //-- FIN VALIDACIONES

    function maxi() {
        $.post("/Liquidador/ObtenerTasas", { fecha: $("input[name=f_contrato]").val(), VIS: $('input[name=vis]:checked').val(), moneda: $("input[name=r_tipo]").val() }, function (data) {
            if ($("input[name=r_tipo]").val() === "UVR" && data !== undefined) {
                maxi_plazo = parseFloat(data.valorTasa).toLocaleString(undefined, { maximumFractionDigits: 2 });
                $("#maxi_plazo").text(" = " + maxi_plazo);
                $("#maxi_mora").text(" = " + parseFloat(data.valorTasa * 1.5).toLocaleString(undefined, { maximumFractionDigits: 2 }));
            } else if ($("input[name=r_tipo]").val() === "PESOS" && data !== undefined) {
                maxi_plazo = parseFloat(data.maxIntRemunatorio + data.variacionUVR);
                $("#maxi_plazo").text(" = " + parseFloat(data.maxIntRemunatorio).toLocaleString(undefined, { maximumFractionDigits: 2 }) + " + " + parseFloat(data.variacionUVR).toLocaleString(undefined, { maximumFractionDigits: 2 }) + " = " + maxi_plazo.toLocaleString(undefined, { maximumFractionDigits: 2 }));
                $("#maxi_mora").text(" = " + parseFloat(maxi_plazo * 1.5).toLocaleString(undefined, { maximumFractionDigits: 2 }));
            } else {
                $("#maxi_plazo").text("");
                $("#maxi_mora").text("");
            }

        }, "json").fail(function (jqXHR, exception) {
            console.log(jqXHR.responseText);
        });
    }
});