function serializeCostas(f) {
    var json = {};
    f.serializeArray().map(function (item) {
        if (item.name.includes('[]')) //-- Trata los arrays antes de subirlos, ya que dan problemas al deserializar
            item.name = item.name.replace('[]', '');
        if (json[item.name] || json[item.name] === '') {
            if (typeof (json[item.name]) === "string")
                json[item.name] = [json[item.name]];
            
            json[item.name].push(item.value);
        } else
            json[item.name] = item.value;
    });

    return JSON.stringify(json);
}

function deserializeToForm(JsonString){
    var json = JSON.parse(JsonString);
    
    for (var element in json) {
        if (json.hasOwnProperty(element))
            if(Array.isArray(json[element])){
                let fields_to_create = json[element].length - t_costas.page.info().recordsTotal;
                let field_name = element.concat('[]');

                if(fields_to_create > 0)
                    for (let i = 0; i < fields_to_create; i++) {
                        t_costas.row.add(['<input type="text" name="asunto[]" />', '<input type="text" class="no-numbar" name="valor[]" />']);
                        new AutoNumeric('#t_costas tbody tr:last-child td:last-child input', autoNumericOptionsThousands);
                    }
                else if(fields_to_create < 0)
                    for(var i = t_costas.page.info().recordsTotal - 1 ; i >= json[element].length ; i-- )
                        t_costas.row(i).remove();

                t_costas.draw();

                $('input[name="' + field_name + '"]').each(function (index, obj) {
                    $(this).hasClass('no-numbar') ?
                        AutoNumeric.getAutoNumericElement(obj).set(json[element][index]) :
                        $(this).val(json[element][index]);
                });
            }
    }

    sumar();
}

function sumar(){
    var suma = 0;
    var valores = $('.no-numbar');
    for(var i = 0 ; i < $(".no-numbar").length ; i++ )
        suma += AutoNumeric.getAutoNumericElement(valores[i]).getNumber();

    total1.set(suma); total2.set(suma);
}

$(document).ready(function() {
    AutoNumeric.multiple('.no-numbar', autoNumericOptionsThousands); //- Se le da el formato a los inputs
    [total1, total2] = AutoNumeric.multiple('.total', autoNumericOptionsThousands);
    
    t_costas = $("#t_costas").DataTable({
        "paging": false,
        "ordering": false,
        "info": false,
        "searching": false
    });

    $("#t_costas tbody").on( 'dblclick', "tr", function () {
        t_costas.row(this).remove().draw();
    });

    $('#t_costas').on('focusout', 'input[name="valor[]"]:last', function () {
        t_costas.row.add(['<input type="text" name="asunto[]" />', '<input type="text" class="no-numbar" name="valor[]" />']).draw();
        new AutoNumeric('#t_costas tbody tr:last-child td:last-child input', autoNumericOptionsThousands);
        $('input[name="asunto[]"]:last').focus();
    });
    
    $("#t_costas").on("change","input.no-numbar",function(){
        sumar();
    });
    
    $("#btn-guardar").click(function(){
        var formData = new FormData();
        formData.append('formulario', serializeCostas($('#frm_costas')));
        formData.append('tipo', 97);
    
        return $.ajax({
            beforeSend: function () {
                if (!$('#frm_costas')[0].checkValidity()) {
                    $('#frm_costas button[type=submit]').click();
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
            },
            error: function (jqXHR, exception, errorThrown) {
                jqXHR.responseJSON.type ? alert(jqXHR.responseJSON.type.Message) : alert(jqXHR.responseJSON.message);
            }
        });
    });

    // $("#btn-print").click(function(){
    //     var formData = new FormData();
    //     formData.append('formulario', serializeCostas($('#frm_costas')));
    
    //     window.location.href = "DescargarExcel?formulario=" + formData;
    // });
    
    $('#guardados').on('click', 'a', function (e) {
        e.preventDefault();
        $.post(this.getAttribute('href'), function (data) {
            deserializeToForm(data);
            alert("Datos cargados con éxito.");
        });
    });
});