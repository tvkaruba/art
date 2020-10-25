create table Task (
    Id bigint not null identity(1, 1)
  , ModuleId int not null
  , TaskTypeId int not null
  , [Name] nvarchar(max) not null
  , Body nvarchar(max) not null
  , IsActive bit not null
  , CreatedAtUtc datetime not null constraint DF_TaskCreatedAtUtc default getutcdate()
  , ChangedAtUtc datetime null
  , constraint PK_Task primary key clustered (Id)
  , constraint FK_Task_Module_ModuleId foreign key (ModuleId) references Module (Id)
  , constraint FK_Task_TaskType_TaskTypeId foreign key (TaskTypeId) references TaskType (Id)
)