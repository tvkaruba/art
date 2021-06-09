create table [Image]
(
    Id bigint not null
  , [Url] nvarchar(max) not null
  , constraint PK_Image primary key clustered (Id)
)
