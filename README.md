# Contacts-Management-System-C-Console-App-3-Tier-Architecture-
A fully structured Contacts Management System built using C# and designed with clean 3-Tier Architecture (Presentation – Business – Data Access).
The project applies core concepts of ADO.NET, OOP, and multi-layered system design.
-----------------------------------------------------------------------------------
**🎯 Purpose of the Project**

    This project was built as part of practicing backend development fundamentals:

    Building structured multi-layered systems

    Applying separation of concerns

    Learning real SQL operations using ADO.NET

    Preparing for enterprise-level .NET back-end work (API development later)
  <hr>
  
**🚀 Features**

<ul>🔹Contact Management

    - Add new contact
    - Update existing contact
    - Delete contact by ID
    - Find contact by ID
    - Check if contact exists
    - List all contacts
    - Handle one-to-many relation between Contacts & Countries
</ul>
<ul>🔹Country Management

    - Find country by ID
    - Find country by name
    - Check if a country exists by name
    - Business logic with enMode (AddNew / Update)
</ul>
<hr>

**🧱 3-Tier Architecture Breakdown**

<ul>
  
1️⃣ Presentation Layer (PL) – Console Application

    - Contains test functions for all operations

    - Calls Business Layer only (no database logic inside PL)

    - Handles output formatting
</ul>
<ul>
  
2️⃣ Business Layer (BL)

      - Contains clsContact and clsCountry classes
      - Implements:
      - Save logic (Insert / Update)
      - Object mapping
      - Validation (Existence checking)
      - Constructors for new and existing entities
</ul>
<ul>
  
3️⃣ Data Access Layer (DAL)

    - Fully parameterized queries
    - Uses using blocks for secure connection handling
    - Returns data safely using ref parameters
    - SQL operations implemented using parameterized ADO.NET commands
Handles SQL Server operations using **ADO.NET** :

    SqlConnection
    SqlCommand
    SqlDataReader
</ul>
<hr>

**🗄️ DataBase Structure**
<ul>
  
📌 Countries Table

| Column          | Data Type         | Description                        |
| --------------- | ----------------- | ---------------------------------- |
| **CountryID**   | INT (Primary Key) | Unique identifier for each country |
| **CountryName** | NVARCHAR(20)      | Name of the country                |
| **CountryCode** | NVARCHAR(3)       | Code of the country                |
| **PhoneCode**   | NVARCHAR(3)       | Code of the Phone for this country |

📌 Contacts Table

| Column          | Data Type         | Description                                   |
| --------------- | ----------------- | --------------------------------------------- |
| **ContactID**   | INT (Primary Key) | Unique identifier for each contact            |
| **FirstName**   | NVARCHAR          | Contact’s first name                          |
| **LastName**    | NVARCHAR          | Contact’s last name                           |
| **Email**       | NVARCHAR          | Contact’s email address                       |
| **Phone**       | NVARCHAR          | Contact’s phone number                        |
| **Address**     | NVARCHAR          | Contact’s physical address                    |
| **DateOfBirth** | DATETIME          | Date of birth                                 |
| **ImagePath**   | NVARCHAR          | Optional image file path                      |
| **CountryID**   | INT (Foreign Key) | Linked country (Reference to Countries table) |

</ul>
<hr>

**📌 Status**

The project is under continuous development.
Upcoming improvements include:

    - Adding validation logic

    - Enhancing error handling

    - Adding more helper utilities

    - Preparing UI / API layers in future versions
  <hr>
  
**Copyright &copy; December,2025 *Mohamed Magdy*. All rights reserved**
