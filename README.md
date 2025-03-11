# eShopContainers - Microservicios con .NET y Docker

Este repositorio contiene la implementación del proyecto **eShopContainers**, un ejemplo de arquitectura de microservicios desarrollada por Microsoft, basada en .NET y Docker.

## 🛠️ Tecnologías Utilizadas

- **.NET** (ASP.NET Core, Entity Framework Core)
- **Docker y Kubernetes**
- **SQL Server**
- **RabbitMQ** (mensajería y eventos)
- **Redis** (caché)
- **SignalR** (comunicación en tiempo real)
- **Azure DevOps** (CI/CD)

## 📦 Estructura del Proyecto

```
📂 eShopContainers
 ├── 📂 src                 # Código fuente de los microservicios
 │   ├── 📂 Services        # Microservicios principales
 │   ├── 📂 Web             # Aplicaciones frontend
 │   ├── 📂 BuildingBlocks # Librerías compartidas
 ├── 📂 docker             # Configuración de contenedores
 ├── 📂 k8s                # Configuración para Kubernetes
 ├── 📄 docker-compose.yml  # Configuración para levantar los servicios
 ├── 📄 README.md           # Documentación del proyecto
```

## 🚀 Cómo Ejecutar el Proyecto

### Requisitos Previos

- **Docker y Docker Compose**
- **.NET SDK** (versión recomendada en el `global.json` del proyecto)
- **Visual Studio 2022** o **VS Code** (opcional)

### Pasos para Levantar los Servicios

1. Clona este repositorio:
   ```bash
   git clone https://github.com/tu_usuario/eshopcontainers.git
   cd eshopcontainers
   ```
2. Construye y levanta los servicios con Docker Compose:
   ```bash
   docker-compose up -d
   ```
3. Accede a la aplicación web en tu navegador:
   ```
   http://localhost:5100
   ```

## 📜 Principales Microservicios

| Servicio        | Descripción                                      | Puerto |
|----------------|--------------------------------------------------|--------|
| Basket.API     | Carrito de compras                              | 5102   |
| Catalog.API    | Catálogo de productos                           | 5101   |
| Identity.API   | Autenticación y autorización                    | 5105   |
| Ordering.API   | Gestión de pedidos                              | 5103   |
| WebSPA         | Aplicación web en Angular/React                 | 5100   |

## 🛠️ Desarrollo y Contribuciones

Si deseas contribuir, por favor:
1. Crea un **fork** del repositorio.
2. Crea una rama con tu nueva funcionalidad:
   ```bash
   git checkout -b feature/nueva-funcionalidad
   ```
3. Realiza tus cambios y haz un commit:
   ```bash
   git commit -m "Agregada nueva funcionalidad X"
   ```
4. Envía un **pull request**.

## 📄 Licencia

Este proyecto está bajo la licencia **MIT**. Consulta el archivo `LICENSE` para más detalles.

