const txtFiltro = document.getElementById("txtFiltro");
const fecharEstadia = document.getElementById("fecharEstadia");



if (fecharEstadia) {
    fecharEstadia.addEventListener("click", (e) => {
        if (confirm('Tem certeza que deseja fechar a estadia?')) {

            var estadiaId = e.currentTarget.getAttribute('idKey')

            fetch('/Estadias/FecharEstadia?estadiaId=' + estadiaId, {
                method: 'POST'
            })
                .then(x => x.json())
                .then(dados => {
                    if (dados) {
                        window.location = '/Estadias/Total?estadiaId=' + estadiaId;
                    }
                });     
        }
        else {
            alert('nada')
        }
    })
}




// filtro
txtFiltro.addEventListener("keyup", (e) => {
    var valor = e.target.value.toLowerCase();
    for (let el of document.querySelectorAll('.filtrohide')) el.style.display = 'none';
    var allRows = document.getElementsByClassName('filtrohide');
    for (var i = 0; i < allRows.length; i++) {
        var key = allRows[i].getAttribute("filtro");
            
        if (allRows[i].getAttribute("filtro").indexOf(valor) >= 0)
            allRows[i].style.display = 'table-row';
    }
})
