create table PersonRole (
    Id int not null identity(1, 1)
  , Code nvarchar(255) not null
  , constraint PK_PersonRole primary key clustered (Id)
  , constraint UQ_PersonRole_Code unique (Code)
)