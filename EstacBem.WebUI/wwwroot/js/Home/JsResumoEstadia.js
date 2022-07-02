var frmResumoEstadia = document.getElementById("frmResumoEstadia");
var placaResumo = document.getElementById("placaResumo");
var btSubmitResumo = document.getElementById("btSubmitResumo");


btSubmitResumo.addEventListener('click', function (e) {
    e.preventDefault();
    if (validaForm()) {
        debugger;
        fetch('/Home/HasVeiculoEstacionado?placa=' + placaResumo.value)
            .then(value => value.json())
            .then(dados => {
                if (dados == false) {
                    $.alert("Veículo sem estadia aberta...", " ATENÇÃO! ");
                }
                else {
                    frmResumoEstadia.submit();
                }
            });
    }
});

function validaForm() {
    if (placaResumo.value == "") {
        $.alert("Preencha a placa...", " ATENÇÃO! ");
        return false;
    }
    return true;
}
