create table TaskType (
    Id int not null identity(1, 1)
  , Code nvarchar(255) not null
  , constraint PK_TaskType primary key clustered (Id)
  , constraint UQ_TaskType_Code unique (Code)
)