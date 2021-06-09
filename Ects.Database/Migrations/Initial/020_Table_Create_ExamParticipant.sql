create table ExamParticipant
(
    Id bigint not null
        identity(1, 1)
  , ExamId bigint not null
  , AccountId bigint not null
  , StartTime datetime2(2) null
  , EndTime datetime2(2) null
  , Result float null
  , MaxResult float null
  , constraint PK_ExamParticipant primary key clustered (Id)
  , constraint FK_ExamParticipant_Exam_ExamId foreign key (ExamId) references Exam (Id)
  , constraint FK_ExamParticipant_Account_AccountId foreign key (AccountId) references Account (Id)
)
