var btFinalizarEstadia = document.getElementById("btFinalizarEstadia");
var statusId = document.getElementById("statusId");
var estadiaId = document.getElementById("estadiaId");
var frmPostEstadia = document.getElementById("frmPostEstadia");

if (btFinalizarEstadia) {
    btFinalizarEstadia.addEventListener('click', (e) => {

        //var estadiaId = e.currentTarget.getAttribute('idKey')
        e.preventDefault();
        if (statusId.value == 3) {
            $.alert("O pagamento não pode ser pendente...", " ATENÇÃO! ");
        } else {
            frmPostEstadia.submit();
        }
    })
}