# Taskify (Challenge)

Desarrollar una ToDo List. Puede ser tan simple o compleja como quieras (dentro de los límites considerados), 
eso lo decidís vos. Sólo vamos a darte algunas consideraciones más abajo 👇 👇 👇.

* Crear y eliminar una tarea.
* Marcar una tarea como completada.
* Establecer un deadline a cada tarea.
* Listar todas las tareas creadas.
* Ordenar lista de tareas por fecha.

## Para correr el proyecto:

- npm install
- npm run dev

### Considereciones:

- Asegurese de que el backend este corriendo
- Asegurese de que el puerto en el que esta corriendo la api sea el mismo que esta seteado como baseUrl en '/utils/customAxios.ts' ej:

```javascript
export const api = axios.create({
    baseURL: 'https://localhost:7257/api' // <- aqui
})

```