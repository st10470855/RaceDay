# RaceDay Roles and Permissions Guide

## Overview

RaceDay uses role-based access to separate the responsibilities of Organisers and Participants.

The system has two primary roles:

* Organiser
* Participant

Each role has different responsibilities within the event-management system.

## Organiser

The Organiser is responsible for managing athletic events and their associated information.

### Organiser Responsibilities

An Organiser can:

* Create events.
* Edit events.
* Delete events.
* Manage event categories.
* Capture participant results.
* View all event enrolments.

### Event Management

Organisers can create and maintain event information such as the event date, location, distance, and event type.

Supported event types are:

* Run
* Walk
* Cycle

### Category Management

Organisers can manage categories associated with events.

Categories can be used to group participants according to rules such as age ranges or event-specific distance requirements.

### Result Management

After an event has taken place, Organisers can capture participant results.

Results can include:

* Finish time
* Finish position
* The user who recorded the result

### Enrolment Management

Organisers can view the enrolments associated with events.

This allows organisers to monitor which participants have entered their events.

## Participant

The Participant is an athlete who uses RaceDay to discover and enter events and track their own participation.

### Participant Responsibilities

A Participant can:

* Create an account.
* Browse available events.
* Enter an event by selecting a category.
* View their own enrolments.
* Track their personal results.

### Account Management

Participants can create an account and provide the personal information required by the system.

The participant profile can include information such as:

* First name
* Last name
* Email
* Phone number
* Running club
* Date of birth
* Gender

### Event Browsing

Participants can browse events available through the RaceDay system.

Event information can include:

* Event date
* Location
* Distance
* Event type

### Event Entry

A Participant can enter an event by selecting an appropriate category.

The database prevents the same participant from registering for the same event more than once.

### Enrolment Tracking

Participants can view their own enrolments.

This allows them to keep track of the events they have entered without accessing other participants' enrolment information.

### Personal Results

Participants can view their own race results after results have been recorded.

This supports the requirement for participants to track their personal performance.

## Role Separation

The two roles have different responsibilities and access requirements.

| Function              | Organiser | Participant |
| --------------------- | --------- | ----------- |
| Create account        | Yes       | Yes         |
| Browse events         | Yes       | Yes         |
| Create events         | Yes       | No          |
| Edit events           | Yes       | No          |
| Delete events         | Yes       | No          |
| Manage categories     | Yes       | No          |
| Enter events          | No        | Yes         |
| View all enrolments   | Yes       | No          |
| View own enrolments   | No        | Yes         |
| Capture results       | Yes       | No          |
| View personal results | No        | Yes         |

## Access Control Principle

RaceDay should enforce role-based access at the API level.

The MVC interface should also reflect the permissions of the logged-in user's role.

For example:

* Organiser-only management functions should not be available to Participants.
* Participant personal information and enrolments should be appropriately restricted.
* Participants should only be able to access their own enrolment and result information.
* Organisers should have access to the management functions required for operating events.

## Security Considerations

Role permissions should not rely only on hiding buttons or menu items in the user interface.

The API must verify the authenticated user's role before allowing access to protected operations.

This provides an additional security boundary between Organiser and Participant functionality.

## Relationship to the Database

The database supports role separation through the `Role`, `User`, `Organizer`, and `Participant` tables.

The `Role` table identifies whether a user is an Organiser or Participant.

The `Organizer` and `Participant` tables store information specific to each role.

This database structure provides the foundation for implementing role-based access in the API and MVC application.

## Conclusion

Role separation is an important part of the RaceDay system.

Organisers focus on managing events, categories, enrolments, and results, while Participants focus on discovering events, entering events, viewing their enrolments, and tracking their personal results.

These permissions will guide the API implementation in Part 2 and the MVC interface implementation in Part 3.
