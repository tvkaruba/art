create table Module (
    Id int not null identity(1, 1)
  , Code nvarchar(255) not null
  , constraint PK_Module primary key clustered (Id)
  , constraint UQ_Module_Code unique (Code)
)