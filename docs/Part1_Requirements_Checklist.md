# RaceDay Part 1 Requirements Checklist

## Overview

This checklist tracks the RaceDay Part 1 requirements and supporting deliverables.

It provides a quick overview of the planning, database, documentation, version-control, CI/CD, and submission preparation completed for Part 1.

## Part 1 Deliverables

| Requirement                                 | Status     | Evidence                                 |
| ------------------------------------------- | ---------- | ---------------------------------------- |
| ERD completed                               | ✅ Complete | `docs/RaceDay_ERD.png`                   |
| ERD contains the planned database structure | ✅ Complete | `docs/RaceDay_ERD.png`                   |
| API Endpoint Plan completed                 | ✅ Complete | `docs/RaceDay API Endpoint Plan.pdf`     |
| Authentication endpoints planned            | ✅ Complete | API Endpoint Plan                        |
| User Profile endpoints planned              | ✅ Complete | API Endpoint Plan                        |
| Event endpoints planned                     | ✅ Complete | API Endpoint Plan                        |
| Category endpoints planned                  | ✅ Complete | API Endpoint Plan                        |
| Event Enrolment endpoints planned           | ✅ Complete | API Endpoint Plan                        |
| Results endpoints planned                   | ✅ Complete | API Endpoint Plan                        |
| SQL database script completed               | ✅ Complete | `docs/RaceDay_Database.sql`              |
| Database created and verified in SQL Server | ✅ Complete | `docs/Database_Setup_Guide.md`           |
| Database verification queries documented    | ✅ Complete | `docs/Database_Verification_Queries.sql` |
| Database design decisions documented        | ✅ Complete | `docs/Database_Design_Decisions.md`      |
| Data dictionary completed                   | ✅ Complete | `docs/Data_Dictionary.md`                |
| ERD relationships explained                 | ✅ Complete | `docs/ERD_Explanation.md`                |
| SQL implementation documented               | ✅ Complete | `docs/SQL_Implementation_Notes.md`       |
| Database test cases documented              | ✅ Complete | `docs/Part1_Database_Test_Cases.md`      |
| Organiser role documented                   | ✅ Complete | `docs/Roles_and_Permissions_Guide.md`    |
| Participant role documented                 | ✅ Complete | `docs/Roles_and_Permissions_Guide.md`    |
| API role access documented                  | ✅ Complete | `docs/API_Role_Access_Matrix.md`         |
| Project structure documented                | ✅ Complete | `docs/Project_Structure_Guide.md`        |
| GitHub Actions workflow created             | ✅ Complete | `.github/workflows/part1-validation.yml` |
| GitHub Actions validation successful        | ✅ Complete | `docs/RaceDay_CI_Green_Build.png`        |
| README created                              | ✅ Complete | `README.md`                              |
| Part 1 video recorded                       | ✅ Complete | Part 1 video                             |
| Part 1 video uploaded to YouTube            | ✅ Complete | Unlisted YouTube video                   |
| YouTube link added to README                | ✅ Complete | `README.md`                              |
| GitHub repository ready for ARC submission  | ✅ Complete | GitHub repository                        |
| Meaningful Part 1 commits                   | ✅ Complete | Git commit history                       |

## Database Documentation

The following database documentation has been completed:

* `RaceDay_Database.sql`
* `Database_Setup_Guide.md`
* `Database_Verification_Queries.sql`
* `Database_Verification_Guide.md`
* `Database_Design_Decisions.md`
* `Data_Dictionary.md`
* `SQL_Implementation_Notes.md`
* `Part1_Database_Test_Cases.md`

These documents provide information about the database structure, implementation, verification, data integrity, and testing.

## API Planning Documentation

The following API planning documentation has been completed:

* `RaceDay API Endpoint Plan.pdf`
* `API_Design_Notes.md`
* `API_Role_Access_Matrix.md`

The documentation covers the planned API resources, HTTP methods, roles, permissions, authentication, enrolments, and results.

## Role Documentation

RaceDay supports two main system roles:

### Organiser

Organisers can:

* Create events.
* Edit events.
* Delete events.
* Manage event categories.
* View event enrolments.
* Capture participant results.

### Participant

Participants can:

* Create an account.
* Browse events.
* Enter events.
* Select an event category.
* View their own enrolments.
* Track their personal results.

## Version Control

The RaceDay project is maintained using Git and GitHub.

Meaningful commits are used to record project progress and document individual pieces of work.

The project is maintained on the `main` branch.

The commit history provides evidence of the development and documentation work completed during Part 1.

## CI/CD

GitHub Actions is used to validate the required Part 1 documentation.

The workflow is located at:

`.github/workflows/part1-validation.yml`

A successful GitHub Actions build is documented using:

`docs/RaceDay_CI_Green_Build.png`

## Video Evidence

The Part 1 demonstration video has been recorded and uploaded to YouTube as an unlisted video.

The YouTube link has been added to the project `README.md`.

The video demonstrates the completed Part 1 work and provides supporting evidence for the project submission.

## Final Submission Preparation

Before submitting Part 1 through ARC, the following items should be confirmed:

* [x] GitHub repository contains the Part 1 documentation.
* [x] ERD is stored in the `docs` folder.
* [x] API Endpoint Plan is stored in the `docs` folder.
* [x] SQL script is stored in the `docs` folder.
* [x] Supporting database documentation is stored in the `docs` folder.
* [x] API planning documentation is stored in the `docs` folder.
* [x] GitHub Actions workflow is present.
* [x] GitHub Actions build has passed.
* [x] CI/CD screenshot is included.
* [x] Part 1 video has been recorded.
* [x] Part 1 video has been uploaded to YouTube as an unlisted video.
* [x] YouTube link has been added to the README.
* [x] GitHub repository link is ready for ARC submission.
* [x] Meaningful Part 1 commits have been completed.

## Part 1 Completion

The RaceDay Part 1 planning and database work has been completed.

The repository contains the required planning documents, ERD, API Endpoint Plan, SQL database implementation, supporting documentation, CI/CD workflow, video evidence, and Git history.

The project is ready for final Part 1 review and submission.

## Future Development

Part 2 will implement the planned RaceDay API.

The API implementation should remain consistent with:

* The database structure.
* The ERD.
* The API Endpoint Plan.
* The role and permission requirements.

Part 3 will implement the MVC application that consumes the API and provides the user interface for Organisers and Participants.

## Conclusion

The Part 1 checklist confirms that the major planning, database, documentation, testing, version-control, CI/CD, and video requirements have been addressed.

This checklist can be used as a final reference before submitting the RaceDay Part 1 repository through ARC.
