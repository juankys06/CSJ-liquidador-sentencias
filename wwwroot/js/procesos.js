const autoNumericOptionsThousands = {
    digitGroupSeparator: '.',
    decimalCharacter: ',',
    unformatOnHover: false,
    unformatOnSubmit: true,
    currencySymbol: '$ '
};

var t_guardados = $('#guardados').DataTable({
    "paging": true,
    "ordering": false,
    "info": true,
    "searching": false,
    "language": {
        "decimal": ",",
        "thousands": ".",
        "lengthMenu": "Mostrar _MENU_ registros",
        "emptyTable": "La tabla no contiene registros",
        "zeroRecords": "No se encontraron registros",
        "infoEmpty": "Sin registros para mostrar.",
        "info": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_.",
        "infoFiltered": "(filtrado de un total de _MAX_ registros)",
        "paginate": {
            "first": "Inicio",
            "last": "Final",
            "next": "Siguiente",
            "previous": "Anterior"
        }
    },
    "columns": [
        {
            "data": null, render: function (data, type, row) {
                return '<a href="/Liquidador/Cargar?liquidacion=' + row.id + '">' + row.llaveproc + '</a>';
            }
        },
        { "data": "tipo" },
        {
            "data": "fecha", render: function (data, type, row, meta) {
                return new Date(data).toLocaleDateString("es-CO", { year: 'numeric', month: '2-digit', day: '2-digit', hour: "numeric", minute: "numeric" });
            }
        },
        { "data": "usuario" },
        {
            "data": "autoGuardar", render: function (data, type, row) {
                if (data)
                    return "Auto Guardado";
                else
                    return "";
            }
        }
    ]
});

/**
 * Convierte cualquier formulario a JSON.
 * @param {any} f
 */
function serializeToJSON(f) {
    var json = {};
    f.serializeArray().map(function (item) {
        if (item.name.includes('[]')) //-- Trata los arrays antes de subirlos, ya que dan problemas al deserializar
            item.name = item.name.replace('[]', '');
        if (json[item.name]) {
            if (typeof (json[item.name]) === "string") {
                json[item.name] = [json[item.name]];
            }
            if (item.value != "")
                json[item.name].push(item.value);
        } else {
            json[item.name] = item.value;
        }
    });

    return JSON.stringify(json);
}

/**
 * Validar que las fechas se encuentren dentro del intervalo propuesto de la liquidación
 * 
 * @param {*} oInicio Fecha de Inicio
 * @param {*} oFinal  Fecha Final
 * @param {*} oDate   Fecha a Evaluar
 */
function validarAbonosCapitales(oInicio, oFinal, oDate) {
    var inicio = new Date(oInicio.val());
    var final  = new Date(oFinal.val());
    var fecha  = new Date(oDate.val());

    if(isNaN(inicio)) {
        oInicio.focus();
        alert('Debe asignar la fecha de inicio de obligación.');
        return;
    }

    if(isNaN(final)){
        oFinal.focus();
        alert('Debe asignar la fecha de liquidación.');
        return;
    }

    if(fecha < inicio || fecha > final) {
        
        alert("LA FECHA DEBE ESTAR ENTRE\nLA FECHA INICIAL DE LA OBLIGACION Y LA FECHA DE LIQUIDACION");
        return;
    }
}

$(document).ready(function () {
    $.fn.dataTable.moment('DD/MM/YYYY'); //-- Para el formato de datatables con fechas
    var datos; //-- Para retener el array JSON
    var pos = 0; //-- Posicion del JSON

    $("#progressbar").progressbar({
        value: false
    });

    /**
     * Completa con los valores que retorna el servidor, los campos del formulario.
     * @param {JSON} proceso
     */
    function set_formValues(proceso) {
        $('input[name=completo]').val(proceso.completo);
        $('input[name=idProceso]').val(proceso.completo);
        $('input[name=tipo]').val(proceso.tipo);
        $('input[name=clase]').val(proceso.clase);
        $('input[name=descripcion]').val(proceso.descripcion);
        $('input[name=demandante]').val(proceso.demandante);
        $('input[name=demandado]').val(proceso.demandado);
        //-- Formulario
        $('input[name=ciudad]').val(proceso.ciudad);
        $('input[name=entidad]').val(proceso.entidad);
        $('input[name=especialidad]').val(proceso.especialidad);
        $('input[name=despacho]').val(proceso.despacho);
        $('input[name=numero]').val(proceso.numero);
    }

    /**
     * Llena la tabla dónde se muestra los procesos de liquidación guardados, con los datos devueltos por el servidor.
     * @param {any} guardados
     */
    function tablaGuardados(guardados) {
        t_guardados.clear();
        t_guardados.rows.add(guardados).draw();
    }

    /**
     * Al suscribir el formulario, chequear cuantos procesos retorna el servidor. Y los procesos de liquidación guardados.
     */
    $('#search_process').ajaxForm({
        beforeSubmit: function () { $('.progress').removeClass('oculto') },
        success: function (data) {
            $('.progress').addClass('oculto');
            datos = data; //-- Igualo la variable global de datos, a la data retornada
            pos = 0;

            set_formValues(data.procesos[pos]);
            data.guardados.length > 0 ? tablaGuardados(data.guardados[pos]) : t_guardados.clear().draw();

            if (data.procesos.length > 1) {
                $("input[value=Anterior]").removeAttr("disabled");
                $("input[value=Siguiente]").removeAttr("disabled");
            }
        },
        dataType: "json",
        error: function (jqXHR) {
            $('.progress').addClass('oculto');
            switch (jqXHR.status) {
                case 502:
                    //alert('Error al consultar el CPNU. Por favor, intente de nuevo más tarde.');
                    alert('Error al consultar el CPNU. Por favor, asegurese de que los datos que introducirá a continuación, sean correctos.');
                case 404:
                    window.open("/Home/CrearProceso", "_blank", "toolbar=no,scrollbars=no,resizable=no,top=100,left=300,width=650,height=410");
                    break;
                case 500:
                    alert('Error al consultar el CPNU. Por favor, asegurese de que los datos que introducirá a continuación, sean correctos.');
                    break;
                case 401:
                    alert('Su sesión ha expirado.!');
                    window.location.href = "/";
                    break;
                case 403:
                    alert('El usuario no está asignado al despacho al que pertenece el proceso.');
                    break;
                default:
                    alert('Error al guardar el proceso del CPNU a la base de datos. Contacte al administrador');
                    break;
            }
        }
    });

    /**
     * Reinicia la pantalla a su estado inicial, cuando se reinicia el formulario
     */
    $("#search_process").on('reset', function () {
        location.reload(true);
    });

    /**
     * Ir al anterior proceso.
     **/
    $("input[value=Anterior]").click(function () {
        if (datos.procesos[pos - 1]) {
            set_formValues(datos.procesos[--pos]);
            tablaGuardados(datos.guardados[pos]);
        } else
            alert("No hay más procesos.");
    });

    /**
     * Ir al siguiente proceso.
     **/
    $("input[value=Siguiente]").click(function () {
        if (datos.procesos[pos + 1]) {
            set_formValues(datos.procesos[++pos]);
            tablaGuardados(datos.guardados[pos]);
        } else
            alert("No hay más procesos");
    });

    /**
     * Rellena el campo del código del proceso, con los ceros faltantes para completar 5 dígitos.
     **/
    $("input[name=codProceso]").focusout(function () {
        if (this.value.length < 5) {
            temp = 5 - this.value.length;
            switch (temp) {
                case 1:
                    this.value = '0' + this.value;
                    break;
                case 2:
                    this.value = '00' + this.value;
                    break;
                case 3:
                    this.value = '000' + this.value;
                    break;
                case 4:
                    this.value = '0000' + this.value;
                    break;
            }
        }
    });

    /**
     * Reajusta las datatables cuando se muestran debido al bug mostrado en https://www.gyrocode.com/articles/jquery-datatables-column-width-issues-with-bootstrap-tabs/#example1
     * que usa https://datatables.net/reference/api/columns.adjust() como solución
     **/
    $('a[data-toggle="tab"]').on('shown.bs.tab', function () {
        $($.fn.dataTable.tables(true)).DataTable().columns.adjust();
    });
});