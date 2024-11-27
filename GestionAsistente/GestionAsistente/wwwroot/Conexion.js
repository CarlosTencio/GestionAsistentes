function updateConnectionStatus() {
    const statusElement = document.getElementById('connectionStatus');
    const alertElement = document.getElementById('connectionAlert');

    if (navigator.onLine) {
        
        statusElement.style.color = "green";

        // Ocultar el mensaje de alerta si hay conexión
        alertElement.style.display = "none";
    } else {
    
        statusElement.style.color = "red";

        // Mostrar el mensaje de alerta si no hay conexión
        alertElement.style.display = "block";
    }
}

// Llamar a la función al cargar el script
updateConnectionStatus();

// Detectar cambios en el estado de la conexión
window.addEventListener('online', updateConnectionStatus);
window.addEventListener('offline', updateConnectionStatus);
