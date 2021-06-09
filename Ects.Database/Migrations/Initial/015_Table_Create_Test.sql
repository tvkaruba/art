create table Test
(
    Id bigint not null
        identity(1, 1)
  , NamespaceId bigint not null
  , [Name] nvarchar(128) not null
  , [Description] nvarchar(max) not null
  , [Version] bigint not null
        constraint DF_Test_Version default 1
  , IsActive bit not null
        constraint DF_Test_IsActive default 1
  , CreatedAtUtc datetime2(2) not null
        constraint DF_Test_CreatedAtUtc default sysutcdatetime()
  , CreatedBy bigint not null
  , ChangedAtUtc datetime2(2) null
        constraint DF_Test_ChangedAtUtc default null
  , ChangedBy bigint null
        constraint DF_Test_ChangedBy default null
  , constraint PK_Test primary key clustered (Id)
  , constraint FK_Test_Namespace_NamespaceId foreign key (NamespaceId) references [Namespace] (Id)
  , constraint FK_Test_Account_CreatedBy foreign key (CreatedBy) references Account (Id)
  , constraint FK_Test_Account_ChangedBy foreign key (ChangedBy) references Account (Id)
)
