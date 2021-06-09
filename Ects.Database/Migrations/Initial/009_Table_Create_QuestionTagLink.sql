create table QuestionTagLink
(
    Id bigint not null
        identity(1, 1)
  , QuestionId bigint not null
  , TagId bigint not null
  , constraint PK_QuestionTagLink primary key clustered (Id)
  , constraint FK_QuestionTagLink_Question_QuestionId foreign key (QuestionId) references Question (Id)
  , constraint FK_QuestionTagLink_Tag_TagId foreign key (TagId) references Tag (Id)
)
