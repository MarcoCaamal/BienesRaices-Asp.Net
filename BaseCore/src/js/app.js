
document.addEventListener('DOMContentLoaded', function () {
    eventListers();

    darkMode();
});

function eventListers() {
    const mobileMenu = document.querySelector('.mobile-menu');

    mobileMenu.addEventListener('click', navegacionResponsive);

    //Muestra campos condicionales
    const metodoContacto = document.querySelectorAll('input[name="OpcionContacto"]');
    metodoContacto.forEach(input => input.addEventListener('click', mostrarMetodosContacto));
}

function mostrarMetodosContacto(e) {
    const contactoDiv = document.getElementById('contacto');
    if (e.target.value === 'telefono') {
        contactoDiv.innerHTML = `
            <label for="telefono">N\u00famero De Tel\u00e9fono:</label>
            <input type="tel" name="telefono" placeholder="Tu Tel\u00e9fono" id="telefono"/>
            <p>Elija la fecha y la hora en la que desea ser contactado</p>
            <label for="fecha">Fecha:</label>
            <input name="Fecha" type="date" id="fecha" />
            <label for="hora">Hora:</label>
            <input type="time" name="Hora" id="hora" min="09:00" max="18:00"/>
        `;
    }
    else {
        contactoDiv.innerHTML = `
            <label for="email">E-mail:</label>
            <input type="email" placeholder="Tu E-mail" name="email" id="email"/>
        `;
    }
}

function navegacionResponsive() {
    const navegacion = document.querySelector('.navegacion');

    navegacion.classList.toggle('mostrar');

    //if (navegacion.classList.contains('mostrar')) {
    //    navegacion.classList.remove('mostrar');
    //    return;
    //}

    //navegacion.classList.add('mostrar');
}

function darkMode() {

    const prefiereDarkMode = window.matchMedia('(prefers-color-scheme: dark)');

    if (prefiereDarkMode.matches) {
        document.body.classList.add('dark-mode');
    }
    else {
        document.body.classList.remove('dark-mode');
    }

    prefiereDarkMode.addEventListener('change', () => {
        if (prefiereDarkMode.matches) {
            document.body.classList.add('dark-mode');
        }
        else {
            document.body.classList.remove('dark-mode');
        }
    });

    const botonDarkMode = document.querySelector('.dark-mode-boton');

    botonDarkMode.addEventListener('click', function() {
        document.body.classList.toggle('dark-mode');
    });
}