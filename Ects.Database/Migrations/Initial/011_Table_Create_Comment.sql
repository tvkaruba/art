create table Comment
(
    Id bigint not null
        identity(1, 1)
  , Body nvarchar(max) not null
  , IsActive bit not null
        constraint DF_Comment_IsActive default 1
  , CreatedAtUtc datetime2(2) not null
        constraint DF_Comment_CreatedAtUtc default sysutcdatetime()
  , CreatedBy bigint not null
  , ChangedAtUtc datetime2(2) null
        constraint DF_Comment_ChangedAtUtc default null
  , ChangedBy bigint null
        constraint DF_Comment_ChangedBy default null
  , constraint PK_Comment primary key clustered (Id)
  , constraint FK_Comment_Account_CreatedBy foreign key (CreatedBy) references Account (Id)
  , constraint FK_Comment_Account_ChangedBy foreign key (ChangedBy) references Account (Id)
)
