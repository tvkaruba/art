create table TaskTaskConflict (
    FirstTaskId bigint not null
  , SecondTaskId bigint not null
  , constraint PK_TaskTask primary key clustered (FirstTaskId, SecondTaskId)
  , constraint FK_TaskTaskConflict_Task_FirstTaskId foreign key (FirstTaskId) references Task (Id)
  , constraint FK_TaskTaskConflict_Task_SecondTaskId foreign key (SecondTaskId) references Task (Id)
)