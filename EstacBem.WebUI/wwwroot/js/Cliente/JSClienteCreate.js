var clienteCpf = document.getElementById("CPF");
var clienteNome = document.getElementById("Nome");
var clienteSobrenome = document.getElementById("SobreNome");
var btSubmit = document.getElementById("btSubmit");
var frmPostCliente = document.getElementById("frmPostCliente");




btSubmit.addEventListener('click', function (e) {
    e.preventDefault();
    validaForm();
});

function validaForm() {

    if (clienteCpf.value == "" || clienteNome.value == "" || clienteSobrenome.value == "") {
        $.alert("Todos os campos são obrigatórios....", " ATENÇÃO! ");
        return false;
    }
    fetch('/Clientes/GetClienteByCPF?cpf=' + clienteCpf.value)
        .then(x => x.json())
        .then(d => {
            if (d) {
                $.alert("Cliente já está cadastrado...", " ATENÇÃO! ");
            }
            else {
                $.alert("Cliente cadastrado com sucesso!", { type: 'success', title: 'Tudo certo!!'});
                setTimeout(
                    function () {
                        frmPostCliente.submit();
                    }, 1500);
            }
        });
}