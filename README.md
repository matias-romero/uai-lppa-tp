# uai-lppa-tp
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
