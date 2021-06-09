create trigger trQuestion_AfterUpdate
    on Question
   for update
   not for replication
as
merge into QuestionHistory as dst
using deleted as src
   on src.Id = dst.Id
  and ( src.ChangedBy = dst.ChangedBy or src.ChangedBy is null and dst.ChangedBy is null )
  and ( src.ChangedAtUtc = dst.ChangedAtUtc or src.ChangedAtUtc is null and dst.ChangedAtUtc is null )
 when not matched
 then insert
      (
          QuestionId
        , [Name]
        , [Description]
        , Body
        , Answers
        , Rights
        , ChangedAtUtc
        , ChangedBy
       )
       values
       (
           src.Id
         , src.[Name]
         , src.[Description]
         , src.Body
         , src.Answers
         , src.Rights
         , src.ChangedAtUtc
         , src.ChangedBy
       );
