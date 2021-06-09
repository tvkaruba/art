create table QuestionQuestionConflict
(
    Id bigint not null
        identity(1, 1)
  , FirstQuestionId bigint not null
  , SecondQuestionId bigint not null
  , IsResolved bit not null
        constraint DF_QuestionQuestionConflict_IsResolved default 0
  , constraint PK_QuestionQuestionConflict primary key clustered (Id)
  , constraint FK_QuestionQuestionConflict_Question_FirstQuestionId foreign key (FirstQuestionId) references Question (Id)
  , constraint FK_QuestionQuestionConflict_Question_SecondQuestionId foreign key (SecondQuestionId) references Question (Id)
)
