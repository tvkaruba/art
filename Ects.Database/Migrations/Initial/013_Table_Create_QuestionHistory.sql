create table QuestionHistory
(
    Id bigint not null
        identity(1, 1)
  , QuestionId bigint not null
  , [Name] nvarchar(128) not null
  , [Description] nvarchar(max) not null
  , Body nvarchar(max) not null
  , Answers nvarchar(max) not null
  , Rights nvarchar(max) not null
  , ChangedAtUtc datetime2(2) null
  , ChangedBy bigint null
  , constraint PK_QuestionHistory primary key clustered (Id)
  , constraint FK_QuestionHistory_Question_QuestionId foreign key (QuestionId) references Question (Id)
  , constraint FK_QuestionHistory_Account_ChangedBy foreign key (ChangedBy) references Account (Id)
)
