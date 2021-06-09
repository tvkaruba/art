create table Account
(
    Id bigint not null
        identity(1, 1)
  , Oid uniqueidentifier not null
  , [Login] nvarchar(128) not null
  , [Name] nvarchar(128) not null
  , Roles nvarchar(max) not null
  , Groups nvarchar(max) not null
  , IsActive bit not null
        constraint DF_Account_IsActive default 1
  , StudyGroup nvarchar(128) not null
  , constraint PK_Account primary key clustered (Id)
  , constraint UQ_Account_Oid unique (Oid)
)
