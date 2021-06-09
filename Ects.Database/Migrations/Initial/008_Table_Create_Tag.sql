create table Tag
(
    Id bigint not null
        identity(1, 1)
  , [Key] nvarchar(128) not null
  , [Value] nvarchar(128) not null
  , constraint PK_Tag primary key clustered (Id)
  , constraint UQ_Tag_KeyValue unique ([Key], [Value])
)
