$(document).ready(function () {
    $('#convertionToCOP').ajaxForm({
        dataType: "json",
        success: function (data) {
            if (data != null) {
                $('#result').text(parseFloat(data).toLocaleString(undefined, { maximumFractionDigits: 2 }));
            } else
                alert("ERROR: no se encontró registro con esa fecha.");
        },
        error: function (jqXHR) { alert(jqXHR.responseJSON.message); }
    });

    $('#convertionUVRPesos').ajaxForm({
        dataType: "json",
        success: function (data) {
            if (data != null) {
                $('#result').text(parseFloat(data.conversion).toLocaleString(undefined, { maximumFractionDigits: 2 }));
                $('#tasa').text(parseFloat(data.tasa).toLocaleString(undefined, { maximumFractionDigits: 4 }));
            } else
                alert("ERROR: no se encontró registro con esa fecha.");
        },
        error: function (jqXHR) { alert(jqXHR.responseJSON.message); }
    });

    $('#convertionUPACPesos').ajaxForm({
        dataType: "json",
        success: function (data) {
            if (data != null)
                $('#result').text(parseFloat(data).toLocaleString(undefined, { maximumFractionDigits: 2 }));
            else
                alert("ERROR: no se encontró registro con esa fecha.");
        },
        error: function (jqXHR) { alert(jqXHR.responseJSON.message); }
    });

    $('#btn_upacuvr').click(function () {
        var result;

        if ($("input[value=uvr]").is(':checked'))
            result = $("input[type=number]").val() * 160.7749827;
        else
            result = $("input[type=number]").val() / 160.7749827;

        $("#result").text(parseFloat(result).toLocaleString(undefined, { maximumFractionDigits: 5 }));
    });

    $('#rbtn_upacuvr').click(function () {
        $("#result").text("");
        $("input[type=number]").val("");
    });

    $('#nominal').click(function () {
        $("#formulaEE").hide();
        $("#formulaNE").show();
        $("#base").hide();
        $("#resultados").hide();
        $("#resultado").show();
        $("#tasaN").show();
        $("#tasaE").hide();
    });
    $('#efectiva').click(function () {
        $("#formulaNE").hide();
        $("#formulaEE").show();
        $("#base").show();
        $("#resultados").show();
        $("#resultado").hide();
        $("#tasaN").hide();
        $("#tasaE").show();
    });
    $('#efectiva1').click(function () {
        $("#formulaNE").hide();
        $("#formulaEE").show();
        $("#base").show();
        $("#resultados").show();
        $("#resultado").hide();
        $("#tasaN").hide();
        $("#tasaE").show();
    });
    

    $("#convertionEA").ajaxForm(function (data) {
        if (data.p == "anual") {
            $("#result3").text(parseFloat(data.ea).toLocaleString(undefined, { maximumFractionDigits: 4 }));
        }
        else {
            $("#result").text(parseFloat(data.ea).toLocaleString(undefined, { maximumFractionDigits: 4 }));
            $("#result2").text(parseFloat(data.eA2).toLocaleString(undefined, { maximumFractionDigits: 4 }));
        }
       
    });
});