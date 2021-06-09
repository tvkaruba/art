create table ExamParticipantAnswer
(
    Id bigint not null
        identity(1, 1)
  , ExamParticipantId bigint not null
  , QuestionId bigint not null
  , Answer nvarchar(max) not null
  , [Value] float null
  , constraint PK_ExamParticipantAnswer primary key clustered (Id)
  , constraint FK_ExamParticipantAnswer_ExamParticipant_ExamParticipantId foreign key (ExamParticipantId) references ExamParticipant (Id)
  , constraint FK_ExamParticipantAnswer_Question_QuestionId foreign key (QuestionId) references Question (Id)
)
