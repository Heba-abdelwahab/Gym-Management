# 🏋️‍♂️ Gmawy - Gym Management System

Gmawy is a web-based gym management platform designed to simplify the operations between gym owners, coaches, and trainees. Admins manage gym approvals, while coaches and trainees interact in real-time for training and nutrition planning.

---

## 🎯 Key Features

### 🛡️ Admin
- Accept or reject gym addition requests from gym owners
- Oversee platform-wide operations

### 🏢 Gym Owner
- Submit gyms for approval by the admin
- Define gym features and create membership plans
- Set up classes and assign coaches to them
- Assign coaches to specific trainees

### 🧑‍🏫 Coach
- Request to join specific gyms
- Manage trainees assigned to them
- Create exercise and diet plans
- Communicate with trainees in real-time

### 🧍 Trainee
- Register for memberships at approved gyms
- Attend available gym classes
- View assigned workouts and diet plans
- Chat directly with their coach

---

## 🏗️ Architecture

- **Union Architecture** for clear separation of concerns
- **Specification Pattern** for clean and reusable query logic
- **Repository Pattern** to abstract database interactions
- **Unit of Work** to ensure transactional integrity across multiple repositories

---

## 🧰 Tech Stack

- **Backend:** ASP.NET Core Web API  
- **ORM:** Entity Framework Core  
- **Database:** SQL Server  
- **Authentication:** JWT Tokens  
- **Real-time Messaging:** SignalR  
- **Frontend:** Angular  
