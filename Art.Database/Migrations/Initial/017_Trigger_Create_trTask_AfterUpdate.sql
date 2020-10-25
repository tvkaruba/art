create trigger trTask_AfterUpdate on Task
   for update
   not for replication
as
merge into TaskHistory as dst
using deleted as src
   on (
    src.Id = dst.Id
  ) and (
    src.ChangedAtUtc = dst.ChangedAtUtc
    or
    src.ChangedAtUtc is null
    and
    dst.ChangedAtUtc is null
  )
 when not matched then
   insert (
       Id
     , ModuleId
     , TaskTypeId
     , [Name]
     , Body
     , IsActive
     , ChangedAtUtc
   ) values (
       src.Id
     , src.ModuleId
     , src.TaskTypeId
     , src.[Name]
     , src.Body
     , src.IsActive
     , src.ChangedAtUTC
   );