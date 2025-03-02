# 📌 Company Management System

## Overview  
The **Company Management System** is a powerful and scalable web application designed to streamline **employee, department, and project management**. Built with **ASP.NET Core MVC**, it follows **Onion Architecture** and adheres to **Clean Code** principles, ensuring maintainability and flexibility.  

## 🏗️ Architecture  
The system is structured using **Onion Architecture** for better separation of concerns:  
- **Presentation Layer** – Handles UI and user interactions.  
- **Application Layer** – Contains business logic, services, and use cases.  
- **Infrastructure Layer** – Manages database access, external APIs, authentication, and repositories.  
- **Domain Layer** – Defines core business models, rules, and shared properties.  

## 🔑 Features  
✅ **Client & Server-Side Validations** (Ensuring data integrity and smooth user experience)  
✅ **Custom Validation Attributes** for specific property validation  
✅ **Image Upload Functionality** for employee profiles  
✅ **Role-Based Access Control** (Admin, Employee, and Manager)  
✅ **Authentication & Authorization** (ASP.NET Identity, Google Authentication, Forget Password)  
✅ **User Management** (Create, Update, Delete, Lock/Unlock Users, Role Assignments)  
✅ **Department & Employee Management** (Full CRUD operations)  
✅ **Project Management** (Assign Employees, Track Progress)  
✅ **Role-Specific Data Access**:  
   - **Managers** can view employees in their department and all projects.  
   - **Employees** can view only their own data.  
✅ **AutoMapper** for seamless object mapping  
✅ **Specification Design Pattern** for flexible query filtering  
✅ **Repository & Unit of Work Patterns** for database transactions  
✅ **Enum Support** (Gender, Relation)  
✅ **Clean & Maintainable Code** (SOLID Principles, Dependency Injection)  
✅ **Code-First Approach** for database management  

## 🔐 Authentication & Authorization  
- Uses **ASP.NET Identity** for secure authentication  
- **Google Authentication** integration for seamless login  
- Role-based access:  
   - **Admin**: Full control over all features  
   - **Manager**: Manages employees within their department and accesses all projects  
   - **Employee**: Can only view personal data  

## 📦 Tech Stack  
- **ASP.NET Core MVC**  
- **Entity Framework Core (Code-First)**  
- **SQL Server**  
- **Bootstrap & jQuery**  
- **AJAX for Asynchronous Operations**  
- **Fluent Validation & Data Annotations** for robust validation  
- **Specification Design Pattern** for optimized queries  
- **Repository Pattern & Unit of Work** for better database management  
- **Dependency Injection** for modular and scalable architecture  
- **SOLID Principles** to ensure maintainability  

## 🛠️ Models  
- **ApplicationUser** (Extends IdentityUser for user management)  
- **BaseEntity** (Common properties for all models)  
- **CommonPersonProperties** (Shared properties for person-related models)  
- **CommonProperties** (Reusable properties across entities)  
- **Department** (Represents company departments)  
- **Dependent** (Employee dependents)  
- **Employee** (Company employees)  
- **EmployeeProjects** (Employee-project assignments)  
- **Project** (Company projects)  

## 🚀 Getting Started  
### ✅ Prerequisites  
- .NET 8+  
- SQL Server  
- Entity Framework Core  
- LINQ  
- C#  

### 🔧 Installation & Setup  
1. Clone the repository:
   ```sh
   git clone https://github.com/your-repo-url.git
   ```
2. Navigate to the project directory:
   ```sh
   cd CompanyManagementSystem
   ```
3. Restore dependencies:
   ```sh
   dotnet restore
   ```
4. Apply migrations:
   ```sh
   dotnet ef database update
   ```
5. Run the application:
   ```sh
   dotnet run
   ```
6. Open your browser and go to:
   ```
   http://localhost:5000
   ```

## 📜 License  
This project is licensed under the **MIT License**. Feel free to use and modify it as needed.  

---  
The **Company Management System** is built for **scalability, security, and efficiency**, making company operations seamless. 🚀
