# RaceDay Part 1 Database Test Cases

## Overview

This document describes test cases that can be used to verify the RaceDay database design and data-integrity rules.

The test cases focus on the database constraints and business rules defined in the RaceDay SQL implementation.

The tests can be performed using Microsoft SQL Server Management Studio (SSMS).

## Test Case 1: Database Exists

**Purpose:** Confirm that the RaceDay database was created successfully.

**Action:**

Check the list of databases in SSMS.

**Expected Result:**

The database `RaceDayDB` should exist.

**Status:** Passed

## Test Case 2: Required Tables Exist

**Purpose:** Confirm that all required RaceDay tables were created.

**Action:**

Expand:

`RaceDayDB → Tables`

**Expected Result:**

The following tables should exist:

* Role
* User
* Organizer
* Participant
* Event
* Category
* EventEnrolment
* Result

**Status:** Passed

## Test Case 3: Valid Event Type

**Purpose:** Confirm that supported event types can be stored.

**Action:**

Create an event using one of the supported event types:

* Run
* Walk
* Cycle

**Expected Result:**

The event should be accepted by the database.

**Status:** Passed

## Test Case 4: Invalid Event Type

**Purpose:** Confirm that unsupported event types are rejected.

**Action:**

Attempt to create an event using an unsupported event type.

Example:

`Swimming`

**Expected Result:**

The database should reject the record because the event type is restricted to the supported RaceDay event types.

**Status:** Passed

## Test Case 5: Positive Event Distance

**Purpose:** Confirm that event distances must be greater than zero.

**Action:**

Create an event with a positive distance.

Example:

`10`

**Expected Result:**

The event should be accepted.

**Status:** Passed

## Test Case 6: Invalid Event Distance

**Purpose:** Confirm that invalid event distances are rejected.

**Action:**

Attempt to create an event with a distance of zero or a negative value.

Examples:

`0`

`-5`

**Expected Result:**

The database should reject the invalid distance.

**Status:** Passed

## Test Case 7: Valid Category Age Range

**Purpose:** Confirm that logical category age ranges can be stored.

**Action:**

Create a category with:

* Minimum age: 13
* Maximum age: 19

**Expected Result:**

The category should be accepted.

**Status:** Passed

## Test Case 8: Invalid Category Age Range

**Purpose:** Confirm that invalid category age ranges are rejected.

**Action:**

Attempt to create a category where the minimum age is greater than the maximum age.

Example:

* Minimum age: 40
* Maximum age: 20

**Expected Result:**

The database should reject the invalid age range.

**Status:** Passed

## Test Case 9: Unique User Email

**Purpose:** Confirm that duplicate email addresses are prevented.

**Action:**

Create a user with an email address that already exists in the database.

**Expected Result:**

The database should reject the duplicate email address.

**Status:** Passed

## Test Case 10: Valid Enrolment Status

**Purpose:** Confirm that valid enrolment statuses are accepted.

**Action:**

Create or update an enrolment using one of the supported statuses:

* Confirmed
* Cancelled
* Pending

**Expected Result:**

The enrolment should be accepted.

**Status:** Passed

## Test Case 11: Invalid Enrolment Status

**Purpose:** Confirm that unsupported enrolment statuses are rejected.

**Action:**

Attempt to create an enrolment using an unsupported status.

Example:

`Completed`

**Expected Result:**

The database should reject the invalid status.

**Status:** Passed

## Test Case 12: Duplicate Participant Enrolment

**Purpose:** Confirm that a participant cannot enrol in the same event more than once.

**Action:**

Attempt to create two enrolments for the same participant and event.

**Expected Result:**

The first enrolment should be accepted.

The second enrolment should be rejected because of the unique participant/event constraint.

**Status:** Passed

## Test Case 13: Valid Finish Position

**Purpose:** Confirm that a valid finishing position can be stored.

**Action:**

Create a result with a positive finishing position.

Example:

`1`

**Expected Result:**

The result should be accepted.

**Status:** Passed

## Test Case 14: Invalid Finish Position

**Purpose:** Confirm that finishing positions must be greater than zero.

**Action:**

Attempt to create a result with:

`0`

or a negative finishing position.

**Expected Result:**

The database should reject the invalid finishing position.

**Status:** Passed

## Test Case 15: Foreign Key Relationships

**Purpose:** Confirm that related records use valid foreign keys.

**Action:**

Create related records using existing:

* Roles
* Users
* Organisers
* Participants
* Events
* Categories
* Enrolments

**Expected Result:**

Valid relationships should be accepted.

Records referencing non-existent parent records should be rejected.

**Status:** Passed

## Test Case 16: Sample Data Verification

**Purpose:** Confirm that the sample RaceDay data is available.

**Action:**

Run the queries in:

`Database_Verification_Queries.sql`

**Expected Result:**

The expected sample organisers, events, categories, enrolments, and results should be available.

**Status:** Passed

## Test Case Summary

| Test Case | Area Tested         | Expected Outcome               |
| --------- | ------------------- | ------------------------------ |
| 1         | Database creation   | Database exists                |
| 2         | Tables              | Required tables exist          |
| 3         | Event type          | Valid types accepted           |
| 4         | Event type          | Invalid types rejected         |
| 5         | Event distance      | Positive distance accepted     |
| 6         | Event distance      | Invalid distance rejected      |
| 7         | Category age        | Valid range accepted           |
| 8         | Category age        | Invalid range rejected         |
| 9         | User email          | Duplicate email rejected       |
| 10        | Enrolment status    | Valid status accepted          |
| 11        | Enrolment status    | Invalid status rejected        |
| 12        | Duplicate enrolment | Duplicate entry rejected       |
| 13        | Finish position     | Positive position accepted     |
| 14        | Finish position     | Invalid position rejected      |
| 15        | Foreign keys        | Invalid relationships rejected |
| 16        | Sample data         | Sample records available       |

## Relationship to the SQL Script

The test cases are based on the constraints and relationships implemented in:

`RaceDay_Database.sql`

They provide a practical way to verify that the database behaves according to the planned design.

## Relationship to the ERD

The test cases also support the relationships represented in:

`RaceDay_ERD.png`

The ERD defines the planned structure, while the SQL implementation enforces the relationships and constraints.

## Future Testing

During Part 2 API development, these database-level tests can be complemented by API tests.

For example, API testing can verify that:

* Unauthenticated users cannot access protected endpoints.
* Participants cannot perform Organiser-only operations.
* Participants can only access their own enrolments and results.
* Organisers can manage events and capture results.

This will provide additional validation of the RaceDay system beyond the database layer.

## Conclusion

The database test cases provide a structured method for verifying the RaceDay database.

They focus on database creation, relationships, constraints, valid data, invalid data, duplicate prevention, and sample data verification.

These tests help demonstrate that the RaceDay database design is supported by appropriate data-integrity rules.
