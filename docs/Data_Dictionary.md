# RaceDay Data Dictionary

## Overview

This data dictionary describes the main tables and fields used by the RaceDay database.

The RaceDay database contains eight main tables supporting user management, roles, events, categories, enrolments, and results.

The data dictionary provides a reference for understanding the database structure alongside the ERD and SQL script.

## Role

The `Role` table stores the roles available in the RaceDay system.

| Field    | Description                                                |
| -------- | ---------------------------------------------------------- |
| RoleID   | Unique identifier for a role.                              |
| RoleName | Name of the system role, such as Organiser or Participant. |

## User

The `User` table stores the core account and personal information for system users.

| Field        | Description                                            |
| ------------ | ------------------------------------------------------ |
| UserID       | Unique identifier for a user.                          |
| FirstName    | User's first name.                                     |
| LastName     | User's surname.                                        |
| Email        | User's email address. It must be unique.               |
| PasswordHash | Securely stored representation of the user's password. |
| PhoneNumber  | User's contact telephone number.                       |
| RoleID       | Identifies the role assigned to the user.              |

## Organizer

The `Organizer` table stores information specific to organisers.

| Field            | Description                                                   |
| ---------------- | ------------------------------------------------------------- |
| OrganizerID      | Unique identifier for the organiser record.                   |
| UserID           | Links the organiser record to the corresponding user account. |
| OrganisationName | Name of the organisation managed by the organiser.            |
| Website          | Website associated with the organisation.                     |

## Participant

The `Participant` table stores information specific to participants.

| Field         | Description                                                     |
| ------------- | --------------------------------------------------------------- |
| ParticipantID | Unique identifier for the participant record.                   |
| UserID        | Links the participant record to the corresponding user account. |
| RunningClub   | Running or sports club associated with the participant.         |
| DateOfBirth   | Participant's date of birth.                                    |
| Gender        | Participant's recorded gender.                                  |

## Event

The `Event` table stores information about RaceDay athletic events.

| Field       | Description                                         |
| ----------- | --------------------------------------------------- |
| EventID     | Unique identifier for an event.                     |
| OrganizerID | Identifies the organiser responsible for the event. |
| EventName   | Name of the athletic event.                         |
| EventDate   | Date on which the event takes place.                |
| Location    | Location where the event takes place.               |
| Distance    | Distance of the event.                              |
| EventType   | Type of event: Run, Walk, or Cycle.                 |

## Category

The `Category` table stores categories associated with events.

| Field        | Description                                            |
| ------------ | ------------------------------------------------------ |
| CategoryID   | Unique identifier for a category.                      |
| EventID      | Identifies the event associated with the category.     |
| CategoryName | Name of the participant category.                      |
| MinimumAge   | Minimum age allowed for the category where applicable. |
| MaximumAge   | Maximum age allowed for the category where applicable. |

## EventEnrolment

The `EventEnrolment` table records participant entries into events and categories.

| Field            | Description                                                 |
| ---------------- | ----------------------------------------------------------- |
| EventEnrolmentID | Unique identifier for an enrolment.                         |
| ParticipantID    | Identifies the participant who entered the event.           |
| EventID          | Identifies the event being entered.                         |
| CategoryID       | Identifies the category selected by the participant.        |
| Status           | Current enrolment status: Confirmed, Cancelled, or Pending. |

A unique constraint prevents a participant from enrolling in the same event more than once.

## Result

The `Result` table stores participant performance information after an event.

| Field            | Description                                          |
| ---------------- | ---------------------------------------------------- |
| ResultID         | Unique identifier for a result.                      |
| EventEnrolmentID | Identifies the enrolment associated with the result. |
| FinishTime       | Participant's recorded finishing time.               |
| FinishPosition   | Participant's finishing position.                    |
| RecordedBy       | Identifies the user who recorded the result.         |

## Data Integrity Rules

The database uses constraints to protect data integrity.

Important rules include:

* Role values are restricted to Organiser and Participant.
* Event types are restricted to Run, Walk, and Cycle.
* Enrolment statuses are restricted to Confirmed, Cancelled, and Pending.
* Event distances must be greater than zero.
* Finish positions must be greater than zero.
* Category age ranges must be logical.
* Email addresses must be unique.
* A participant cannot enrol in the same event more than once.

## Relationship to Other Documentation

This data dictionary should be used together with:

* `RaceDay_ERD.png`
* `RaceDay_Database.sql`
* `Database_Design_Decisions.md`
* `Database_Verification_Queries.sql`

The ERD provides the visual representation of the database relationships, while the SQL script provides the database implementation.

## Purpose

The data dictionary provides a quick reference for developers and reviewers working with the RaceDay database.

It will also help maintain consistency between the database design, API implementation in Part 2, and MVC application in Part 3.

## Conclusion

The RaceDay data dictionary documents the purpose of the main database fields and provides a reference for understanding the system's data model.

It supports the ERD and SQL implementation and will help guide future development.
