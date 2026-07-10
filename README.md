# Eco de las Sombras

**Eco de las Sombras** es un videojuego de terror psicológico y exploración en primera persona desarrollado en el motor gráfico Unity. La experiencia sumerge al jugador en el rol de Clara, una mujer adulta que regresa a su antigua residencia familiar para confrontar y resolver los traumas reprimidos de su infancia.

A diferencia de las propuestas convencionales del género, el software prescinde de monstruos corpóreos u obstáculos físicos; la amenaza latente es una simulación de su propia psicosis, manifestada mediante un denso diseño de audio tridimensional y mecánicas adaptativas de control.

---

## Mecánicas Clave del Sistema

* **Bucle de Puzle Central:** Localizar y transportar de forma segura **6 velas rituales** esparcidas proceduralmente por el escenario hacia un altar central para consumar la liberación emocional de Clara.
* **Manifestación Invisible (FSM):** Un agente abstracto e invisible navega por el mapa (`NavMesh`) rastreando la proximidad del jugador. No posee representación geométrica, pero delata su posición exclusivamente mediante el paisaje sonoro.
* **Atenuación Acústica de Audio 3D:** Integración del Unity Audio Mixer con curvas logarítmicas de atenuación tridimensional que rotan y se intensifican en base a la cercanía del peligro perceptual.
* **Degradación Motriz Procedural:** El pánico ataca directamente el sistema de locomoción, reduciendo la velocidad de marcha y carrera en un 50% e inyectando fluctuaciones de movimiento (*ruido matemático*) en las entradas del controlador cuando Clara sufre una crisis.

---

## Arquitectura y Patrones de Diseño

El backend del software se cimenta bajo la taxonomía de patrones clásicos de la ingeniería de software:
1. **Patrones de Comportamiento (Observador):** Manejo desacoplado de eventos globales. El script del altar notifica los cambios en el inventario de velas a los observadores (módulos de sonido, controlador de pánico e interfaz de usuario).
2. **Patrones de Comportamiento (Estado):** Una Máquina de Estados Finitos (FSM) que transmuta limpiamente las variables de control del avatar según su nivel de estrés (Estados: *Latente, Acoso, Crisis y Calma*).
3. **Patrón Estructural (Componente):** Acoplamiento nativo modular para dar soporte físico de navegación y emisión sonora al agente invisible del trauma.

---

## Técnicas de Optimización y Rendimiento

Diseñado para asegurar una tasa de refresco mínima y estable de **60+ FPS** en hardware de gama media:
* **Object Pooling:** Reutilización síncrona en memoria RAM de los componentes efímeros de audio y efectos de niebla de las crisis.
* **Batching (Static & Dynamic):** Agrupamiento de llamadas de dibujo (*Draw Calls*) para combinar la arquitectura estática de la casa y las texturas compartidas de las velas.
* **Occlusion Culling:** Horneado estático del mapa para desactivar la renderización de las habitaciones contiguas que se encuentran fuera de la oclusión del muro visualizado por la cámara.
* **Persistencia de Datos:** Modelo relacional estructurado serializado localmente (JSON) para almacenar la posición espacial de los assets y métricas clínicas de estrés.

---

## Tecnologías Utilizadas

* **Motor Gráfico:** Unity Engine (Versión LTS)
* **Lenguaje de Programación:** C# (.NET Core)
* **Sistemas Nativos:** NavMesh Components, Unity Audio Mixer, Canvas UI.
* **Esquema de Control:** Teclado (WASD + Shift + E) y Ratón Posicional.

---

*Desarrollado con fines académicos para la documentación detallada de sistemas interactivos.*
