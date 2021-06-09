create table Exam
(
    Id bigint not null
        identity(1, 1)
  , TestId bigint not null
  , [Name] nvarchar(128) not null
  , [Description] nvarchar(max) not null
  , StartAvailabilityTime datetime2(2) not null
  , EndAvailabilityTime datetime2(2) not null
  , MaxDurationInMinutes bigint not null
  , IsActive bit not null
        constraint DF_Exam_IsActive default 1
  , CreatedAtUtc datetime2(2) not null
        constraint DF_Exam_CreatedAtUtc default sysutcdatetime()
  , CreatedBy bigint not null
  , ChangedAtUtc datetime2(2) null
        constraint DF_Exam_ChangedAtUtc default null
  , ChangedBy bigint null
        constraint DF_Exam_ChangedBy default null
  , constraint PK_Exam primary key clustered (Id)
  , constraint FK_Exam_Test_TestId foreign key (TestId) references Test (Id)
  , constraint FK_Exam_Account_CreatedBy foreign key (CreatedBy) references Account (Id)
  , constraint FK_Exam_Account_ChangedBy foreign key (ChangedBy) references Account (Id)
)
