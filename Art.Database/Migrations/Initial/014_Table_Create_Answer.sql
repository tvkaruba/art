create table Answer (
    Id bigint not null identity(1, 1)
  , TaskId bigint not null
  , Code nvarchar(255) not null
  , constraint PK_Answer primary key clustered (Id)
  , constraint FK_Answer_Task foreign key (TaskId) references Task (Id)
)