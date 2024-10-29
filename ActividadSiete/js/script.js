const turnos = [
    { id: 1, fecha: "28-10-2024", hora: "10:30", cliente: "Ramiro Ibazeta" },
    { id: 2, fecha: "01-11-2024", hora: "12:15", cliente: "Lionel Messi" }, 
    { id: 3, fecha: "09-12-2024", hora: "11:30", cliente: "El Maligno" },
    { id: 4, fecha: "12-12-2024", hora: "10:00", cliente: "Franco Colapinto" }
];

function mostrarSeccion(seccion) {
    document.querySelectorAll('.section').forEach(sec => sec.classList.add('d-none'))
    document.getElementById(seccion).classList.remove('d-none')
    if (seccion === 'consultar') cargarTurnos()
    if (seccion === 'inicio') cargarInicio()
}

function cargarTurnos() {
    const tabla = document.getElementById("tablaTurnos")
    tabla.innerHTML = ""
    turnos.forEach(turno => {
        tabla.innerHTML += `<tr><td>${turno.id}</td><td>${turno.fecha}</td><td>${turno.hora}</td><td>${turno.cliente}</td></tr>`;
    })
}

function cargarInicio() {
    const $tabla = document.getElementById("tablaInicio");
    $tabla.innerHTML = "";

    const clientes = {};

    for (let i = 0; i < turnos.length; i++) {
        const cliente = turnos[i].cliente;
        if (clientes[cliente]) {
            clientes[cliente] += 1;
        } else {
            clientes[cliente] = 1;
        }
    }

    for (const cliente in clientes) {
        $tabla.innerHTML += `<tr><td>${cliente}</td><td>${clientes[cliente]}</td></tr>`;
    }
}


function agregarTurno(event) {
    event.preventDefault();
    const $elegido = document.getElementById("elegirCliente")
    const id = $elegido.value
    const nombre = $elegido.options[$elegido.selectedIndex].text

    const fecha = document.getElementById("fecha").value
    const hora = document.getElementById("hora").value

    if (validar()){
        turnos.push({ id, fecha, hora, cliente: nombre})
        alert("Turno agregado")
        mostrarSeccion('consultar')
    }else{
        alert("Error al agregar turno")
    }   
}

function validar() {
    const $elegido = document.getElementById("elegirCliente");
    const $id = $elegido.value;
    let aux = true;

    if ($id < 1) {
        aux = false;
        alert("Seleccionar cliente");
        return aux;
    }

    const $fecha = document.getElementById("fecha").value;
    const hoy = new Date();
    hoy.setHours(0, 0, 0, 0);

    const fechaSeleccionada = new Date($fecha);

    if (fechaSeleccionada < hoy) {
        aux = false;
        alert("El turno debe ser a partir de maniana");
        return aux;
    }

    return aux;
}