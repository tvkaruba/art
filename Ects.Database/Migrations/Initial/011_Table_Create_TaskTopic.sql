create table TaskTopic (
    TaskId bigint not null
  , TopicId bigint not null
  , constraint PK_TaskTopic primary key clustered (TaskId, TopicId)
  , constraint FK_TaskTopic_Task_TaskId foreign key (TaskId) references Task (Id)
  , constraint FK_TaskTopic_Topic_TopicId foreign key (TopicId) references Topic (Id)
)