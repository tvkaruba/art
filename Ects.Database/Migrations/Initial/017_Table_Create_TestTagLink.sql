create table TestTagLink
(
    Id bigint not null
        identity(1, 1)
  , TestId bigint not null
  , TagId bigint not null
  , constraint PK_TestTagLink primary key clustered (Id)
  , constraint FK_TestTagLink_Test_TestId foreign key (TestId) references Test (Id)
  , constraint FK_TestTagLink_Tag_TagId foreign key (TagId) references Tag (Id)
)
