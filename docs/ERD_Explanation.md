# RaceDay ERD Explanation

## Overview

The RaceDay Entity Relationship Diagram (ERD) represents the database structure used by the RaceDay event-management system.

The ERD shows the main entities, their attributes, primary keys, foreign keys, and relationships.

The database contains eight main entities:

* Role
* User
* Organizer
* Participant
* Event
* Category
* EventEnrolment
* Result

## Role and User Relationship

The `Role` entity defines the roles available in the RaceDay system.

The two supported roles are:

* Organiser
* Participant

The `User` entity stores the main account information.

A role can be assigned to multiple users, while each user has one assigned role.

Therefore, the relationship is:

**Role 1 → Many Users**

The `RoleID` foreign key in the `User` table connects each user to their role.

## User and Organizer Relationship

The `Organizer` entity contains information specific to organisers.

An organiser is linked to a user account through `UserID`.

The relationship allows general user information to remain in the `User` table while organiser-specific information is stored separately.

The relationship is:

**User 1 → 0 or 1 Organizer**

A user does not necessarily have to be an organiser, but an organiser record belongs to a user account.

## User and Participant Relationship

The `Participant` entity contains information specific to participants.

It is linked to the `User` entity through `UserID`.

This keeps participant-specific information separate from general account information.

The relationship is:

**User 1 → 0 or 1 Participant**

A user may have a participant profile, depending on their assigned role.

## Organizer and Event Relationship

The `Event` entity represents a RaceDay event.

Each event is created and managed by an organiser.

The `Event` table stores the organiser's identifier using `OrganizerID`.

The relationship is:

**Organizer 1 → Many Events**

One organiser can manage multiple events, while each event belongs to one organiser.

## Event and Category Relationship

The `Category` entity represents a category within an event.

An event can contain multiple categories.

For example, an event may have:

* Junior
* Senior
* Masters

The relationship is:

**Event 1 → Many Categories**

Each category belongs to one event.

The `EventID` foreign key in the `Category` table identifies the event associated with the category.

## Participant and Event Relationship

Participants can enter events through the `EventEnrolment` entity.

A participant can enter multiple events, and an event can have multiple participants.

This creates a many-to-many relationship between participants and events.

The `EventEnrolment` table acts as the linking entity between them.

The relationship can be represented as:

**Participant 1 → Many EventEnrolments**

and:

**Event 1 → Many EventEnrolments**

Together, these relationships represent:

**Participant Many ↔ Many Event**

## Category and EventEnrolment Relationship

Each event enrolment records the category selected by the participant.

The `CategoryID` foreign key in `EventEnrolment` identifies the selected category.

The relationship is:

**Category 1 → Many EventEnrolments**

A category can therefore contain multiple participant enrolments.

## EventEnrolment and Result Relationship

The `Result` entity stores the result of a participant's event entry.

A result is linked to an enrolment through `EventEnrolmentID`.

The relationship is:

**EventEnrolment 1 → 0 or 1 Result**

An enrolment may not have a result before the event has taken place.

After the result has been recorded, the enrolment can have an associated result.

## User and Result Relationship

The `Result` entity also records which user captured the result.

The `RecordedBy` field identifies the user who recorded the result.

This supports the requirement that organisers can capture participant results.

The relationship is:

**User 1 → Many Results**

A user can record multiple results.

## Overall Database Relationships

The main relationships in the RaceDay database can be summarised as follows:

| Relationship                 | Cardinality        |
| ---------------------------- | ------------------ |
| Role → User                  | One-to-Many        |
| User → Organizer             | One-to-Zero-or-One |
| User → Participant           | One-to-Zero-or-One |
| Organizer → Event            | One-to-Many        |
| Event → Category             | One-to-Many        |
| Participant → EventEnrolment | One-to-Many        |
| Event → EventEnrolment       | One-to-Many        |
| Category → EventEnrolment    | One-to-Many        |
| EventEnrolment → Result      | One-to-Zero-or-One |
| User → Result                | One-to-Many        |

## Many-to-Many Relationship

The most important many-to-many relationship in the RaceDay system is between Participants and Events.

A participant can enter many events.

An event can have many participants.

The `EventEnrolment` entity resolves this many-to-many relationship by storing each participant's event entry.

This also allows the system to store the selected category and enrolment status.

## Data Integrity

The ERD relationships are supported by primary keys and foreign keys in the SQL database.

The database also uses constraints to help maintain valid data.

Examples include:

* Unique email addresses.
* Valid event types.
* Valid enrolment statuses.
* Positive event distances.
* Positive finish positions.
* Valid category age ranges.
* Prevention of duplicate participant enrolments for the same event.

## Relationship to the SQL Database

The ERD provides the planned visual representation of the RaceDay database.

The SQL script implements the database structure using tables, primary keys, foreign keys, and constraints.

The ERD and SQL implementation should remain consistent as the project progresses into API development.

## Conclusion

The RaceDay ERD provides a visual overview of how users, roles, organisers, participants, events, categories, enrolments, and results are connected.

The relationships support the functional requirements of the RaceDay system and provide the database foundation for the API in Part 2 and the MVC application in Part 3.
