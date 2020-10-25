create table VariantTask (
    VariantId bigint not null
  , TaskId bigint not null
  , constraint PK_VariantTask primary key clustered (VariantId, TaskId)
  , constraint FK_VariantTask_Variant_VariantId foreign key (VariantId) references Variant (Id)
  , constraint FK_VariantTask_Task_TaskId foreign key (TaskId) references Task (Id)
)