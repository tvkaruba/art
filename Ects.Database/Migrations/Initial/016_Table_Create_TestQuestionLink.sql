create table TestQuestionLink
(
    Id bigint not null
        identity(1, 1)
  , TestId bigint not null
  , QuestionId bigint not null
  , [Value] float not null
        constraint DF_TestQuestionLink_Value default 1.0
  , [Order] bigint not null
  , constraint PK_TestQuestionLink primary key clustered (Id)
  , constraint FK_TestQuestionLink_Test_TestId foreign key (TestId) references Test (Id)
  , constraint FK_TestQuestionLink_Question_QuestionId foreign key (QuestionId) references Question (Id)
  , constraint UQ_TestQuestionLink_TestIdQuestionId unique (TestId, QuestionId)
  , constraint UQ_TestQuestionLink_Order unique ([Order])
)
