USE master;
GO

CREATE DATABASE RaceDayDB;
GO

USE RaceDayDB;
GO




CREATE TABLE Role
(
    RoleID INT IDENTITY(1,1) NOT NULL,
    RoleName VARCHAR(50) NOT NULL,

    CONSTRAINT PK_Role PRIMARY KEY (RoleID),
    CONSTRAINT UQ_Role_RoleName UNIQUE (RoleName)
);
GO



CREATE TABLE [User]
(
    UserID INT IDENTITY(1,1) NOT NULL,
    RoleID INT NOT NULL,
    FirstName VARCHAR(100) NOT NULL,
    LastName VARCHAR(100) NOT NULL,
    Email VARCHAR(150) NOT NULL,
    PasswordHash VARCHAR(255) NOT NULL,
    Phone VARCHAR(20) NULL,
    DateCreated DATETIME2 NOT NULL
        CONSTRAINT DF_User_DateCreated DEFAULT GETUTCDATE(),
    IsActive BIT NOT NULL
        CONSTRAINT DF_User_IsActive DEFAULT 1,

    CONSTRAINT PK_User PRIMARY KEY (UserID),

    CONSTRAINT FK_User_Role
        FOREIGN KEY (RoleID)
        REFERENCES Role(RoleID),

    CONSTRAINT UQ_User_Email UNIQUE (Email)
);
GO




CREATE TABLE Organizer
(
    OrganizerID INT IDENTITY(1,1) NOT NULL,
    UserID INT NOT NULL,
    OrganisationName VARCHAR(150) NOT NULL,
    Description VARCHAR(500) NULL,
    Website VARCHAR(150) NULL,

    CONSTRAINT PK_Organizer PRIMARY KEY (OrganizerID),

    CONSTRAINT FK_Organizer_User
        FOREIGN KEY (UserID)
        REFERENCES [User](UserID),

    CONSTRAINT UQ_Organizer_UserID UNIQUE (UserID)
);
GO




CREATE TABLE Participant
(
    ParticipantID INT IDENTITY(1,1) NOT NULL,
    UserID INT NOT NULL,
    DateOfBirth DATE NOT NULL,
    Gender VARCHAR(10) NULL,
    Club VARCHAR(150) NULL,

    CONSTRAINT PK_Participant PRIMARY KEY (ParticipantID),

    CONSTRAINT FK_Participant_User
        FOREIGN KEY (UserID)
        REFERENCES [User](UserID),

    CONSTRAINT UQ_Participant_UserID UNIQUE (UserID)
);
GO




CREATE TABLE Event
(
    EventID INT IDENTITY(1,1) NOT NULL,
    OrganizerID INT NOT NULL,
    EventName VARCHAR(200) NOT NULL,
    Description VARCHAR(1000) NULL,
    EventDate DATE NOT NULL,
    Location VARCHAR(200) NOT NULL,
    Distance DECIMAL(6,2) NOT NULL,
    EventType VARCHAR(20) NOT NULL,
    CreatedAt DATETIME2 NOT NULL
        CONSTRAINT DF_Event_CreatedAt DEFAULT GETUTCDATE(),

    CONSTRAINT PK_Event PRIMARY KEY (EventID),

    CONSTRAINT FK_Event_Organizer
        FOREIGN KEY (OrganizerID)
        REFERENCES Organizer(OrganizerID),

    CONSTRAINT CK_Event_Distance
        CHECK (Distance > 0),

    CONSTRAINT CK_Event_EventType
        CHECK (EventType IN ('Run', 'Walk', 'Cycle'))
);
GO




CREATE TABLE Category
(
    CategoryID INT IDENTITY(1,1) NOT NULL,
    EventID INT NOT NULL,
    CategoryName VARCHAR(100) NOT NULL,
    Description VARCHAR(255) NULL,
    MinAge INT NULL,
    MaxAge INT NULL,
    MinDistance DECIMAL(6,2) NULL,
    MaxDistance DECIMAL(6,2) NULL,
    SortOrder INT NOT NULL
        CONSTRAINT DF_Category_SortOrder DEFAULT 0,

    CONSTRAINT PK_Category PRIMARY KEY (CategoryID),

    CONSTRAINT FK_Category_Event
        FOREIGN KEY (EventID)
        REFERENCES Event(EventID),

    CONSTRAINT CK_Category_AgeRange
        CHECK (
            (MinAge IS NULL OR MinAge >= 0)
            AND
            (MaxAge IS NULL OR MaxAge >= 0)
            AND
            (MinAge IS NULL OR MaxAge IS NULL OR MinAge <= MaxAge)
        ),

    CONSTRAINT CK_Category_DistanceRange
        CHECK (
            (MinDistance IS NULL OR MinDistance > 0)
            AND
            (MaxDistance IS NULL OR MaxDistance > 0)
            AND
            (
                MinDistance IS NULL
                OR MaxDistance IS NULL
                OR MinDistance <= MaxDistance
            )
        )
);
GO



CREATE TABLE EventEnrolment
(
    EnrolmentID INT IDENTITY(1,1) NOT NULL,
    ParticipantID INT NOT NULL,
    EventID INT NOT NULL,
    CategoryID INT NOT NULL,
    EnrolmentDate DATETIME2 NOT NULL
        CONSTRAINT DF_EventEnrolment_EnrolmentDate
        DEFAULT GETUTCDATE(),
    Status VARCHAR(20) NOT NULL
        CONSTRAINT DF_EventEnrolment_Status
        DEFAULT 'Confirmed',

    CONSTRAINT PK_EventEnrolment
        PRIMARY KEY (EnrolmentID),

    CONSTRAINT FK_EventEnrolment_Participant
        FOREIGN KEY (ParticipantID)
        REFERENCES Participant(ParticipantID),

    CONSTRAINT FK_EventEnrolment_Event
        FOREIGN KEY (EventID)
        REFERENCES Event(EventID),

    CONSTRAINT FK_EventEnrolment_Category
        FOREIGN KEY (CategoryID)
        REFERENCES Category(CategoryID),

    CONSTRAINT CK_EventEnrolment_Status
        CHECK (Status IN ('Confirmed', 'Cancelled', 'Pending')),

    CONSTRAINT UQ_EventEnrolment_Participant_Event
        UNIQUE (ParticipantID, EventID)
);
GO



CREATE TABLE Result
(
    ResultID INT IDENTITY(1,1) NOT NULL,
    EnrolmentID INT NOT NULL,
    FinishTime TIME(0) NOT NULL,
    FinishPosition INT NOT NULL,
    OverallPosition INT NULL,
    RecordedBy INT NULL,
    RecordedAt DATETIME2 NOT NULL
        CONSTRAINT DF_Result_RecordedAt DEFAULT GETUTCDATE(),

    CONSTRAINT PK_Result PRIMARY KEY (ResultID),

    CONSTRAINT FK_Result_Enrolment
        FOREIGN KEY (EnrolmentID)
        REFERENCES EventEnrolment(EnrolmentID),

    CONSTRAINT FK_Result_RecordedBy
        FOREIGN KEY (RecordedBy)
        REFERENCES [User](UserID),

    CONSTRAINT UQ_Result_Enrolment
        UNIQUE (EnrolmentID),

    CONSTRAINT CK_Result_FinishPosition
        CHECK (FinishPosition > 0),

    CONSTRAINT CK_Result_OverallPosition
        CHECK (
            OverallPosition IS NULL
            OR OverallPosition > 0
        )
);
GO




INSERT INTO Role (RoleName)
VALUES
    ('Organizer'),
    ('Participant');
GO




INSERT INTO [User]
(
    RoleID,
    FirstName,
    LastName,
    Email,
    PasswordHash,
    Phone
)
VALUES
(
    1,
    'Thabo',
    'Mokoena',
    'thabo@raceday.co.za',
    'DEMO_HASH_THABO',
    '0825551001'
),
(
    1,
    'Naledi',
    'Dlamini',
    'naledi@raceday.co.za',
    'DEMO_HASH_NALEDI',
    '0825551002'
),
(
    2,
    'Sipho',
    'Ndlovu',
    'sipho@example.co.za',
    'DEMO_HASH_SIPHO',
    '0825552001'
),
(
    2,
    'Aisha',
    'Naidoo',
    'aisha@example.co.za',
    'DEMO_HASH_AISHA',
    '0825552002'
);
GO




INSERT INTO Organizer
(
    UserID,
    OrganisationName,
    Description,
    Website
)
VALUES
(
    1,
    'Mokoena Events',
    'South African road running and community event organiser.',
    'https://example.co.za/mokoena-events'
),
(
    2,
    'Dlamini Sports Events',
    'Organiser of running, walking and cycling events.',
    'https://example.co.za/dlamini-sports'
);
GO




INSERT INTO Participant
(
    UserID,
    DateOfBirth,
    Gender,
    Club
)
VALUES
(
    3,
    '2001-06-15',
    'Male',
    'Johannesburg Road Runners'
),
(
    4,
    '1998-11-22',
    'Female',
    'Cape Town Running Club'
);
GO



INSERT INTO Event
(
    OrganizerID,
    EventName,
    Description,
    EventDate,
    Location,
    Distance,
    EventType
)
VALUES
(
    1,
    'Johannesburg Community 10K',
    'A community road running event for runners of different abilities.',
    '2027-03-14',
    'Johannesburg, Gauteng',
    10.00,
    'Run'
),
(
    1,
    'Soweto Family Walk',
    'A family-friendly walking event through Soweto.',
    '2027-04-10',
    'Soweto, Gauteng',
    5.00,
    'Walk'
),
(
    2,
    'Cape Town Coastal Cycle',
    'A recreational cycling event along the Cape Town coastline.',
    '2027-05-02',
    'Cape Town, Western Cape',
    42.00,
    'Cycle'
);
GO



INSERT INTO Category
(
    EventID,
    CategoryName,
    Description,
    MinAge,
    MaxAge,
    MinDistance,
    MaxDistance,
    SortOrder
)
VALUES
(
    1,
    'Junior',
    'Participants under 20 years old.',
    13,
    19,
    NULL,
    NULL,
    1
),
(
    1,
    'Senior',
    'Participants aged 20 to 39.',
    20,
    39,
    NULL,
    NULL,
    2
),
(
    1,
    'Masters',
    'Participants aged 40 and older.',
    40,
    NULL,
    NULL,
    NULL,
    3
),
(
    2,
    'Family Walk',
    'General family walking category.',
    NULL,
    NULL,
    5.00,
    5.00,
    1
),
(
    2,
    'Senior Walk',
    'Walking category for older participants.',
    40,
    NULL,
    NULL,
    NULL,
    2
),
(
    3,
    'Open Cycle',
    'Open category for the 42 km cycling event.',
    18,
    NULL,
    42.00,
    42.00,
    1
),
(
    3,
    'Masters Cycle',
    'Masters cycling category.',
    40,
    NULL,
    42.00,
    42.00,
    2
);
GO


INSERT INTO EventEnrolment
(
    ParticipantID,
    EventID,
    CategoryID,
    Status
)
VALUES
(
    1,
    1,
    2,
    'Confirmed'
),
(
    2,
    1,
    2,
    'Confirmed'
),
(
    1,
    2,
    4,
    'Confirmed'
),
(
    2,
    3,
    6,
    'Confirmed'
);
GO




INSERT INTO Result
(
    EnrolmentID,
    FinishTime,
    FinishPosition,
    OverallPosition,
    RecordedBy
)
VALUES
(
    1,
    '00:52:34',
    1,
    1,
    1
),
(
    2,
    '00:58:12',
    2,
    2,
    1
);
GO



SELECT * FROM Role;
SELECT * FROM [User];
SELECT * FROM Organizer;
SELECT * FROM Participant;
SELECT * FROM Event;
SELECT * FROM Category;
SELECT * FROM EventEnrolment;
SELECT * FROM Result;
GO