create table Tag (
    Id bigint not null identity(1, 1)
  , Code nvarchar(255) not null
  , constraint PK_Tag primary key clustered (Id)
  , constraint UQ_Tag_Code unique (Code)
)