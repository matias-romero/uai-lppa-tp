# uai-lppa-tp
Este proyecto fue presentado para el final de la materia **T109 37 - LENGUAJES DE PROGRAMACIÓN PARA LA ADMINISTRACIÓN** de la carrera Ing. en Sistemas Informáticos de la Universidad Abierta Interamericana.

El objetivo era mostrar un sitio web con funcionalidades básicas de Login/Logout que muestre diferentes pantallas según el rol del usuario (Autorización basada en roles) y un caso de uso funcional que es la solicitud de turnos en un calendario.
Las restricciones impuestas por la cátedra son:
* Debe utilizar controles WebForms
* La autenticación debe hacerse mediante Cookies
* (Opcional) Consumo de un servicio web

El código se encuentra divido en 3 proyectos codificados en C#. 
* SaludArTE.Entities 
  * Es la capa de entidades
  * Cada entidad no tiene comportamiento, solamente propiedades get/set
* SaludArTE.Data 
  * Ofrece las abstracciones necesarias para trabajar con el repositorio de datos mediante el objeto `UnitOfWorkHelper` a bajo nivel, a alto nivel se encapsula en un “Repository Pattern” por cada entidad importante.
  * Contiene los mappers para las diferentes entidades en el espacio de nombres `Mappers`
  * Encapsula la funcionalidad de integridad de los dígitos verificadores en el espacio de nombres `RedundancyCheck`
* SaludArTE (GUI)
  * Utiliza tecnología ASP.NET WebForms para la presentación (páginas .aspx)
  * Consume servicios web con los datos de la agenda con llamadas ajax siguiendo una arquitectura REST, representando recursos en notación JSON.
  * Tiene separada la lógica de negocios en el espacio de nombres BLL
  * Controla la autenticación mediante el módulo de ASP.NET FormsAuthentication
  * Controla la autorización a cada página según en qué carpeta se encuentran. 
    - `~/*` accesible a cualquier usuario identificado
    - `~/BackendPages/*` accesible sólo para usuarios con rol “backend”
    - `~/AdminPages/*` accesible sólo para usuarios con el rol “admin”
