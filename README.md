# SeguridadApp
Mantenimiento empleados de un Deparamento de Seguridad

Aplicación que relaciona dos tablas, Agentes y Rangos, con una relación muchos a uno.

Desarrollo:
- C#
- MVC
- Materialize
- Fontawesom y materialIcons (en linea).
- Access como base de datos.

Se divide la capa Data Access Layer (DAL); para asignarle la responsabilidad al acceso de los datos. Utilizando Singleton como patron 
de diseño (se instancia una unica vez la conexion a la BD).

En la capa DAL se crea un helper que se encarga de hacer los query a la BD. Excepto en el caso del ActionResult Editar; en este envía
el query del update directamente usando OleDb ya que el metodo no funcionó para los parametros. Ver nota en el Metodo.

Los demas datos se van aclarando con comentarios dentro del codigo.

Repositorio de uso publico.


