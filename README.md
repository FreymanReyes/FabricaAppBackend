# FabricaAppBackend

🛠️ API REST desarrollada en C# con ASP.NET Core para la gestión de usuarios, productos y pedidos.  
Incluye autenticación, operaciones CRUD y lógica de negocio. Pensado como base para proyectos más robustos o con arquitectura en capas.

---

## 🚀 Tecnologías Utilizadas

- ASP.NET Core
- Entity Framework Core
- SQL Server
- JWT para autenticación
- Swagger para documentación de la API

---

## ⚙️ Requisitos Previos

- [.NET 6 SDK o superior](https://dotnet.microsoft.com/en-us/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (o Azure SQL)
- Visual Studio 2022 o Visual Studio Code
- (Opcional) Postman o cualquier herramienta para probar endpoints

---

## 🛠️ Pasos para la Instalación

1. **Clona el repositorio:**

```bash
git clone https://github.com/FreymanReyes/FabricaAppBackend.git
cd FabricaAppBackend

2. **Configura la cadena de conexión en appsettings.json:

"ConnectionStrings": {
  "DefaultConnection": "Server=TU_SERVIDOR;Database=FabricaAppDB;Trusted_Connection=True;MultipleActiveResultSets=true"
}
⚠️ Asegúrate de tener acceso al servidor SQL y permisos para crear bases de datos/tablas.

3. **Aplica las migraciones y crea la base de datos:

dotnet ef database update

Si no tienes las herramientas de EF instaladas:

dotnet tool install --global dotnet-ef

4. **Ejecuta la API:

dotnet run

5. **Abre en el navegador:

https://localhost:5001/swagger

🔐 Consideraciones de Seguridad
Asegúrate de configurar correctamente la clave JWT en appsettings.json.

Usa HTTPS en entornos de producción.

Implementa CORS si vas a consumir desde un frontend separado.

🧪 Endpoints y Pruebas
Puedes probar los endpoints disponibles a través de Swagger:
👉 https://localhost:5001/swagger

O también usando Postman / Insomnia.

📦 Arquitectura
El proyecto sigue una arquitectura en capas básica:

📦 FabricaAppBackend
 ┣ 📂Aplicacion
 ┣ 📂Dominio
 ┣ 📂FabricaApi
 ┣ 📂Persistencia
 ┣ 📂Seguridad