# AdminTiendasTech

Desarrollar una aplicación web gratuita, hospedada en Microsoft Azure, que permita a los tenderos gestionar su inventario, registrar ventas, y almacenar información clave (productos, proveedores, ventas) para facilitar la toma de decisiones informadas sobre qué comprar y cuándo reabastecer sus inventarios. La aplicación estará construida utilizando .NET como plataforma de desarrollo, aprovechando las capacidades de la nube de Azure para asegurar escalabilidad, disponibilidad y seguridad en el manejo de los datos.
Arquitectura de la Solución en Azure:
Backend en Azure:
•	Azure App Services: Se utilizará para alojar la aplicación web en un entorno escalable y gestionado, facilitando su despliegue y mantenimiento.
•	Azure SQL Database: La información de inventarios, ventas, y proveedores se almacenará en una base de datos relacional escalable y segura, que permitirá almacenar grandes volúmenes de datos de manera estructurada.
•	Azure Functions: Se implementarán funciones de backend para manejar las notificaciones automáticas sobre pagos a proveedores y productos agotados, con la capacidad de programarse y ejecutarse según los datos registrados.
Frontend:
•	Se desarrollará en ASP.NET Core MVC para la gestión de la interfaz de usuario. La aplicación será responsiva, permitiendo acceso desde dispositivos de escritorio y móviles, lo que es crucial para los tenderos en el campo.
Integración de Notificaciones:
•	Azure Logic Apps o Azure Service Bus: Para gestionar las notificaciones de manera automática, alertando a los tenderos cuando deban pagar a sus proveedores o cuando un producto esté cerca de agotarse.
•	Notificaciones vía correo electrónico y dentro de la propia plataforma.
Autenticación y Seguridad:
•	Azure Active Directory B2C: Para gestionar de forma segura el acceso a la aplicación, proporcionando autenticación de usuarios a través de cuentas de Google, Facebook, o cuentas personalizadas.
Desarrollo del Código en .NET:
•	Se utilizarán los servicios de .NET 6 para la creación de la aplicación web.
•	Entity Framework Core se empleará para la comunicación con la base de datos en Azure SQL, lo que facilitará las operaciones CRUD (Crear, Leer, Actualizar, Eliminar) de la información.
•	C# se utilizará como el lenguaje principal para la lógica de negocio y las funciones de backend, gestionando la interacción con la base de datos, los procesos de cálculo de inventarios, y las notificaciones.
•	Razor Pages para el desarrollo de las vistas web, asegurando una experiencia de usuario rápida y fluida..
