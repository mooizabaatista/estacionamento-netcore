var placa = document.getElementById("Placa");
var marca = document.getElementById("Marca");
var modelo = document.getElementById("Modelo");
var cor = document.getElementById("Cor");
var clienteId = document.getElementById("ClienteId");
var frmPostVeiculo = document.getElementById("frmPostVeiculo");
var btSubmit = document.getElementById("btSubmit");


btSubmit.addEventListener('click', function (e) {
    e.preventDefault();
    ValidaForm();
})


function ValidaForm() {
    if (placa.value == "" || marca.value == "" || modelo.value == "" || cor.value == "" || clienteId.value == "") {
        $.alert("Todos os campos são obrigatórios...", " ATENÇÃO! ");
        return false;
    }
    fetch('/Veiculos/GetVeiculoByPlaca?placa=' + placa.value)
        .then(x => x.json())
        .then(d => {
            if (d) {
                $.alert("Veículo já está cadastrado...", " ATENÇÃO! ");
            }
            else {
                $.alert("Veículo cadastrado com sucesso!", { type: 'success', title: 'Tudo certo!' });
                setTimeout(
                    function () {
                        frmPostVeiculo.submit();
                    }, 1500);
            }
        });
}

