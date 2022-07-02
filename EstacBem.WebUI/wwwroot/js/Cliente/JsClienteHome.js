const txtFiltroCPF = document.getElementById("txtFiltroCPF");

if (txtFiltroCPF) {

    // filtro
    txtFiltroCPF.addEventListener("keyup", (e) => {
        var valor = e.target.value.toLowerCase();
        for (let el of document.querySelectorAll('.filtrohide')) el.style.display = 'none';
        var allRows = document.getElementsByClassName('filtrohide');
        for (var i = 0; i < allRows.length; i++) {

            var key = allRows[i].getAttribute("filtro");

            if (allRows[i].getAttribute("filtro").indexOf(valor) >= 0)
                allRows[i].style.display = 'table-row';
        }
    })
}