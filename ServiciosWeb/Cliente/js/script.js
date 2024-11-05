function mostrarSeccion(seccion) {
    document.querySelectorAll('.section').forEach(s => s.classList.add('d-none'));
    document.getElementById(seccion).classList.remove('d-none');
    if (seccion === 'consultar') {
        cargarServicios();
    }
}

async function cargarServicios() {
    await fetch('https://localhost:7130/api/Servicios')
        .then(response => response.json())
        .then(data => {
            const tablaServicios = document.getElementById('tablaServicios');
            tablaServicios.innerHTML = '';
            data.forEach(servicio => {
                const row = `<tr><td>${servicio.id}</td><td>${servicio.nombre}</td><td>${servicio.costo}</td><td>${servicio.enPromocion === 'S' ? 'Si' : 'No'}</td></tr>`;
                tablaServicios.insertAdjacentHTML('beforeend', row);
            });
        })
        .catch(error => console.error('Error al cargar los servicios:', error));
}

function cargarServicio(event) {
    event.preventDefault();
    const nombre = document.getElementById('nombreServicio').value;
    const costo = document.getElementById('costoServicio').value;
    const enPromocion = document.getElementById('enPromocion').checked ? 'S' : 'N';

    fetch('https://localhost:7130/api/Servicios', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ nombre, costo, enPromocion })
    })
    .then(response => {
        if (response.ok) {
            alert('Servicio agregado con exito');            
            document.getElementById('nombreServicio').value = '';
            document.getElementById('costoServicio').value = '';
            document.getElementById('enPromocion').checked = false;
            mostrarSeccion('consultar');
        } else {
            alert('Error al agregar el servicio');
        }
    })
    .catch(error => console.error('Error al agregar el servicio:', error));
}
