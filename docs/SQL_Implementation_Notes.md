# RaceDay SQL Implementation Notes

## Overview

The RaceDay SQL implementation provides the database foundation for the RaceDay event-management system.

The SQL script creates the `RaceDayDB` database, its tables, relationships, constraints, and sample data required for Part 1.

The implementation is designed to correspond with the RaceDay ERD.

## Database Creation

The database is named:

`RaceDayDB`

The SQL script creates the database and then selects it before creating the RaceDay tables.

This ensures that the tables are created inside the correct database.

## Tables

The SQL implementation contains eight main tables:

1. `Role`
2. `User`
3. `Organizer`
4. `Participant`
5. `Event`
6. `Category`
7. `EventEnrolment`
8. `Result`

These tables represent the main entities shown in the RaceDay ERD.

## Role Table

The `Role` table stores the roles available in the system.

The supported roles are:

* Organiser
* Participant

The `RoleID` primary key uniquely identifies each role.

The `User` table uses `RoleID` as a foreign key to connect users with their assigned role.

## User Table

The `User` table stores common account information.

It includes:

* User ID
* First name
* Last name
* Email
* Password hash
* Phone number
* Role ID

The email field is subject to a uniqueness rule so that duplicate account email addresses cannot be stored.

## Organizer and Participant Tables

The `Organizer` table stores information specific to organisers.

The `Participant` table stores information specific to participants.

Both tables are connected to the `User` table through `UserID`.

This design keeps common account information separate from role-specific profile information.

## Event Table

The `Event` table stores information about RaceDay events.

Each event is associated with an organiser using `OrganizerID`.

Event information includes:

* Event name
* Event date
* Location
* Distance
* Event type

The event type is restricted to:

* Run
* Walk
* Cycle

The distance must also be greater than zero.

## Category Table

The `Category` table stores categories belonging to events.

Each category is linked to an event using `EventID`.

Category information includes:

* Category name
* Minimum age
* Maximum age

The database validates the category age range so that the minimum age cannot be greater than the maximum age.

## EventEnrolment Table

The `EventEnrolment` table connects participants with events and categories.

It contains foreign keys for:

* Participant
* Event
* Category

The table also stores the enrolment status.

Supported statuses are:

* Confirmed
* Cancelled
* Pending

A unique constraint prevents the same participant from enrolling in the same event more than once.

This supports the business rule that a participant should have only one entry per event.

## Result Table

The `Result` table stores race results after an event has taken place.

A result is connected to an event enrolment using `EventEnrolmentID`.

Result information includes:

* Finish time
* Finish position
* User who recorded the result

Finish positions are required to be greater than zero.

## Primary Keys

Primary keys provide unique identifiers for records in each table.

The main primary keys are:

| Table          | Primary Key      |
| -------------- | ---------------- |
| Role           | RoleID           |
| User           | UserID           |
| Organizer      | OrganizerID      |
| Participant    | ParticipantID    |
| Event          | EventID          |
| Category       | CategoryID       |
| EventEnrolment | EventEnrolmentID |
| Result         | ResultID         |

## Foreign Keys

Foreign keys establish relationships between related tables.

Important relationships include:

| Table          | Foreign Key      | References     |
| -------------- | ---------------- | -------------- |
| User           | RoleID           | Role           |
| Organizer      | UserID           | User           |
| Participant    | UserID           | User           |
| Event          | OrganizerID      | Organizer      |
| Category       | EventID          | Event          |
| EventEnrolment | ParticipantID    | Participant    |
| EventEnrolment | EventID          | Event          |
| EventEnrolment | CategoryID       | Category       |
| Result         | EventEnrolmentID | EventEnrolment |
| Result         | RecordedBy       | User           |

These foreign keys implement the relationships represented in the ERD.

## Constraints

The SQL implementation uses constraints to protect data integrity.

Important constraints include:

* Unique email addresses.
* Valid role values.
* Valid event types.
* Valid enrolment statuses.
* Positive event distances.
* Positive finish positions.
* Logical category age ranges.
* Prevention of duplicate participant enrolments for the same event.

These constraints help prevent invalid information from being stored.

## Sample Data

The database contains sample data for demonstrating and testing the RaceDay system.

Example organisers include:

* Mokoena Events
* Dlamini Sports Events

Example events include:

* Johannesburg Community 10K
* Soweto Family Walk
* Cape Town Coastal Cycle

Example categories include:

* Junior
* Senior
* Masters

The sample data provides realistic South African event-management examples.

## ERD and SQL Consistency

The SQL implementation is intended to match the RaceDay ERD.

The ERD provides the visual database design, while the SQL script provides the actual database implementation.

The following should remain consistent between the two:

* Entity names.
* Primary keys.
* Foreign keys.
* Relationships.
* Important attributes.

Any future changes to the database structure should also be reflected in the ERD and supporting documentation.

## Verification

The database can be verified using:

`Database_Verification_Queries.sql`

The verification queries can confirm:

* The correct database is selected.
* The expected tables exist.
* Sample records are available.
* Events and categories have been created.
* Enrolments and results are available.

The database was successfully created and verified using SQL Server Management Studio.

## Relationship to Future Development

The SQL database will provide the data layer for the RaceDay API in Part 2.

The API will use the database structure to support:

* Authentication.
* User profiles.
* Event management.
* Category management.
* Event enrolments.
* Results.

The MVC application in Part 3 will consume the API and provide the user interface for Organisers and Participants.

## Conclusion

The RaceDay SQL implementation provides a structured and validated database for the event-management system.

The tables, keys, relationships, constraints, and sample data support the requirements identified during Part 1 planning.

The SQL implementation, ERD, and supporting documentation together provide a clear foundation for the API and MVC development stages.
