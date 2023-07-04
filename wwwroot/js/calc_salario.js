$(document).ready(function () {
    AutoNumeric.multiple('.no-numbar', autoNumericOptionsThousands); //- Se le da el formato a los inputs
	var valorHora = 0;
	
	var tbl_cesantias = $("#tbl_cesantias").DataTable({
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
            { "data": "numero" },
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
            { "data": "dias" },
            { "data": "salarioPromedio", render: $.fn.dataTable.render.number('.', ',', 2) },
            { "data": "cesantias", render: $.fn.dataTable.render.number('.', ',', 2) },
            { "data": "intereses", render: $.fn.dataTable.render.number('.', ',', 2) },
            { "data": "rendimiento" },
            {
                "data": null, render: function (data) {
                    return (data.cesantias + data.intereses).toFixed(2);
                }
            }
        ]
    });

    var tbl_primas = $("#tbl_primas").DataTable({
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
            { "data": "numero" },
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
            { "data": "dias" },
            { "data": "salarioPromedio", render: $.fn.dataTable.render.number('.', ',', 2) },
            { "data": "primas", render: $.fn.dataTable.render.number('.', ',', 2) },
        ]
    });

    var tbl_vacaciones = $("#tbl_vacaciones").DataTable({
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
            { "data": "numero" },
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
            { "data": "dias" },
            { "data": "salarioPromedio", render: $.fn.dataTable.render.number('.', ',', 2) },
            { "data": "vacaciones", render: $.fn.dataTable.render.number('.', ',', 2) },
        ]
    });

    $("#frm_cesantias").ajaxForm({
        url: "/Laboral/Cesantias",
        type: "post",
        success: function (data) {
            tbl_cesantias.clear();
            tbl_cesantias.rows.add(data).draw();
        },
        dataType: "json"
    });

    $("#frm_primas").ajaxForm({
        url: "/Laboral/Primas",
        type: "post",
        success: function (data) {
            tbl_primas.clear();
            tbl_primas.rows.add(data).draw();
        },
        dataType: "json"
    });

    $("#frm_vacaciones").ajaxForm({
        url: "/Laboral/Vacaciones",
        type: "post",
        success: function (data) {
            tbl_vacaciones.clear();
            tbl_vacaciones.rows.add(data).draw();
        },
        dataType: "json"
    });
	
    function sumarTotal() {
		var sum = 0;
        $("#tabla_calculadora tbody tr td span").each(function () {
            sum += Number($(this).text());
        });
		
		$("#total span").text(sum.toFixed(2));
    }

    $("input[name=salario]").change(function () {
        $("#total span").text("");
        $("#basico span").text((Number($(this).val())/Number($("select[name=liquidar]").val())).toFixed(2));
		
		valorHora = Number($(this).val())/Number($("select[name=liquidar] option:selected").attr("data-hours"));
		
		$("#hr_diurna span").text((valorHora*1.25).toFixed(2));
		$("#hr_nocturna span").text((valorHora*1.75).toFixed(2));
		$("#hr_fdiurna span").text((valorHora*2.25).toFixed(2));
		$("#hr_fnocturna span").text((valorHora*2.75).toFixed(2));
		$("#hr_recargo span").text((valorHora*0.35).toFixed(2));
		
		$("#he_diurna span").text((Number($("input[name=he_diurna]").val())*valorHora*1.25).toFixed(2));
		$("#he_nocturna span").text((Number($("input[name=he_nocturna]").val())*valorHora*1.75).toFixed(2));
		$("#he_fdiurna span").text((Number($("input[name=he_fdiurna]").val())*valorHora*2.25).toFixed(2));
		$("#he_fnocturna span").text((Number($("input[name=he_fnocturna]").val())*valorHora*2.75).toFixed(2));
		$("#recargo_nocturno span").text((Number($("input[name=he_recargo]").val())*valorHora*0.35).toFixed(2));
		
        sumarTotal();
    });

    $("input[name=subsidio]").change(function () {
        $("#total span").text("");
        $("#subsidio span").text((Number($(this).val())/Number($("select[name=liquidar]").val())).toFixed(2));
        sumarTotal();
    });
	
	$("select[name=liquidar]").change( function() {
		var salario = Number($("input[name=salario]").val());
		var subsidio = Number($("input[name=subsidio]").val());
		
		$("#total span").text("");
		$("#basico span").text((salario/Number($(this).val())).toFixed(2));
		$("#subsidio span").text((subsidio/Number($(this).val())).toFixed(2));
        sumarTotal();
	});
	
	$("input[name=horas]").change( function() {
		$("#basico span").text((valorHora*Number($(this).val())).toFixed(2));
		$("#subsidio span").text((Number($("input[name=subsidio]").val())/240*Number($(this).val())).toFixed(2));
		sumarTotal();
	});
	
	$("input[name=he_diurna]").change( function(){
		$("#he_diurna span").text((Number($(this).val())*valorHora*1.25).toFixed(2));
		sumarTotal();
	});
	
	$("input[name=he_nocturna]").change( function(){
		$("#he_nocturna span").text((Number($(this).val())*valorHora*1.75).toFixed(2));
		sumarTotal();
	});
	
	$("input[name=he_fdiurna]").change( function(){
		$("#he_fdiurna span").text((Number($(this).val())*valorHora*2.25).toFixed(2));
		sumarTotal();
	});
	
	$("input[name=he_fnocturna]").change( function(){
		$("#he_fnocturna span").text((Number($(this).val())*valorHora*2.75).toFixed(2));
		sumarTotal();
	});
	
	$("input[name=he_recargo]").change( function(){
		$("#recargo_nocturno span").text((Number($(this).val())*valorHora*0.35).toFixed(2));
		sumarTotal();
	});
	
	$("#btn_agregar").click( function () {
		$("#t_liquidacion").find('tr').each( function() {
			var trow = $(this);
			
			if(trow.index() === 1)
				trow.append('<td>'+$("input[name=f_inicial]").val()+'</td>');
			else if(trow.index() === 2)
				trow.append('<td>'+$("input[name=f_final]").val()+'</td>');
			else if(trow.index() === 3)
				trow.append('<td>'+$("input[name=salario]").val()+'</td>');
			else if(trow.index() === 4)
				trow.append('<td>'+$("input[name=subsidio]").val()+'</td>');
			else if(trow.index() === 5)
				trow.append('<td>'+$("input[name=horas]").val()+'</td>');
			else if(trow.index() === 6)
				trow.append('<td>'+$("input[name=he_diurna]").val()+'</td>');
			else if(trow.index() === 7)
				trow.append('<td>'+$("input[name=he_nocturna]").val()+'</td>');
			else if(trow.index() === 8)
				trow.append('<td>'+$("input[name=he_fdiurna]").val()+'</td>');
			else if(trow.index() === 9)
				trow.append('<td>'+$("input[name=he_fnocturna]").val()+'</td>');
			else if(trow.index() === 10)
				trow.append('<td>'+$("input[name=he_recargo]").val()+'</td>');
			else if(trow.index() === 11)
				trow.append('<td>'+$("#basico").text()+'</td>');
			else if(trow.index() === 12)
				trow.append('<td>'+$("#subsidio").text()+'</td>');
			else if(trow.index() === 13)
				trow.append('<td>'+$("#he_diurna").text()+'</td>');
			else if(trow.index() === 14)
				trow.append('<td>'+$("#he_nocturna").text()+'</td>');
			else if(trow.index() === 15)
				trow.append('<td>'+$("#he_fdiurna").text()+'</td>');
			else if(trow.index() === 16)
				trow.append('<td>'+$("#he_fnocturna").text()+'</td>');
			else if(trow.index() === 17)
				trow.append('<td>'+$("#recargo_nocturno").text()+'</td>');
		});
	});
});