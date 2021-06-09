create table QuestionType
(
    Id bigint not null
        identity(1, 1)
  , [Type] nvarchar(128) not null
  , [Name] nvarchar(128) not null
  , [Description] nvarchar(max) not null
  , constraint PK_QuestionType primary key clustered (Id)
  , constraint UQ_QuestionType_Type unique ([Type])
)
