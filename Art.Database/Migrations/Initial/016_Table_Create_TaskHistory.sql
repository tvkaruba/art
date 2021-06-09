create table TaskHistory (
    Id bigint not null
  , ModuleId int not null
  , TaskTypeId int not null
  , [Name] nvarchar(max) not null
  , Body nvarchar(max) not null
  , IsActive bit not null
  , ChangedAtUtc datetime null
)