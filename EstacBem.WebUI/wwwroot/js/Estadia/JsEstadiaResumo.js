var bolsaoId = document.getElementById("BolsaoId")
var btSubmit = document.getElementById("btSubmit")
var frmPostEstadia = document.getElementById("frmPostEstadia");
var veiculoId = document.getElementById("VeiculoId");
var statusId = document.getElementById("statusId");
var veiculoPlaca = document.getElementById("veiculo_Placa");




btSubmit.addEventListener('click', function (e) {
    e.preventDefault();
    VerificaForm();

})


function VerificaForm() {
    if (bolsaoId.value == "" || statusId.value == "") {
        $.alert("Preencha todos os campos...", " ATENÇÃO! ");
        bolsaoId.value = "";
        statusId.value = "";
    }

    else {
        frmPostEstadia.submit();
    }
}

bolsaoId.addEventListener("change", (e) => {
    var bolsaoId = e.target.value
    var qtd = 0;
    
    fetch("/Bolsoes/GetBolsaoInfo/" + bolsaoId)
        .then(x => x.json())
        .then(d => {
            if (d.qtd > 0 && d.qtd < 2) {
                $.alert("Só resta uma vaga...", " ATENÇÃO! ");
            } else if (d.qtd == 0) {
                $.alert("Esta área está lotada...", " ATENÇÃO! ");
                document.getElementById("BolsaoId").selectedIndex = "";
            }
        });
});



