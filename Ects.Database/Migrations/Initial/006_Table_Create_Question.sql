create table Question
(
    Id bigint not null
        identity(1, 1)
  , NamespaceId bigint not null
  , QuestionTypeId bigint not null
  , [Name] nvarchar(128) not null
  , [Description] nvarchar(max) not null
  , Body nvarchar(max) not null
  , Answers nvarchar(max) not null
  , Rights nvarchar(max) not null
  , Likes bigint not null
        constraint DF_Question_Likes default 0
  , Dislikes bigint not null
        constraint DF_Question_Dislikes default 0
  , IsActive bit not null
        constraint DF_Question_IsActive default 1
  , CreatedAtUtc datetime2(2) not null
        constraint DF_Question_CreatedAtUtc default sysutcdatetime()
  , CreatedBy bigint not null
  , ChangedAtUtc datetime2(2) null
        constraint DF_Question_ChangedAtUtc default null
  , ChangedBy bigint null
        constraint DF_Question_ChangedBy default null
  , constraint PK_Question primary key clustered (Id)
  , constraint FK_Question_Namespace_NamespaceId foreign key (NamespaceId) references [Namespace] (Id)
  , constraint FK_Question_QuestionType_QuestionTypeId foreign key (QuestionTypeId) references QuestionType (Id)
  , constraint FK_Question_Account_CreatedBy foreign key (CreatedBy) references Account (Id)
  , constraint FK_Question_Account_ChangedBy foreign key (ChangedBy) references Account (Id)
)
