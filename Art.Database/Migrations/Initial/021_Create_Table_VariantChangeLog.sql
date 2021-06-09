create table VariantChangeLog (
    Id bigint not null identity(1, 1)
  , VariantId bigint not null
  , Record nvarchar(max) not null
  , CreatedAtUtc datetime not null constraint DF_RecordCreatedAtUtc default getutcdate()
  , constraint PK_VariantChangeLog primary key clustered (Id)
  , constraint FK_VariantChangeLog_Variant_VariantId foreign key (VariantId) references Variant (Id)
)