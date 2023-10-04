# The Tree App

Welcome to "The Tree App"! This web application is designed to help you efficiently manage your branches and employees. It offers a user-friendly interface to organize and keep track of your branch locations, employee details, and important information.

## Key Features

- **Create and Manage Branch Locations**: Easily add, edit, and organize branch locations.
- **Employee Management**: Add and update employee details with ease.
- **Employee Photo Upload**: Upload employee photos for easy identification.
- **Date Tracking**: Keep track of important dates such as branch opening dates and employee hire dates.

Whether you're a small business owner or a manager responsible for multiple branches, "The Tree App" simplifies administrative tasks and enhances your organization's efficiency.

## How to Run

To run "The Tree App" on your local machine, follow these steps:

1. **Clone the Repository**: Clone this GitHub repository to your local machine using Git or download it as a ZIP archive.

   ```bash
   git clone https://github.com/ZeKublai/TheTreeApp.git
   ```

2. **Open in Visual Studio**: Open Visual Studio 2019.

3. **Open the Project**: Click on "File" > "Open" > "Project/Solution" and select the solution file (.sln) located in the project folder.

4. **Build the Solution**: In Visual Studio, right-click on the solution in the Solution Explorer and choose "Build."

5. **Run the Application**: Press F5 or click the "Start" button in Visual Studio to run the application. It will open in your default web browser.

6. **Explore the App**: You can now explore "The Tree App" in your browser. You may need to create initial data, such as branches and employees, to use the application effectively.

7. **Start Managing Your Data**: Use the app to create branches, add employees, and enjoy the benefits of efficient organization!

## Database Table Structures

### Employees Table

```sql
CREATE TABLE [dbo].[Employees] (
    [Id]               INT             IDENTITY (1, 1) NOT NULL,
    [LastName]         NVARCHAR (MAX)  NOT NULL,
    [FirstName]        NVARCHAR (MAX)  NOT NULL,
    [MiddleName]       NVARCHAR (MAX)  NOT NULL,
    [DateHired]        DATETIME        NOT NULL,
    [PhotoData]        VARBINARY (MAX) NULL,
    [PhotoContentType] NVARCHAR (MAX)  NULL,
    CONSTRAINT [PK_dbo.Employees] PRIMARY KEY CLUSTERED ([Id] ASC)
);
```

### Branches Table

```sql
CREATE TABLE [dbo].[Branches] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [Code]       NVARCHAR (MAX) NOT NULL,
    [Name]       NVARCHAR (MAX) NOT NULL,
    [Address]    NVARCHAR (MAX) NOT NULL,
    [Barangay]   NVARCHAR (MAX) NOT NULL,
    [City]       NVARCHAR (MAX) NOT NULL,
    [PermitNo]   NVARCHAR (MAX) NOT NULL,
    [DateOpened] DATETIME       NOT NULL,
    [IsActive]   BIT            NOT NULL,
    [ManagerId]  INT            DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_dbo.Branches] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Branches_dbo.Employees_BranchManager_Id] FOREIGN KEY ([ManagerId]) REFERENCES [dbo].[Employees] ([Id]) ON DELETE CASCADE
);
```

Feel free to explore and use "The Tree App." If you encounter any issues or have suggestions for improvement, please visit the GitHub Repository and get in touch. Thanks for your feedback!
