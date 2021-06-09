create table Topic (
    Id bigint not null identity(1, 1)
  , Code nvarchar(255) not null
  , constraint PK_Topic primary key clustered (Id)
  , constraint UQ_Topic_Code unique (Code)
)