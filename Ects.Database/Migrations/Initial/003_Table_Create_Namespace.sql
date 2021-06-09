create table [Namespace]
(
    Id bigint not null
        identity(1, 1)
  , [Name] nvarchar(128) not null
  , [Description] nvarchar(max) not null
  , ImageId bigint null
  , IsActive bit not null
        constraint DF_Namespace_IsActive default 1
  , CreatedAtUtc datetime2(2) not null
        constraint DF_Namespace_CreatedAtUtc default sysutcdatetime()
  , CreatedBy bigint not null
  , ChangedAtUtc datetime2(2) null
        constraint DF_Namespace_ChangedAtUtc default null
  , ChangedBy bigint null
        constraint DF_Namespace_ChangedBy default null
  , constraint PK_Namespace primary key clustered (Id)
  , constraint FK_Namespace_Image_ImageId foreign key (ImageId) references [Image] (Id)
  , constraint FK_Namespace_Account_CreatedBy foreign key (CreatedBy) references Account (Id)
  , constraint FK_Namespace_Account_ChangedBy foreign key (ChangedBy) references Account (Id)
  , constraint UQ_Namespace_Name unique ([Name])
)
