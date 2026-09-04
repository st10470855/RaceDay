# RaceDay API Design Notes

## Overview

The RaceDay API will provide the backend services required by the RaceDay event-management system.

The API is planned around the main features of the system:

* Authentication
* User profiles
* Events
* Categories
* Event enrolments
* Results

The detailed HTTP methods, routes, request bodies, and expected responses are documented in the RaceDay API Endpoint Plan.

## API Design Principles

The API should provide clear and predictable routes for each major area of the RaceDay system.

Endpoints should use appropriate HTTP methods according to the operation being performed.

Examples include:

* `GET` for retrieving information.
* `POST` for creating information.
* `PUT` or `PATCH` for updating information.
* `DELETE` for removing information.

The final implementation should closely follow the endpoint plan created during Part 1.

## Authentication

Authentication is required so that RaceDay can identify users and determine their role.

The planned authentication functionality includes:

* User registration.
* User login.
* Authentication of protected requests.

Successful authentication should provide the information required for the application to identify the logged-in user.

## Role-Based Access

RaceDay has two roles:

* Organiser
* Participant

Role-based access will be enforced at the API level.

### Organiser Access

Organisers require access to functionality for:

* Creating events.
* Editing events.
* Deleting events.
* Managing categories.
* Viewing event enrolments.
* Capturing participant results.

### Participant Access

Participants require access to functionality for:

* Creating an account.
* Browsing events.
* Entering events.
* Selecting event categories.
* Viewing their own enrolments.
* Viewing their personal results.

The API must not rely only on the MVC interface to protect restricted operations.

## User Profiles

The API will provide functionality for working with user profile information.

User information should be handled according to the user's authenticated identity and role.

Participant-specific and organiser-specific information should remain appropriately separated.

## Events

Events are a major resource in the RaceDay system.

The API will support operations required to manage and browse events.

Event information includes details such as:

* Event date.
* Location.
* Distance.
* Event type.
* Organiser information.

The supported event types are:

* Run
* Walk
* Cycle

## Categories

Categories are associated with events and allow participants to enter an event using an appropriate category.

The API should support the management and retrieval of categories required by the RaceDay system.

Category rules can include age-related requirements and event-specific requirements.

## Event Enrolments

Event enrolments connect Participants with Events and Categories.

The API should support the creation and retrieval of enrolment information.

A Participant should only be able to access their own enrolment information where personal enrolment data is involved.

Organisers require access to enrolment information for managing their events.

The database also prevents duplicate enrolment of the same participant in the same event.

## Results

Results are recorded after an event has taken place.

The API should support the capture and retrieval of race results.

Result information can include:

* Finish time.
* Finish position.
* The user who recorded the result.

Participants should be able to view their personal results.

Organisers require permission to capture participant results.

## Request and Response Design

API requests should use consistent data structures.

Where an endpoint requires information to create or update a resource, the required information should be supplied in the request body.

Responses should clearly communicate the result of the operation.

The API should also return appropriate HTTP status codes for successful operations and errors.

## Validation

The API should validate incoming information before attempting to store it in the database.

Validation should complement the database constraints already defined in the RaceDay SQL implementation.

Examples include:

* Valid event types.
* Valid enrolment statuses.
* Positive event distances.
* Positive finishing positions.
* Valid category age ranges.
* Prevention of duplicate event enrolments.

## Error Handling

The API should provide clear error responses when a request cannot be completed.

Examples include:

* Missing required information.
* Invalid data.
* Unauthenticated requests.
* Requests from users without the required role.
* Requests for resources that do not exist.
* Duplicate enrolments.

Clear error handling will make the API easier for the MVC application to consume and easier to troubleshoot during development.

## Relationship to the Endpoint Plan

The `RaceDay API Endpoint Plan.pdf` is the primary reference for the planned API routes.

The Part 2 implementation should closely follow that plan.

If changes are required during implementation, the documentation should be updated so that the planned API and implemented API remain consistent.

## Relationship to the Database

The API will act as the application layer between the MVC client and the RaceDay database.

The API should use the database relationships and constraints defined during Part 1 rather than duplicating business data unnecessarily.

The database remains responsible for enforcing important data-integrity rules.

## Security Considerations

Protected API operations should require authentication where appropriate.

Role-restricted operations should verify the authenticated user's role.

Participants must not be able to use participant-level access to perform Organiser-only management operations.

Similarly, access to personal enrolment and result information should be appropriately restricted.

## Future Implementation

The API design documented here will guide the implementation work in Part 2.

The implementation should preserve the planned separation between authentication, user profiles, events, categories, enrolments, and results.

The API will also provide the foundation required by the MVC application developed in Part 3.

## Conclusion

The RaceDay API is designed to provide a structured backend for the event-management system.

The API design focuses on clear resources, appropriate HTTP operations, authentication, role-based access, validation, error handling, and consistency with the Part 1 database and endpoint plan.
