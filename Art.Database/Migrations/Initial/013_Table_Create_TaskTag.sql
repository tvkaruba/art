create table TaskTag (
    TaskId bigint not null
  , TagId bigint not null
  , constraint PK_TaskTag primary key clustered (TaskId, TagId)
  , constraint FK_TaskTag_Task_TaskId foreign key (TaskId) references Task (Id)
  , constraint FK_TaskTag_Tag_TagId foreign key (TagId) references Tag (Id)
)