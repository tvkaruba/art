create table [Right] (
    Id bigint not null identity(1, 1)
  , TaskId bigint not null
  , Code nvarchar(255) not null
  , constraint PK_Right primary key clustered (Id)
  , constraint FK_Right_Task foreign key (TaskId) references Task (Id)
)