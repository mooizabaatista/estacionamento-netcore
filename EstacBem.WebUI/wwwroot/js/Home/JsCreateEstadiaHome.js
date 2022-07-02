var frmModalCadastroEstadia = document.getElementById("frmModalCadastroEstadia");
var placa = document.getElementById("placa");
var btSubmit = document.getElementById("btSubmit");
var frmCadastroEstadia = document.getElementById("frmCadastroEstadia");

if (btSubmit) {
    btSubmit.addEventListener('click', function (e) {
        e.preventDefault();
        validaForm2();
    });
}

function validaForm2() {

    if (placa.value == "") {
        $.alert("Preencha a placa...", " ATENÇÃO! ");
        return false;
    }

    Promise.all([
        fetch('/Home/HasVeiculoEstacionado?placa=' + placa.value).then(value => value.json()),
        fetch('/Home/HasVeiculoCadastrado?placa=' + placa.value).then(value => value.json())
    ])
        .then((value) => {
            if (value[0]) {
                $.alert("Já existe um veículo estacionado...", " ATENÇÃO! ");
            } else if (!value[1]) {
                $.alert("Veículo não está cadastrado....", " ATENÇÃO! ");
            } else {
                frmCadastroEstadia.submit();
            }
        })
        .catch((err) => {
            console.log(err);
        });
}
