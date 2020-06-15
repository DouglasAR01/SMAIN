# SMAIN
El api dispone de una sección 'api/admin/..' orientada a la gestión de las cuentas por parte de los administradores, y de una sección 'api/user/..' para los usuarios.
Por simplicidad, el rol 'administrador' podrá ser una propiedad role de los usuarios como "a", y los no administradores como "u".
Se utiliza JWT para la autenticación y autorización a los endpoints securizados.
Además las contraseñas fueron cifrandas de texto plano a un hash unico apartir del algoritmo de cifrado SHA512 y una cadena aleatoria introducida la principio del texto plano comúnmente llamado como SALT.

# Dependencias e instalación
El proyecto se encuentra alojado en la carpeta MicroServicio y en la carpeta Doc se encuentra el archivo .sql con la estructura y algunos datos base para realizar pruebas.
1. Backend
  1. 1. Microsoft.AspNetCore.App==2.1.1
  1. 2. Microsoft.AspNetCore.Razor.Design==2.1.1
  1. 3. Microsoft.VisualStudio.Web.CodeGeneration.Design==2.1.10
  1. 4. Pomelo.EntityFrameworkCore.MySql==2.1.0
  1. 5. Swashbuckle.AspNetCore==5.4.1
  1. 6. Swashbuckle.AspNetCore.Filters==5.1.1

3. DB
 3. 1. mySQL==4.9
