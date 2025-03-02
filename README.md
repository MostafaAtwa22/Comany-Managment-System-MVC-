## 📌 Overview  
**Company Management System** is a robust and scalable application designed for managing employees, departments, and projects. The project follows **Onion Architecture** and adheres to **Clean Code** principles.  

## 🏗️ Architecture  
The application is structured following **Onion Architecture**, ensuring high maintainability and separation of concerns:  
- **Presentation Layer**: Handles the UI and user interactions.  
- **Application Layer**: Business logic, services, and use cases.  
- **Infrastructure Layer**: Database access, external APIs, authentication, and repositories.  
- **Domain Layer**: Core domain models, business rules, and shared properties.  

## 🔑 Features  
✅ **Admin, Employee & Manager Roles**  
✅ **Authentication & Authorization (Identity & Roles, Forget Password, Google Authentication)**  
✅ **User Management (Manage User Accounts, Delete/Lock Users, Role Assignment)**  
✅ **Department & Employee Management (CRUD Operations)**  
✅ **Project Management (Assign Employees, Track Progress)**  
✅ **Managers can view employees in their department and all projects**  
✅ **Employees can view their own data**  
✅ **AutoMapper for Object Mapping**  
✅ **Specification Design Pattern for Query Filtering**  
✅ **Unit of Work & Repository Pattern for Database Transactions**  
✅ **Enum Support (Gender, Relation)**  
✅ **Clean & Maintainable Code (SOLID Principles, Dependency Injection)**  
✅ **Code-First Approach for Database Management**  

## 🔐 Authentication & Authorization  
- Uses **ASP.NET Identity** for user authentication.  
- **Google Authentication** integration for seamless login.  
- Roles:  
  - **Admin**: Full access to manage everything.  
  - **Manager**: Can manage employees within their department and view all projects.  
  - **Employee**: Can only view their own data.  

## 📦 Technologies Used  
- **ASP.NET Core MVC**  
- **Entity Framework Core (Code-First)**  
- **SQL Server**  
- **Bootstrap & jQuery**  
- **AJAX for Asynchronous Operations**  
- **Forget password**  
- **Specification Design Pattern**  
- **Repository Pattern & Unit of Work**  
- **Dependency Injection**  
- **SOLID Principles**  
- **Onion Architecture**  

## 🛠️ Models  
- **ApplicationUser** (Extends IdentityUser for User Management)  
- **BaseEntity**  
- **CommonPersonProperties**  
- **CommonProperties**  
- **Department**  
- **Dependent**  
- **Employee**  
- **EmployeeProjects**  
- **Project**  

## 🚀 Getting Started  
### ✅ Prerequisites  
- .NET 8+  
- SQL Server  
- EF Core  
- LINQ  
- C#  

The **Company Management System** is designed to be **secure**, **scalable**, and **efficient**, ensuring streamlined management of company operations. 🏢🚀
