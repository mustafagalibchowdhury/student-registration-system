# Student Registration System

A full-stack student registration project. The backend REST API is built from scratch, and a client application connects to it to manage student data.

## Overview
- **StudentRegAPI**: a custom-built backend API that handles student data (create, read, update, delete)
- **StudentRegClient**: the client application that communicates with the API

## Features
- Register new students
- View all registered students
- Update student information
- Delete student records
- Client and backend connected through API endpoints

## Project Structure
StudentRegAPI/      -> Backend API (built from scratch)
StudentRegClient/   -> Client application

## Technologies Used
- Backend: (e.g., ASP.NET Core / Node.js / Spring Boot)
- Client: (e.g., React / Angular / Windows Forms)
- Database: (e.g., SQL Server / MySQL / SQLite)

## API Endpoints
| Method | Endpoint            | Description         |
|--------|---------------------|---------------------|
| GET    | /api/students       | Get all students    |
| GET    | /api/students/{id}  | Get a student by ID |
| POST   | /api/students       | Register a student  |
| PUT    | /api/students/{id}  | Update a student    |
| DELETE | /api/students/{id}  | Delete a student    |

## How to Run
1. Clone the repository:
   git clone https://github.com/your-username/student-registration-system.git
2. Run the backend API (`StudentRegAPI`) first.
3. Run the client (`StudentRegClient`) and make sure it points to the API URL.

## Author
Your Name
