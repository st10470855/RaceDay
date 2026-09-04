# RaceDay

## System Description

RaceDay is a full-stack event-management system designed for South African road running, walking, and cycling events.

The system is designed to support the management of athletic events from event creation and category management through to participant enrolments and race results.

Part 1 focuses on the planning and database foundation of the system. This includes the Entity Relationship Diagram (ERD), API Endpoint Plan, SQL database implementation, supporting documentation, and GitHub Actions validation.

The project will be developed across three parts using a single GitHub repository.

## User Roles

RaceDay has two primary user roles.

### Organiser

Organisers are responsible for managing athletic events.

Organisers can:

* Create events.
* Edit events.
* Delete events.
* Manage event categories.
* View event enrolments.
* Capture participant results.

### Participant

Participants use RaceDay to discover events and track their participation.

Participants can:

* Create an account.
* Browse available events.
* Enter an event by selecting a category.
* View their own enrolments.
* Track their personal results.

Role-based access will be enforced at the API level in Part 2 and reflected in the MVC interface in Part 3.

## Part 1 Documentation

The main Part 1 deliverables are stored in the `docs` folder.

### Database Design

* [RaceDay ERD](docs/RaceDay_ERD.png)
* [RaceDay Database SQL Script](docs/RaceDay_Database.sql)
* [Database Setup Guide](docs/Database_Setup_Guide.md)
* [Database Verification Guide](docs/Database_Verification_Guide.md)
* [Database Verification Queries](docs/Database_Verification_Queries.sql)
* [Database Design Decisions](docs/Database_Design_Decisions.md)
* [Data Dictionary](docs/Data_Dictionary.md)
* [SQL Implementation Notes](docs/SQL_Implementation_Notes.md)
* [Database Test Cases](docs/Part1_Database_Test_Cases.md)

### API Planning

* [RaceDay API Endpoint Plan](docs/RaceDay%20API%20Endpoint%20Plan.pdf)
* [API Design Notes](docs/API_Design_Notes.md)
* [API Role Access Matrix](docs/API_Role_Access_Matrix.md)

### Project Documentation

* [Project Structure Guide](docs/Project_Structure_Guide.md)
* [Roles and Permissions Guide](docs/Roles_and_Permissions_Guide.md)
* [Part 1 Requirements Checklist](docs/Part1_Requirements_Checklist.md)

## Database

The RaceDay database is implemented using Microsoft SQL Server.

The database is named:

`RaceDayDB`

The database contains eight main tables:

1. `Role`
2. `User`
3. `Organizer`
4. `Participant`
5. `Event`
6. `Category`
7. `EventEnrolment`
8. `Result`

The database includes primary keys, foreign keys, validation constraints, and sample data to support the RaceDay requirements.

## API Planning

The Part 1 API planning covers the main RaceDay system functions:

* Authentication
* User profiles
* Events
* Categories
* Event enrolments
* Results

The API Endpoint Plan defines the planned HTTP methods, routes, descriptions, required roles, request bodies, and expected responses.

The API Role Access Matrix provides an additional reference for the permissions required by Organisers and Participants.

## GitHub Actions CI/CD

GitHub Actions is used to validate the required Part 1 documentation.

The workflow is located at:

`.github/workflows/part1-validation.yml`

The workflow checks that the required Part 1 documentation files are present in the repository.

The successful build is shown below.

![Successful GitHub Actions CI/CD Build](docs/RaceDay_CI_Green_Build.png)

## Part 1 Video

The Part 1 demonstration video has been recorded and uploaded to YouTube as an unlisted video.

The video demonstrates the RaceDay Part 1 work and provides supporting evidence of the planning, database, documentation, and project structure.

**YouTube video:** https://www.youtube.com/watch?v=XwXIHUkcRnQ

## Repository Structure

The repository is organised so that Part 1 planning and database documentation is stored in the `docs` folder, while GitHub Actions configuration is stored in `.github/workflows`.

```text
RaceDay/
├── .github/
│   └── workflows/
│       └── part1-validation.yml
│
├── docs/
│   ├── RaceDay_ERD.png
│   ├── RaceDay API Endpoint Plan.pdf
│   ├── RaceDay_Database.sql
│   ├── RaceDay_CI_Green_Build.png
│   ├── Database_Setup_Guide.md
│   ├── Database_Design_Decisions.md
│   ├── Database_Verification_Queries.sql
│   ├── Database_Verification_Guide.md
│   ├── Data_Dictionary.md
│   ├── ERD_Explanation.md
│   ├── SQL_Implementation_Notes.md
│   ├── Part1_Database_Test_Cases.md
│   ├── API_Design_Notes.md
│   ├── API_Role_Access_Matrix.md
│   ├── Project_Structure_Guide.md
│   ├── Roles_and_Permissions_Guide.md
│   └── Part1_Requirements_Checklist.md
│
└── README.md
```

## Version Control

Git and GitHub are used to maintain the RaceDay project history.

The project uses meaningful commits to document the development and documentation work completed during Part 1.

The repository contains the required Part 1 work and the Git history provides evidence of the project progression.

## Part 1 Completion

Part 1 provides the planning and database foundation for the RaceDay system.

The completed work includes:

* Entity Relationship Diagram.
* API Endpoint Plan.
* SQL Server database script.
* Database verification documentation.
* Database design documentation.
* Data dictionary.
* ERD relationship explanation.
* SQL implementation notes.
* Database test cases.
* API design notes.
* API role access matrix.
* Roles and permissions documentation.
* GitHub Actions validation.
* Successful CI/CD build evidence.
* Part 1 demonstration video.

The project is ready for Part 1 review and ARC submission.

## Future Development

### Part 2

Part 2 will implement the RaceDay API based on the planning completed in Part 1.

The API will implement authentication, role-based access, user profiles, events, categories, event enrolments, and results.

### Part 3

Part 3 will implement the MVC application that consumes the RaceDay API.

The MVC application will provide appropriate interfaces for Organisers and Participants while reflecting the role permissions enforced by the API.

## Conclusion

RaceDay Part 1 establishes the database, API planning, documentation, version-control, and CI/CD foundation required for the later development stages.

The repository contains the required planning documents and supporting evidence for the Part 1 submission.
