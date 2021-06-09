create table Variant (
    Id bigint not null identity(1, 1)
  , ModuleId int not null
  , [Name] nvarchar(max) not null
  , IsActive bit not null
  , CreatedAtUtc datetime not null constraint DF_VariantCreatedAtUtc default getutcdate()
  , ChangedAtUtc datetime null
  , constraint PK_Variant primary key clustered (Id)
  , constraint FK_Variant_Module_ModuleId foreign key (ModuleId) references Module (Id)
)