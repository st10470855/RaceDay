# RaceDay Database Design Decisions

## Overview

The RaceDay database is designed to support the management of South African road running, walking, and cycling events.

The database separates user identity, user roles, event information, enrolments, and race results into related tables. This structure supports the two main system roles: Organiser and Participant.

## Database

The database is named:

`RaceDayDB`

The database is implemented using Microsoft SQL Server.

## Identity and User Management

The database uses separate tables for roles, users, organisers, and participants.

### Role

The `Role` table defines the roles available in the RaceDay system.

The system supports two roles:

* Organiser
* Participant

Separating roles from user information allows role information to be managed independently from the user's personal details.

### User

The `User` table stores the core account and personal information required by the system.

This includes:

* First name
* Last name
* Email
* Password hash
* Phone number

Email addresses are required to be unique so that an account can be identified consistently.

### Organizer

The `Organizer` table extends the user information for organisers.

It stores organisation-related information such as:

* Organisation name
* Organisation website

This allows organiser-specific information to be kept separate from general account information.

### Participant

The `Participant` table extends the user information for participants.

It stores participant-specific information including:

* Running club
* Date of birth
* Gender

This separation allows the system to store information relevant to athletes without adding participant-specific fields to every user account.

## Event Management

### Event

The `Event` table represents an athletic event managed by an organiser.

Event information includes:

* Event date
* Location
* Distance
* Event type
* Organiser information

The event type is restricted to:

* Run
* Walk
* Cycle

This prevents unsupported event types from being stored in the database.

### Category

The `Category` table allows an event to be divided into participant categories.

Categories can use age or distance-related rules to organise participants.

For example, the sample database includes categories such as:

* Junior
* Senior
* Masters

Age limits are validated so that the minimum age cannot be greater than the maximum age.

## Enrolments and Results

### EventEnrolment

The `EventEnrolment` table connects participants with events and categories.

This allows the system to record which participant entered which event and category.

An enrolment also has a status. Supported statuses are:

* Confirmed
* Cancelled
* Pending

A unique constraint on the participant and event prevents the same participant from registering for the same event more than once.

### Result

The `Result` table stores the outcome of an event enrolment after the event has taken place.

Result information includes:

* Finish time
* Finish position
* The user who recorded the time/result

This allows participant performance to be tracked after an event.

## Data Integrity Decisions

The database contains validation rules designed to prevent invalid data.

### Event Type Validation

Events are restricted to the supported event types:

`Run`, `Walk`, and `Cycle`.

### Enrolment Status Validation

Enrolment status is restricted to:

`Confirmed`, `Cancelled`, and `Pending`.

### Positive Values

Event distances must be greater than zero.

Finish positions must also be greater than zero.

### Category Age Validation

Category age ranges must be logical.

The minimum age cannot be greater than the maximum age.

### Duplicate Enrolment Prevention

A participant cannot enrol in the same event more than once.

This is enforced using a unique constraint involving the participant and event.

## Sample Data

The database includes sample South African context data for testing and demonstrating the system.

Example organisers include:

* Mokoena Events
* Dlamini Sports Events

Example events include:

* Johannesburg Community 10K
* Soweto Family Walk
* Cape Town Coastal Cycle

The sample data demonstrates the database structure using realistic event-management scenarios.

## Design Rationale

The database separates information into related entities rather than storing all information in a single table.

This design:

* Reduces unnecessary duplication of information.
* Separates account information from role-specific information.
* Makes organiser and participant information easier to manage.
* Allows events to have multiple categories.
* Allows participants to enrol in events.
* Allows results to be recorded after an event.
* Provides database constraints to protect data integrity.

## Relationship Between Database Design and the ERD

The database design is represented visually by the RaceDay Entity Relationship Diagram.

The ERD documents the entities, attributes, primary keys, foreign keys, and relationships used by the system.

The SQL implementation is intended to correspond with the planned ERD structure.

## Conclusion

The RaceDay database provides the foundation for the event-management system.

Its structure supports user authentication and roles, event management, categories, participant enrolments, and race results while using database constraints to maintain valid and consistent information.
