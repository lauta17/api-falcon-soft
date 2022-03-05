# Falcon-Soft

# Tecnologias:
* [NET 6] 
* [EntityFrameworkCore]
* [SQL in memory]
* [XUnit for unit testing]
* [JWT for authentication]

# Arquitectura
*Hexagonal
*Clean code
![image](https://user-images.githubusercontent.com/41306563/156865443-e2cf4db2-e861-4af2-8342-b9504aba98ad.png)

# Layers
* Application: Business Logic
* Domain: Domain entities
* Infraestructure.Database: Database configurations
* Web: Controllers

# Como levantar la aplicacion:

Ejecutar la aplicacion y nos llevara a la siguiente url:
https://localhost:7106/swagger/index.html

Para authenticarse y poder probar la api:

1. Hacer un post a /api/security con el siguiente body:

| Config |  |
| ------ | ------ |
| Usuario | "Lautaro" |
| Password | "123" |

2. Agregar el jwtToken en Authorize.
3. Ya queda registrado el usuario para poder probar los endpoints

nota: Tambien se agrego una postman collection para pruebas Falcon-Soft.postman_collection.json
