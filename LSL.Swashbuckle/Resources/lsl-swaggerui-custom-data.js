 $.getJSON("lsl-swaggerui-custom-data")
    .then(function(data) {
        window.lslCustomData = data;       
    });